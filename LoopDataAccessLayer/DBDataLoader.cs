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
                    Description = d.Controldescription,
                    Manufacturer = d.Manufacturer,
                    Model = d.Model,
                    JB1Tag = d.Jb1tag,
                    JB2Tag = d.Jb2tag,
                    Rack = (d.Rack == null) ? -1 : (int)d.Rack,
                    Slot = (d.Slot == null) ? -1 : (int)d.Slot,
                    Channel = (d.Channel == null) ? -1 : (int)d.Channel,
                    DrawingNumber = d.Newwiringdrawing,
                    MinCalRange = (d.Tblarss == null) ? 0 : (d.Tblarss.Mincalibrange == null) ? 0 : (int)d.Tblarss.Mincalibrange,
                    MaxCalRange = (d.Tblarss == null) ? 0 : (d.Tblarss.Maxcalibrange == null) ? 0 : (int)d.Tblarss.Maxcalibrange,
                    LoLoAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Llalarm,
                    LoAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Loalarm,
                    HiAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Hialarm,
                    HiHiAlarm = (d.Tblarss == null) ? "" : d.Tblarss.Hhalarm,
                    LoControl = (d.Tblarss == null) ? "" : d.Tblarss.Lowctrl,
                    HiControl = (d.Tblarss == null) ? "" : d.Tblarss.Highctrl,
                }).FirstOrDefault();

            return data ?? new DBLoopData();
        }
    }

}
