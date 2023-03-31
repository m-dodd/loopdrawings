using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        protected void PopulateLoopFields(DBLoopData data, string attribute1, string attribute2)
        {
            string[] tagComponents = GetTag1Tag2(data.Tag);
            string upper = FixUpperLoopTag(data, tagComponents[0]);
            if (tagComponents.Length == 2)
            {
                Attributes[attribute1] = upper;
                Attributes[attribute2] = tagComponents[1];
            }
        }

        private string FixUpperLoopTag(DBLoopData data, string upper)
        {
            if (string.IsNullOrEmpty(upper))
            {
                return upper;
            }

            upper = data.IoType.Equals("DI", StringComparison.OrdinalIgnoreCase)
                ? FixDiscrete(upper)
                : data.IoType.Equals("AI", StringComparison.OrdinalIgnoreCase)
                    ? FixAnalog(upper)
                    : upper;

            return upper;
        }

        private string FixDiscrete(string upper)
        {
            if (Regex.IsMatch(upper, @"\bZS[CO]\b", RegexOptions.IgnoreCase))
            {
                return upper.Replace("s", "I", StringComparison.OrdinalIgnoreCase);
            }

            return upper.Replace("s", "A", StringComparison.OrdinalIgnoreCase);
        }

        private string FixAnalog(string upper)
        {
            if (!upper.EndsWith("T", StringComparison.OrdinalIgnoreCase))
            {
                return upper;
            }

            return upper.EndsWith("IT", StringComparison.OrdinalIgnoreCase)
                ? upper.Substring(0, upper.Length - 1)
                : upper + "I";
        }

    }
}
