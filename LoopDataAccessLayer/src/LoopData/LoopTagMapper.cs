using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class LoopTagMapper
    {
        private static string[] definedTagTypes = {
            "AI",
            "AO",
            "DI",
            "DO",
            "CONTROLLER",
            "VALVE",
            "ZSC",
            "ZSO",
            "SOL-BPCS",
            "SOL-SIS",
            "MOTOR-SD-BPCS",
            "MOTOR-SD-SIS",
            "MOTOR-SD-SIS-A",
            "MOTOR-SD-SIS-B",
        };

        public static Dictionary<string, string> BuildTagMap(
            IEnumerable<LoopTagData> tags, TemplateConfig templateConfig
        )
        {
            Dictionary<string, string> tagMap = new();

            foreach(string tagtype in GetTagTypes(templateConfig.BlockMap))
            {
                if (definedTagTypes.Contains( tagtype ))
                {
                    string tagName = GetTag(tags, tagtype);
                    if (!string.IsNullOrEmpty(tagName))
                    {
                        tagMap[tagtype] = tagName;
                    }
                }
            }
            
            return tagMap;
        }

        private static HashSet<string> GetTagTypes(List<BlockMapData> BlockMap)
        {
            HashSet<string> uniqueTags = new HashSet<string>();
            foreach (BlockMapData block in BlockMap)
            {
                foreach(string tag in block.Tags)
                {
                    uniqueTags.Add(tag);
                }
            }

            return uniqueTags;
        }

        private static string GetTag(IEnumerable<LoopTagData> tags, string tagtype)
        {
            LoopTagData? tag = tags.FirstOrDefault(t =>
                tagtype switch
                {
                    "AI" => IsIOType(t, "AI"),
                    "AO" => IsIOType(t, "AO"),
                    "DI" => IsIOType(t, "DI"),
                    "DO" => IsIOType(t, "DO"),
                    "CONTROLLER" => IsIOType(t, "SOFT")
                                    && t.Tag.Contains("IC", StringComparison.OrdinalIgnoreCase),
                    "VALVE" => (string.IsNullOrEmpty(t.IOType) || t.IOType == "---")
                                && Regex.IsMatch(t.InstrumentType, @"ball|gate|globe|butterfly", RegexOptions.IgnoreCase),
                    "ZSC" => IsIOType(t, "DI") 
                             && t.Tag.Contains("ZSC", StringComparison.OrdinalIgnoreCase),
                    "ZSO" => IsIOType(t, "DI")
                             && t.Tag.Contains("ZSO", StringComparison.OrdinalIgnoreCase),
                    "SOL-BPCS" => IsIOInstrumentSystem(t, "DO", "SOLENOID", "BPCS"),
                    "SOL-SIS" => IsIOInstrumentSystem(t, "DO", "SOLENOID", "SIS"),
                    "MOTOR-SD-BPCS" => IsIOInstrumentSystem(t, "DO", "MOTOR_SD", "BPCS"),
                    "MOTOR-SD-SIS" => IsIOInstrumentSystem(t, "DO", "MOTOR_SD", "SIS"),
                    "MOTOR-SD-SIS-A" => IsIOInstrumentSystem(t, "DO", "MOTOR_SD", "SIS")
                                        && EndsWithIgnoreCase(t, "A"),
                    "MOTOR-SD-SIS-B" => IsIOInstrumentSystem(t, "DO", "MOTOR_SD", "SIS")
                                        && EndsWithIgnoreCase(t, "B"),
                    _ => false
                });

            return tag?.Tag ?? string.Empty;
        }
        
        private static bool IsIOType(LoopTagData tag, string ioType)
        {
            return tag.IOType.EqualsCaseInsensitive(ioType);
        }

        private static bool IsSystemType(LoopTagData tag, string systemType)
        {
            return tag.SystemType.EqualsCaseInsensitive(systemType);
        }

        private static bool IsInstrumentType(LoopTagData tag, string instrumentType)
        {
            return tag.InstrumentType.Contains(instrumentType, StringComparison.OrdinalIgnoreCase);
        }

        private static bool EndsWithIgnoreCase(LoopTagData tag, string suffix)
        {
            return tag.Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsIOInstrumentSystem(LoopTagData tag, string ioType, string instrumentType, string systemType)
        {
            return IsIOType(tag, ioType) && IsInstrumentType(tag, instrumentType) && IsSystemType(tag, systemType);
        }

    }

    public static class StringExtensions
    {
        public static bool EqualsCaseInsensitive(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
