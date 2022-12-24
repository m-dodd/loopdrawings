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

        public DBDataLoader()
        {
            this.db = new WTEdgeContext();
        }
        
        public DBLoopData GetLoopData(string tag)
        {
            DBLoopData? data = db.Tblindices.Where(t => t.Tag == tag).Select(
                d => new DBLoopData
                {
                    Tag = d.Tag,
                    Description = d.Controldescription ?? String.Empty,
                    Manufacturer = d.Manufacturer ?? String.Empty,
                    Model = d.Model ?? String.Empty,
                    JB1Tag = d.Jb1tag ?? String.Empty,
                    JB2Tag = d.Jb2tag ?? String.Empty,
                    Rack = (d.Rack == null) ? -1 : (int)d.Rack,
                    Slot = (d.Slot == null) ? -1 : (int)d.Slot,
                    Channel = (d.Channel == null) ? -1 : (int)d.Channel,
                    DrawingNumber = d.Newwiringdrawing ?? String.Empty,
                    MinCalRange = (d.Tblarss == null) ? 0 : (int)(d.Tblarss.Mincalibrange ?? 0),
                    MaxCalRange = (d.Tblarss == null) ? 0 : (int)(d.Tblarss.Maxcalibrange ?? 0),
                    LoLoAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Llalarm ?? String.Empty,
                    LoAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Loalarm ?? String.Empty,
                    HiAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Hialarm ?? String.Empty,
                    HiHiAlarm = (d.Tblarss == null) ? String.Empty : d.Tblarss.Hhalarm ?? String.Empty,
                    LoControl = (d.Tblarss == null) ? String.Empty : d.Tblarss.Lowctrl ?? String.Empty,
                    HiControl = (d.Tblarss == null) ? String.Empty : d.Tblarss.Highctrl ?? String.Empty,
                }).FirstOrDefault();

            return data ?? new DBLoopData();
        }
    }

    public class DBLoopData
    {
        public string Tag { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Manufacturer { get; set; } = String.Empty;
        public string Model { get; set; } = String.Empty;
        public string JB1Tag { get; set; } = String.Empty;
        public string JB2Tag { get; set; } = String.Empty;
        public int Rack { get; set; } = -99;
        public int Slot { get; set; } = -99;
        public int Channel { get; set; } = -99;
        public string DrawingNumber { get; set; } = String.Empty;
        public decimal MinCalRange { get; set; } = -99;
        public decimal MaxCalRange { get; set; } = -99;
        public string LoLoAlarm { get; set; } = String.Empty;
        public string LoAlarm { get; set; } = String.Empty;
        public string HiAlarm { get; set; } = String.Empty;
        public string HiHiAlarm { get; set; } = String.Empty;
        public string LoControl { get; set; } = String.Empty;
        public string HiControl { get; set; } = String.Empty;

        public Dictionary<string, string> ToDict()
        {
            return new Dictionary<string, string>
            {
                { "TAG_01", Tag },
                { "DESCRIPTION_01", Description },
                { "MANUFACTURER_01", Manufacturer },
                { "MODEL_01", Model },
                { "JB_TAG_01", JB1Tag },
                { "JB_TAG_02", JB2Tag },
                { "RACK_01", Rack.ToString() },
                { "SLOT_01", Slot.ToString() },
                { "CHANNEL_01", Channel.ToString() },
                { "DRAWING_NO_01", DrawingNumber },
                { "MinCalRange", MinCalRange.ToString() },
                { "MaxCalRange", MaxCalRange.ToString() },
                { "ALARM_01", HiControl },
                { "ALARM_02", HiHiAlarm },
                { "ALARM_03", HiAlarm },
                { "ALARM_04", LoAlarm },
                { "ALARM_05", LoLoAlarm },
                { "ALARM_06", LoControl },
            };
        }

        public override string ToString()
        {
            return string.Join(System.Environment.NewLine, ToDict().Select(x => x.Key + ": " + x.Value?.ToString()));
        }
    }
}
