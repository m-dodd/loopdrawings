using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LoopDataAccessLayer.src.DataLoader;
using Org.BouncyCastle.Security;
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
            if (data.IsMotorSD && tag.Length >= 3)
            {
                tag = StripSDFromTag(tag);
            }
            if (data.IsMotorStartStop && tag.Length >= 2)
            {
                tag = tag[..^2];
            }

            string[] tagComponents = ExtractInstrumentIdentifierAndLoopNumber(tag);
            if (tagComponents.Length == 2)
            {
                string upper = FixUpperLoopTag(data, tagComponents[0]);
                string lower = tagComponents[1];
                Attributes[attribute1] = upper;
                Attributes[attribute2] = lower;
            }
        }

        private static string StripSDFromTag(string tag)
        {
            // Define the regex pattern to match and capture anything before "SZD" or "SD"
            // also ensures it is in teh proper form for a tag of XXX-NNNSZD and allows for it to end in A or B
            string pattern = @"^(.*?-.*?)(?:SZD|SD)";

            // Use Regex.Match to find the match
            Match match = Regex.Match(tag, pattern);

            // return the preceeding characters - this handles cases wheere there is an A or a B at the end
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return tag;
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
            TagComponents tagComponents = TagComponents.ParseTagIdentifer(upper);

            // Check for specific cases and return the appropriate result
            if (tagComponents.MeasureVariable == "Z")
            {
                return upper.Replace("S", "I", StringComparison.OrdinalIgnoreCase);
            }
            else if (tagComponents.MeasureVariable == "H")
            {
                return "HHS";
            }
            else
            {
                // Default: replace 's' with 'A' (e.g., LS -> LA, FS -> FA)
                return upper.Replace("S", "A", StringComparison.OrdinalIgnoreCase);
            }
        }

        private static string FixAnalog(string upper)
        {
            TagComponents tagComponents = TagComponents.ParseTagIdentifer(upper);
            return tagComponents.MeasureVariable.ToUpper() + "I";
        }

    }
}
