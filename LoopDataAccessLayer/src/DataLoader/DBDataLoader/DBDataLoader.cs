﻿
using System.Text.RegularExpressions;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
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
                "F-1521",
                "F-1914B",

                // AIN tests
                "L-7100",
                "P-1102",
                "A-1100C",

                // PID tests
                "L-1400",

                // XV tests
                "X-1300",

                // Motor Test
                "CM-301",

                // DI
                "X-0018",

                // DO-RELAY
                "X-9001",

                // AI x2
                "P-1533",
            };
            return db.Tblloops
                      // it's possible that I will want to gracefully handle Looptemplate == null in the future
                      // I'm thinking of a log file message or something, but for now, I don't want it
                      .Where(x => (x.Loopno != "---") && (x.Loopno != null) && (x.Looptemplate != null))
                      .Where(x => currentTestingLoops.Contains(x.Loopno)) // just to limit the results for testing purposes
                      .Select(loop => new LoopNoTemplatePair
                                      {
                                          LoopNo = loop.Loopno ?? string.Empty,
                                          Template = loop.Looptemplate ?? string.Empty,
                                          Description = loop.Loopdescription ?? string.Empty,
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
                         System = GetCleanString(tag.System),
                         SystemType = (tag.Tblsystem == null) ? string.Empty : GetCleanString(tag.Tblsystem.SystemType),
                     })
                     .ToList();
        }
        
        public DBLoopData GetLoopData(string tag)
        {
            /// Memoized version
            ///     First check to see if the data is in the dictionar and if it is simply return it
            ///     If it is not in the dict then fetch it and add it to the dict and return it
            ///     Now any future call for this same data will not need to fetch it
            if (loopDataCache.TryGetValue(tag, out DBLoopData? data))
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
                if (data is not null)
                {
                    if (data.IsMotorSD)
                    {
                        UpdateMotorSDAlarms(data);
                    }
                    loopDataCache[tag] = data;
                }
                else
                {
                    loopDataCache[tag] = new DBLoopData();
                }

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
                "ll" => BuildAlarmString("LL", GetCleanString(tblarss.Llalarm)),
                "l" => BuildAlarmString("L", GetCleanString(tblarss.Loalarm)),
                "h" => BuildAlarmString("H", GetCleanString(tblarss.Hialarm)),
                "hh" => BuildAlarmString("HH", GetCleanString(tblarss.Hhalarm)),
                "lc" => BuildAlarmString("LL", GetCleanString(tblarss.Lowctrl)),
                "hc" => BuildAlarmString("LL", GetCleanString(tblarss.Highctrl)),
                _ => string.Empty,
            };
        }

        private static string BuildAlarmString(string prefix, string value)
        {
            return IDBLoopData.IsValidDatabaseString(value) ? prefix + "=" + value : string.Empty;
        }

        private static void UpdateMotorSDAlarms(DBLoopData data)
        {
            Match match = Regex.Match(data.Tag, @"(SD|SZD).*$");
            if (match.Success)
            {
                data.LoAlarm = match.Value.ToUpper();
            }
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
