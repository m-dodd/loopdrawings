
using DocumentFormat.OpenXml.Office2019.Presentation;
using System.Text.RegularExpressions;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    public interface IDBLoader
    {
        List<LoopNoTemplatePair> GetLoops();
        IEnumerable<LoopTagData> GetLoopTags(LoopNoTemplatePair loop);
        IEnumerable<LoopTagData> GetLoopTags(string loopNo);
        DBLoopData GetLoopTagData(string tag);
        List<SDKData> GetSDsForTag(string tag);
        List<SDKData> GetSDsForLoop(string loopNo);
    }


    public class DBDataLoader : IDBLoader
    {
        private readonly WTEdgeContext db;
        private readonly Dictionary<string, DBLoopData> loopDataCache;
        private readonly Dictionary<string, List<SDKData>> sdkTagDataCache;
        private readonly Dictionary<string, List<SDKData>> sdkLoopDataCache;
        private readonly Dictionary<string, List<LoopTagData>> loopTagsCache;


        public DBDataLoader()
        {
            this.db = new WTEdgeContext();
            loopDataCache = new Dictionary<string, DBLoopData>();
            sdkTagDataCache = new Dictionary<string, List<SDKData>>();
            sdkLoopDataCache = new Dictionary<string, List<SDKData>>();
            loopTagsCache = new Dictionary<string, List<LoopTagData>>();
        }

        #region DataLoaderMethods
        public List<SDKData> GetSDsForTag(string tag)
        {
            if (sdkTagDataCache.TryGetValue(tag, out var data))
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
                                                    GetCleanString(sd.OutputtagNavigation.Controldescription),
                        SdGroup = GetCleanString(sd.Sdgroup),
                        SdAction1 = GetCleanString(sd.Sdaction1),
                        SdAction2 = GetCleanString(sd.Sdaction2)
                    })
                    .ToList();
                sdkTagDataCache[tag] = data;
                return data;
            }
        }

        public List<SDKData> GetSDsForTags(IEnumerable<string> tags)
        {
            // Filter the tags that are not already in the cache
            var tagsToQuery = tags.Where(tag => !sdkTagDataCache.ContainsKey(tag)).ToList();

            // Fetch data from the database for tags that are not in the cache and update cache
            if (tagsToQuery.Count > 0)
            {
                // Fetch data from the database for tags that are not in the cache
                var x = db.Tblsdkrelations
                    .Where(sd => tagsToQuery.Contains(sd.Parenttags!)).ToList();
                var dbDataDictionary = db.Tblsdkrelations
                    .Where(sd => tagsToQuery.Contains(sd.Parenttags!))
                    .Select(sd => new SDKData
                    {
                        ParentTag = GetCleanString(sd.Parenttags),
                        InputTag = GetCleanString(sd.Inputtags),
                        OutputTag = GetCleanString(sd.Outputtag),
                        OutputDescription = sd.OutputtagNavigation == null ?
                                                    string.Empty :
                                                    GetCleanString(sd.OutputtagNavigation.Controldescription),
                        SdGroup = GetCleanString(sd.Sdgroup),
                        SdAction1 = GetCleanString(sd.Sdaction1),
                        SdAction2 = GetCleanString(sd.Sdaction2)
                    })
                    .AsEnumerable() // Switch to LINQ to Objects
                    .GroupBy(sd => sd.ParentTag)
                    .ToDictionary(group => group.Key, group => group.ToList());

                // Update the sdkTagDataCache with the fetched data or empty lists for missing tags
                foreach (var tag in tagsToQuery)
                {
                    // Try to get data from dbDataDictionary or use an empty list if not found
                    sdkTagDataCache[tag] = dbDataDictionary.TryGetValue(tag, out var data) ? data : new List<SDKData>();
                }
            }

            // Retrieve SD data from the updated cache for the list of tags and convert it into a list of SDKData
            var sdkDataList = tags
                .Where(tag => sdkTagDataCache.ContainsKey(tag))
                .SelectMany(tag => sdkTagDataCache[tag])
                .ToList();

            return sdkDataList;
        }

        public List<SDKData> GetSDsForLoop(string loopNo)
        {
            if (sdkLoopDataCache.TryGetValue(loopNo, out var data))
            {
                return data;
            }
            else
            {
                IEnumerable<LoopTagData> loopTags = GetLoopTags(loopNo);
                var tags = loopTags.Select(t => t.Tag);
                sdkLoopDataCache[loopNo] = data = GetSDsForTags(tags);
                return data;
            }
        }

        public List<LoopNoTemplatePair> GetLoops()
        {
            string[] currentTestingLoops =
            {
                //// DIN-4W tests
                //"F-1521",
                //"F-1914B",

                //// AIN tests
                //"L-7100",

                //"A-1100C",

                //// PID tests
                //"L-1400",

                // XV tests -2xy
                //"X-1300",

                //// SIS Motor Test
                //"CM-301",

                //// BPCS Motor Test
                //"PM-103",

                // //DI
                //"X-0018",

                //// DO-RELAY
                //"X-9001",

                //// AI x2
                //"P-1533",

                //// funky double template
                //"S-1220",
                
                //// Double DI
                //"H-1540",
                //"Z-1211",

                //// Double DI SIS
                //"E-1538",

                //// Double DO RLY
                //"X-9001",
                //"X-1801AB",

                //// XV-1XY
                //"X-1501",

                // DEBUG TESTING
                //"H-1800",
                //"E-9000",
                "E-9001",
                //"E-9002",
                //"A-1100F",
                //"X-1210",
                //"X-1005",
                //"X-1220",
                //"V-1401A",
                //"V-1401B",
            };
            string[] excludedTemplates = { "---", "MANUAL", "DOUTX3_2W_RLY", "DINX3_2W" };
            return db.Tblloops
                      // it's possible that I will want to gracefully handle Looptemplate == null in the future
                      // I'm thinking of a log file message or something, but for now, I don't want it
                      .Where(x => x.Loopno != "---"
                                  && x.Loopno != null
                                  && x.Looptemplate != null
                                  && !excludedTemplates.Contains(x.Looptemplate.ToUpper()))
                      //.Where(x => currentTestingLoops.Contains(x.Loopno)) // just to limit the results for testing purposes
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
            return GetLoopTags(loop.LoopNo);
        }

        public IEnumerable<LoopTagData> GetLoopTags(string loopNo)
        {
            string[] badStatus = { "OUT OF SCOPE", "DELETE", "HOLD" };
            if (loopTagsCache.TryGetValue(loopNo, out List<LoopTagData>? data))
            {
                return data;
            }
            else
            {
                data = db.Tblindices
                     .Where(t => (t.Loopno == loopNo) && !badStatus.Contains(t.Status))
                     .Select(tag => new LoopTagData
                     {
                         Tag = GetCleanString(tag.Tag),
                         IOType = GetCleanString(tag.Iotype),
                         InstrumentType = GetCleanString(tag.Instrumenttype),
                         System = GetCleanString(tag.System),
                         SystemType = (tag.Tblsystem == null) ? string.Empty : GetCleanString(tag.Tblsystem.SystemType),
                     })
                     .ToList();

                return loopTagsCache[loopNo] = data;
            }
        }

        public DBLoopData GetLoopTagData(string tag)
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
                data = db.Tblindices
                    .Where(t => t.Tag == tag)
                    .Select( d => new DBLoopData
                    {
                        Tag = d.Tag,
                        LoopNo = GetCleanString(d.Loopno),
                        Description = GetCleanString(d.Controldescription),

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
                        SignalLevel = GetCleanString(d.Signallevel),

                        System = GetCleanString(d.System),
                        SystemType = (d.Tblsystem == null) ? string.Empty : GetCleanString(d.Tblsystem.SystemType)
                    })
                    .FirstOrDefault();

                if (data is not null && data.IsMotorSD)
                {
                    UpdateMotorSDAlarms(data);
                }
                loopDataCache[tag] = data ?? new DBLoopData();

                return loopDataCache[tag];
            }
        }

        public IDictionary<string, DBLoopData> GetLoopTagsData(IEnumerable<string> tags)
        {
            /// Memoized version
            ///     First check to see if the data is in the dictionar and if it is simply return it
            ///     If it is not in the dict then fetch it and add it to the dict and return it
            ///     Now any future call for this same data will not need to fetch it
            ///     
            /// NOT USED, but could be used to pre-fetch all of the tags for a loop and cache them

            // Step 1: Initialize a dictionary to store the results
            var resultDict = new Dictionary<string, DBLoopData>();

            // Step 2: Determine which tags need to be fetched from the database
            var tagsToFetch = tags.Except(loopDataCache.Keys).ToList();

            // Step 3: Fetch the missing data from the database and update the cache
            if (tagsToFetch.Any())
            {
                var dataDict = db.Tblindices
                    .Where(t => tagsToFetch.Contains(t.Tag))
                    .ToDictionary(
                        t => t.Tag,
                        d => new DBLoopData
                        {
                            Tag = d.Tag,
                            LoopNo = GetCleanString(d.Loopno),
                            Description = GetCleanString(d.Controldescription),

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
                            SignalLevel = GetCleanString(d.Signallevel),

                            System = GetCleanString(d.System),
                            SystemType = (d.Tblsystem == null) ? string.Empty : GetCleanString(d.Tblsystem.SystemType)
                        });

                foreach (var (tag, d) in dataDict)
                {
                    if (d is not null && d.IsMotorSD)
                    {
                        UpdateMotorSDAlarms(d);
                    }
                    loopDataCache[tag] = d ?? new DBLoopData();
                    resultDict[tag] = d ?? new DBLoopData();
                }
            }


            // Step 4: Populate resultDict with cached data
            foreach (var tag in tags.Except(tagsToFetch))
            {
                resultDict[tag] = loopDataCache[tag];
            }

            return resultDict;

        }
        #endregion DataLoaderMethods

        #region PrivateHelperMethods
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
        #endregion PrivateHelperMethods
    }
}
