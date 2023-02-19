
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    public class DBDataLoader : IDBLoader
    {
        private readonly WTEdgeContext db;
        private readonly Dictionary<string, DBLoopData> loopData;

        public DBDataLoader()
        {
            this.db = new WTEdgeContext();
            loopData = new Dictionary<string, DBLoopData>();
        }

        public List<LoopNoTemplatePair> GetLoops()
        {
            return db.Tblloops
                      // it's possible that I will want to gracefully handle Looptemplate == null in the future
                      // I'm thinking of a log file message or something, but for now, I don't want it
                      .Where(x => (x.Loop != "---") && (x.Loop != null) && (x.Looptemplate != null))
                      .Select(loop => new LoopNoTemplatePair
                                      {
                                          LoopNo = loop.Loop ?? string.Empty,
                                          Template = loop.Looptemplate ?? string.Empty,
                                      })
                      .ToList();
        }

        public List<LoopTagData> GetLoopTags(LoopNoTemplatePair loop)
        {
            string[] badStatus = { "OUT OF SCOPE", "DELETE", "HOLD" };
            return db.Tblindices
                     .Where(t => (t.Loopno == loop.LoopNo) && !badStatus.Contains(t.Status))
                     .Select(tag => new LoopTagData
                     {
                         Tag = tag.Tag ?? string.Empty,
                         IOType = tag.Iotype ?? string.Empty,
                         InstrumentType = tag.Instrumenttype ?? string.Empty,
                     })
                     .ToList();
        }
        
        public DBLoopData GetLoopData(string tag)
        {
            /// Memoized version
            ///     First check to see if the data is in the dictionar and if it is simply return it
            ///     If it is not in the dict then fetch it and add it to the dict and return it
            ///     Now any future call for this same data will not need to fetch it
            if (loopData.TryGetValue(tag, out var data))
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
                        Manufacturer = GetCleanString(d.Manufacturer),
                        Model = GetCleanString(d.Model),
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

                    }).FirstOrDefault();
                loopData[tag] = data ?? new DBLoopData();

                return loopData[tag];
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
                "hc" => GetCleanString(tblarss.Lowctrl),
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

        //private string GetFailPosition(string? failPosition)
        //{
        //    string[] failPositionsOK = new string[] {"FC", "FO", "FL"};
        //    if(!string.IsNullOrEmpty(failPosition))
        //    {
        //        if (failPosition.ToUpper() == "CLOSED")
        //        {
        //            return "FC";
        //        }
        //        if (failPosition.ToUpper() == "OPEN")
        //        {
        //            return "FO";
        //        }
        //        if (failPositionsOK.Contains(failPosition))
        //        {
        //            return failPosition;
        //        }
        //    }
        //    return string.Empty;
        //}
    }
}
