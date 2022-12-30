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
                        Description = d.Controldescription ?? String.Empty,
                        Manufacturer = d.Manufacturer ?? String.Empty,
                        Model = d.Model ?? String.Empty,
                        JB1Tag = d.Jb1tag ?? String.Empty,
                        JB2Tag = d.Jb2tag ?? String.Empty,

                        Rack = ((d.Rack == null) ? -1 : (int)d.Rack).ToString(),
                        Slot = ((d.Slot == null) ? -1 : (int)d.Slot).ToString(),
                        Channel = ((d.Channel == null) ? -1 : (int)d.Channel).ToString(),
                        ModTerm = ((d.Port == null) ? -1 : (int)d.Port).ToString(),

                        DrawingNumber = d.Newwiringdrawing ?? String.Empty,
                        MinCalRange = ((d.Tblarss == null) ? DBLoopData.CALERROR : (int)(d.Tblarss.Mincalibrange ?? DBLoopData.CALERROR)).ToString(),
                        MaxCalRange = ((d.Tblarss == null) ? DBLoopData.CALERROR : (int)(d.Tblarss.Maxcalibrange ?? DBLoopData.CALERROR)).ToString(),
                        LoLoAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Llalarm ?? String.Empty,
                        LoAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Loalarm ?? String.Empty,
                        HiAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Hialarm ?? String.Empty,
                        HiHiAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Hhalarm ?? String.Empty,
                        LoControl = (d.Tblarss == null) ? String.Empty : d.Tblarss.Lowctrl ?? String.Empty,
                        HiControl = (d.Tblarss == null) ? String.Empty : d.Tblarss.Highctrl ?? String.Empty,

                    }).FirstOrDefault();
                loopData[tag] = data ?? new DBLoopData();

                return loopData[tag];
            }
        }
    }

    public class DBLoopData
    {
        public const int CALERROR = -9999;

        public string Tag { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Manufacturer { get; set; } = String.Empty;
        public string Model { get; set; } = String.Empty;
        public string JB1Tag { get; set; } = String.Empty;
        public string JB2Tag { get; set; } = String.Empty;
        public string Rack { get; set; } = "-99";
        public string Slot { get; set; } = "-99";
        public string Channel { get; set; } = "-99";
        public string ModTerm { get; set; } = String.Empty;
        public string DrawingNumber { get; set; } = String.Empty;
        public string MinCalRange { get; set; } = "-99";
        public string MaxCalRange { get; set; } = "-99";
        public string LoLoAlarm { get; set; } = String.Empty;
        public string LoAlarm { get; set; } = String.Empty;
        public string HiAlarm { get; set; } = String.Empty;
        public string HiHiAlarm { get; set; } = String.Empty;
        public string LoControl { get; set; } = String.Empty;
        public string HiControl { get; set; } = String.Empty;
    }
}
