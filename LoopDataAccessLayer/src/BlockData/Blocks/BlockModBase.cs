using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public abstract class BlockModBase : BlockDataExcelDB
    {
        public BlockModBase(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

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

        protected static string GetSymbolType(string systemType)
        {
            // having this function provides flexibility if things change in the future
            // but as of now, April 1, 2023, the systemType from the DB aligns with the
            // visibility field of the block
            return systemType.ToUpper();
        }

        protected void PopulateLoopFields(DBLoopData data, string attribute1, string attribute2)
        {
            string tag = data.Tag;
            if (data.IsMotorSD)
            {
                tag = Regex.Match(data.Tag, @"^.*?(?=SD|SZD)").Value;
            }
            
            string[] tagComponents = GetTag1Tag2(tag);
            string upper = FixUpperLoopTag(data, tagComponents[0]);
            string lower = tagComponents[1];
            
            if (tagComponents.Length == 2)
            {
                Attributes[attribute1] = upper;
                Attributes[attribute2] = lower;
            }
        }

        private static string FixUpperLoopTag(DBLoopData data, string upper)
        {
            if (string.IsNullOrEmpty(upper))
            {
                return upper;
            }

            return data.IoType.ToUpper() switch
            {
                "DI" => FixDiscrete(upper),
                "AI" => FixAnalog(upper),
                _ => upper
            };
        }

        private static string FixDiscrete(string upper)
        {
            if (Regex.IsMatch(upper, @"\bZS[CO]\b", RegexOptions.IgnoreCase))
            {
                return upper.Replace("s", "I", StringComparison.OrdinalIgnoreCase);
            }

            return upper.Replace("s", "A", StringComparison.OrdinalIgnoreCase);
        }

        private static string FixAnalog(string upper)
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
