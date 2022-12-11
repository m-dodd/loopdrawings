using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAcessObjects;

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
                    LoLoAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Llalarm ?? String.Empty,
                    LoAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Loalarm ?? String.Empty,
                    HiAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Hialarm ?? String.Empty,
                    HiHiAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Hhalarm ?? String.Empty,
                    LoControl = (d.Tblarss == null) ? "" : d.Tblarss.Lowctrl ?? String.Empty,
                    HiControl = (d.Tblarss == null) ? "" : d.Tblarss.Highctrl ?? String.Empty,
                }).FirstOrDefault();

            return data ?? new DBLoopData();
        }
    }

    public class DBLoopData
    {
        public string Tag { get; set; } = String.Empty;
        //{ 
        //    get { return Tag; }
        //    set { Tag = value ?? String.Empty; }
        //}
        public string Description { get; set; } = String.Empty;
        //{
        //    get { return Description; }
        //    set { Description = value ?? String.Empty; }
        //}
        public string Manufacturer { get; set; } = String.Empty;
        //{
        //    get { return Manufacturer; }
        //    set { Manufacturer = value ?? String.Empty; }
        //}
        public string Model { get; set; } = "";
        public string JB1Tag { get; set; } = "";
        public string JB2Tag { get; set; } = "";
        public int Rack { get; set; } = -99;
        public int Slot { get; set; } = -99;
        public int Channel { get; set; } = -99;
        public string DrawingNumber { get; set; } = "";
        public decimal MinCalRange { get; set; } = -99;
        public decimal MaxCalRange { get; set; } = -99;
        public string LoLoAlarm { get; set; } = "";
        public string LoAlarm { get; set; } = "";
        public string HiAlarm { get; set; } = "";
        public string HiHiAlarm { get; set; } = "";
        public string LoControl { get; set; } = "";
        public string HiControl { get; set; } = "";

        public Dictionary<string, string> ToDict()
        {
            return new Dictionary<string, string>
            {
                { "Tag", Tag },
                { "Description", Description },
                { "Manufacturer", Manufacturer },
                { "Model", Model },
                { "JB1Tag", JB1Tag },
                { "JB2Tag", JB2Tag },
                { "Rack", Rack.ToString() },
                { "Slot", Slot.ToString() },
                { "Channel", Channel.ToString() },
                { "DrawingNumber", DrawingNumber },
                { "MinCalRange", MinCalRange.ToString() },
                { "MaxCalRange", MaxCalRange.ToString() },
                { "LoLoAlarm", LoLoAlarm },
                { "LoAlarm", LoAlarm },
                { "HiAlarm", HiAlarm },
                { "HiHiAlarm", HiHiAlarm },
                { "LoControl", LoControl },
                { "HiControl", HiControl },
            };
        }

        public override string ToString()
        {
            return string.Join(System.Environment.NewLine, ToDict().Select(x => x.Key + ": " + x.Value?.ToString()));
        }
    }
}
