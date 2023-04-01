
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    // idea here is that I could break this class up into smaller classes with interfaces - I like the idea
    //public class LoopCache : ICachedData
    //{
    //    private readonly WTEdgeContext db;
    //    public LoopCache(WTEdgeContext db)
    //    {
    //        this.db = db;
    //    }
    //}

    public class DBDataLoader : IDBLoader
    {
        private readonly WTEdgeContext db;
        private readonly Dictionary<string, DBLoopData> loopDataCache;
        private readonly Dictionary<string, List<SDKData>> sdkDataCache;
        public DBDataLoader()
        {
            this.db = new WTEdgeContext();
            loopDataCache = new Dictionary<string, DBLoopData>();
            sdkDataCache = new Dictionary<string, List<SDKData>>();
        }

        public List<SDKData> GetSDs(string tag)
        {
            if (sdkDataCache.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = db.Tblsdkrelations
                    .Where(x => x.Parenttags == tag)
                    .Select(sd => new SDKData
                            {
                                ParentTag = GetCleanString(sd.Parenttags),
                                InputTag = GetCleanString(sd.Inputtags),
                                OutputTag = GetCleanString(sd.Outputtag),
                                OutputDescription = sd.OutputtagNavigation == null ?
                                                    string.Empty :
                                                    GetCleanString(sd.OutputtagNavigation.Servicedescription),
                                SdGroup = GetCleanString(sd.Sdgroup),
                                SdAction1 = GetCleanString(sd.Sdaction1),
                                SdAction2 = GetCleanString(sd.Sdaction2)
                            })
                    .ToList();
                sdkDataCache[tag] = data;
                return data;
            }
        }

        public List<LoopNoTemplatePair> GetLoops()
        {
            string[] currentTestingLoops =
            {
                // DIN-4W tests
                //"F-1521",
                //"F-1914B",

                // AIN tests
                "L-7100",
                "P-1102",

                // PID tests
                "L-1400",

                // XV tests
                //"X-1300",

                
            };
            return db.Tblloops
                      // it's possible that I will want to gracefully handle Looptemplate == null in the future
                      // I'm thinking of a log file message or something, but for now, I don't want it
                      .Where(x => (x.Loop != "---") && (x.Loop != null) && (x.Looptemplate != null))
                      .Where(x => currentTestingLoops.Contains(x.Loop)) // just to limit the results for testing purposes
                      .Select(loop => new LoopNoTemplatePair
                                      {
                                          LoopNo = loop.Loop ?? string.Empty,
                                          Template = loop.Looptemplate ?? string.Empty,
                                      })
                      .ToList();
        }

        public IEnumerable<LoopTagData> GetLoopTags(LoopNoTemplatePair loop)
        {
            string[] badStatus = { "OUT OF SCOPE", "DELETE", "HOLD" };
            return db.Tblindices
                     .Where(t => (t.Loopno == loop.LoopNo) && !badStatus.Contains(t.Status))
                     .Select(tag => new LoopTagData
                     {
                         Tag = GetCleanString(tag.Tag),
                         IOType = GetCleanString(tag.Iotype),
                         InstrumentType = GetCleanString(tag.Instrumenttype),
                         System = GetCleanString(tag.System)
                     })
                     .ToList();
        }
        
        public DBLoopData GetLoopData(string tag)
        {
            /// Memoized version
            ///     First check to see if the data is in the dictionar and if it is simply return it
            ///     If it is not in the dict then fetch it and add it to the dict and return it
            ///     Now any future call for this same data will not need to fetch it
            if (loopDataCache.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = db.Tblindices.Where(t => t.Tag == tag).Select(
                    d => new DBLoopData
                    {
                        Tag = d.Tag,
                        LoopNo = GetCleanString(d.Loopno),
                        Description = GetCleanString(d.Servicedescription),

                        Manufacturer = FetchManufacturerModel(d.Tblbominstr, "Manufacturer"),
                        Model = FetchManufacturerModel(d.Tblbominstr, "Model"),

                        JB1Tag = GetCleanString(d.Jb1tag),
                        JB2Tag = GetCleanString(d.Jb2tag),

                        Rack = FetchRackSlotChannel(d, "rack"),
                        Slot = FetchRackSlotChannel(d, "slot"),
                        Channel = FetchRackSlotChannel(d, "channel"),

                        PidDrawingNumber = GetCleanString(d.Pid),

                        MinCalRange = FetchCalRange(d.Tblarss, "min"),
                        MaxCalRange = FetchCalRange(d.Tblarss, "max"),
                        RangeUnits = FetchCalRange(d.Tblarss, "units"),

                        LoLoAlarm = FetchAlarmString(d.Tblarss, "ll"),
                        LoAlarm = FetchAlarmString(d.Tblarss, "l"),
                        HiAlarm = FetchAlarmString(d.Tblarss, "h"),
                        HiHiAlarm = FetchAlarmString(d.Tblarss, "hh"),
                        LoControl = FetchAlarmString(d.Tblarss, "lc"),
                        HiControl = FetchAlarmString(d.Tblarss, "hc"),

                        FailPosition = GetCleanString(d.Failposition),
                        InstrumentType = GetCleanString(d.Instrumenttype),
                        IoType = GetCleanString(d.Iotype),

                        System = GetCleanString(d.System),
                        SystemType = (d.Tblsystem == null) ? string.Empty : GetCleanString(d.Tblsystem.SystemType)

                    }).FirstOrDefault();
                loopDataCache[tag] = data ?? new DBLoopData();

                return loopDataCache[tag];
            }
        }

        private static string FetchRackSlotChannel(Tblindex index, string rackSlotChannel)
        {
            if (index is null)
            {
                return "-1";
            }

            return rackSlotChannel.ToLower() switch
            {
                "rack" => (index.Rack ?? -1).ToString(),
                "slot" => (index.Slot ?? -1).ToString(),
                "channel" => (index.Channel ?? -1).ToString(),
                _ => string.Empty
            };
        }

        private static string FetchManufacturerModel(Tblbominstr? bom, string manufacturerModel)
        {
            if (bom is null)
            {
                return string.Empty;
            }

            return manufacturerModel.ToLower() switch
            {
                "manufacturer" => GetCleanString(bom.Manufacturer),
                "model" => GetCleanString(bom.Model),
                _ => string.Empty
            };
        }

        private static string FetchAlarmString(Tblarss? tblarss, string alarm)
        {
            if (tblarss is null)
            {
                return string.Empty;
            }

            return alarm.ToLower() switch
            {
                "ll" => GetCleanString(tblarss.Llalarm),
                "l" => GetCleanString(tblarss.Loalarm),
                "h" => GetCleanString(tblarss.Hialarm),
                "hh" => GetCleanString(tblarss.Hhalarm),
                "lc" => GetCleanString(tblarss.Lowctrl),
                "hc" => GetCleanString(tblarss.Highctrl),
                _ => string.Empty,
            };
            
        }

        private static string FetchCalRange(Tblarss? tblarss, string alarm)
        {
            if (tblarss is null)
            {
                return DBLoopData.CALERROR.ToString();
            }

            return alarm.ToLower() switch
            {
                "min" => ((int)(tblarss.Mincalibrange ?? DBLoopData.CALERROR)).ToString(),
                "max" => ((int)(tblarss.Maxcalibrange ?? DBLoopData.CALERROR)).ToString(),
                "units" => GetCleanString(tblarss.Calibrangeunits),
                _ => DBLoopData.CALERROR.ToString(),
            };

        }

        private static string GetCleanString(string? inputString) => (inputString ?? string.Empty).Trim();

    }
}
