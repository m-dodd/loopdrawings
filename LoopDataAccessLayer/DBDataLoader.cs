using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using LoopDataAdapterLayer;

namespace LoopDataAccessLayer
{
    
    public class DBDataLoader
    {
        private readonly WTEdgeContext db;
        private readonly Dictionary<string, DBLoopData> loopData;

        public DBDataLoader()
        {
            this.db = new WTEdgeContext();
            loopData = new Dictionary<string, DBLoopData>();
        }
        
        public DBLoopData GetLoopData(string tag)
        {
            /// Memoized version
            ///     First check to see if the data is in the dictionar and if it is simply return it
            ///     If it is not in the dict then fetch it and add it to the dict and return it
            ///     Now any future call for this same data will not need to fetch it
            DBLoopData? data;
            if (loopData.TryGetValue(tag, out data))
            {
                return data;
            }
            else
            {
                data = db.Tblindices.Where(t => t.Tag == tag).Select(
                    d => new DBLoopData
                    {
                        Tag = d.Tag,
                        LoopNo = d.Loopno ?? string.Empty,
                        Description = d.Servicedescription ?? string.Empty,
                        Manufacturer = d.Manufacturer ?? string.Empty,
                        Model = d.Model ?? string.Empty,
                        JB1Tag = d.Jb1tag ?? string.Empty,
                        JB2Tag = d.Jb2tag ?? string.Empty,

                        Rack = ((d.Rack == null) ? -1 : (int)d.Rack).ToString(),
                        Slot = ((d.Slot == null) ? -1 : (int)d.Slot).ToString(),
                        Channel = ((d.Channel == null) ? -1 : (int)d.Channel).ToString(),

                        PidDrawingNumber = d.Pid ?? string.Empty,
                        MinCalRange = ((d.Tblarss == null) ? DBLoopData.CALERROR : (int)(d.Tblarss.Mincalibrange ?? DBLoopData.CALERROR)).ToString(),
                        MaxCalRange = ((d.Tblarss == null) ? DBLoopData.CALERROR : (int)(d.Tblarss.Maxcalibrange ?? DBLoopData.CALERROR)).ToString(),
                        LoLoAlarm = (d.Tblarss == null) ? string.Empty : d.Tblarss.Llalarm ?? string.Empty,
                        LoAlarm = (d.Tblarss == null) ? string.Empty : d.Tblarss.Loalarm ?? string.Empty,
                        HiAlarm = (d.Tblarss == null) ? string.Empty : d.Tblarss.Hialarm ?? string.Empty,
                        HiHiAlarm = (d.Tblarss == null) ? string.Empty : d.Tblarss.Hhalarm ?? string.Empty,
                        LoControl = (d.Tblarss == null) ? string.Empty : d.Tblarss.Lowctrl ?? string.Empty,
                        HiControl = (d.Tblarss == null) ? string.Empty : d.Tblarss.Highctrl ?? string.Empty,
                        FailPosition = d.Failposition ?? string.Empty,

                    }).FirstOrDefault();
                loopData[tag] = data ?? new DBLoopData();

                return loopData[tag];
            }
        }
    }

    public class DBLoopData
    {
        public const int CALERROR = -9999;

        public string Tag { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string JB1Tag { get; set; } = string.Empty;
        public string JB2Tag { get; set; } = string.Empty;
        public string Rack { get; set; } = "-99";
        public string Slot { get; set; } = "-99";
        public string Channel { get; set; } = "-99";
        public string ModTerm1 { get; set; } = string.Empty;
        public string ModTerm2 { get; set; } = string.Empty;
        public string PidDrawingNumber { get; set; } = string.Empty;
        public string MinCalRange { get; set; } = "-99";
        public string MaxCalRange { get; set; } = "-99";
        public string FailPosition { get; set; } = string.Empty;
        public string LoLoAlarm { get; set; } = string.Empty;
        public string LoAlarm { get; set; } = string.Empty;
        public string HiAlarm { get; set; } = string.Empty;
        public string HiHiAlarm { get; set; } = string.Empty;
        public string LoControl { get; set; } = string.Empty;
        public string HiControl { get; set; } = string.Empty;
        public string IoPanel { get; set; } = string.Empty;

        // additional fields that may be useful
        public string LoopNo { get; set; } = string.Empty;
    }
}
