
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
                      //.AsEnumerable();
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
                        LoopNo = (d.Loopno ?? string.Empty).Trim(),
                        Description = (d.Servicedescription ?? string.Empty).Trim(),
                        Manufacturer = (d.Manufacturer ?? string.Empty).Trim(),
                        Model = (d.Model ?? string.Empty).Trim(),
                        JB1Tag = (d.Jb1tag ?? string.Empty).Trim(),
                        JB2Tag = (d.Jb2tag ?? string.Empty).Trim(),

                        Rack = ((d.Rack == null) ? -1 : (int)d.Rack).ToString().Trim(),
                        Slot = ((d.Slot == null) ? -1 : (int)d.Slot).ToString().Trim(),
                        Channel = ((d.Channel == null) ? -1 : (int)d.Channel).ToString(),

                        PidDrawingNumber = (d.Pid ?? string.Empty).Trim(),

                        MinCalRange = ((d.Tblarss == null) ? DBLoopData.CALERROR : (int)(d.Tblarss.Mincalibrange ?? DBLoopData.CALERROR)).ToString().Trim(),
                        MaxCalRange = ((d.Tblarss == null) ? DBLoopData.CALERROR : (int)(d.Tblarss.Maxcalibrange ?? DBLoopData.CALERROR)).ToString().Trim(),
                        LoLoAlarm = ((d.Tblarss == null) ? string.Empty : d.Tblarss.Llalarm ?? string.Empty).Trim(),
                        LoAlarm = ((d.Tblarss == null) ? string.Empty : d.Tblarss.Loalarm ?? string.Empty).Trim(),
                        HiAlarm = ((d.Tblarss == null) ? string.Empty : d.Tblarss.Hialarm ?? string.Empty).Trim(),
                        HiHiAlarm = ((d.Tblarss == null) ? string.Empty : d.Tblarss.Hhalarm ?? string.Empty).Trim(),
                        LoControl = ((d.Tblarss == null) ? string.Empty : d.Tblarss.Lowctrl ?? string.Empty).Trim(),
                        HiControl = ((d.Tblarss == null) ? string.Empty : d.Tblarss.Highctrl ?? string.Empty).Trim(),
                        FailPosition = d.Failposition,

                    }).FirstOrDefault();
                loopData[tag] = data ?? new DBLoopData();

                return loopData[tag];
            }
        }

        private string GetFailPosition(string? failPosition)
        {
            string[] failPositionsOK = new string[] {"FC", "FO", "FL"};
            if(!string.IsNullOrEmpty(failPosition))
            {
                if (failPosition.ToUpper() == "CLOSED")
                {
                    return "FC";
                }
                if (failPosition.ToUpper() == "OPEN")
                {
                    return "FO";
                }
                if (failPositionsOK.Contains(failPosition))
                {
                    return failPosition;
                }
            }
            return string.Empty;
        }
    }
}
