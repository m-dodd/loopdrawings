using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockModBase : BlockDataExcelDB
    {
        public BlockModBase(IDataLoader dataLoader) : base(dataLoader) { }

        protected void PopulateAlarms(IDBLoopData data)
        {
            Attributes["ALARM1"] = data.HiHiAlarm;
            Attributes["ALARM2"] = data.HiAlarm;
            Attributes["ALARM3"] = data.HiControl;
            Attributes["ALARM4"] = data.LoControl;
            Attributes["ALARM5"] = data.LoAlarm;
            Attributes["ALARM6"] = data.LoLoAlarm;
        }

        protected void PopulateFourAlarms(IDBLoopData data, string alarmSuffix)
        {
            // discrete alarms? for zsc / zso? what format will they take
            // not really built into the data structure atm
            Attributes["ALARM1" + alarmSuffix] = string.Empty;

            Attributes["ALARM2" + alarmSuffix] = data.HiControl;
            Attributes["ALARM3" + alarmSuffix] = data.LoControl;
            
            Attributes["ALARM4" + alarmSuffix] = string.Empty;
        }

        protected void PopulateRackSlotChannel(IDBLoopData data)
        {
            PopulateRackSlotChannel(data, "");
        }

        protected void PopulateRackSlotChannel(IDBLoopData data, string suffix)
        {
            Attributes["RACK" + suffix] = data.Rack;
            Attributes["SLOT" + suffix] = data.Slot;
            Attributes["CHANNEL" + suffix] = data.Channel;
        }
    }
}
