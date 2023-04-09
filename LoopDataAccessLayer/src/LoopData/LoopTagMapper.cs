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
                    "AI" => t.IOType == "AI",
                    "AO" => t.IOType == "AO",
                    "DI" => t.IOType == "DI",
                    "CONTROLLER" => t.IOType == "SOFT" && t.Tag.Contains("IC", StringComparison.OrdinalIgnoreCase),
                    "VALVE" => (string.IsNullOrEmpty(t.IOType) || t.IOType == "---")
                                && Regex.IsMatch(t.InstrumentType, @"ball|gate|globe|butterfly", RegexOptions.IgnoreCase),
                    "ZSC" => t.IOType == "DI" && t.Tag.Contains("ZSC", StringComparison.OrdinalIgnoreCase),
                    "ZSO" => t.IOType == "DI" && t.Tag.Contains("ZSO", StringComparison.OrdinalIgnoreCase),
                    "SOL-BPCS" => t.IOType == "DO"
                                    && t.InstrumentType.Contains("SOLENOID", StringComparison.OrdinalIgnoreCase)
                                    && t.SystemType.ToUpper() == "BPCS",
                    "SOL-SIS" => t.IOType == "DO"
                                    && t.InstrumentType.Contains("SOLENOID", StringComparison.OrdinalIgnoreCase)
                                    && t.SystemType.ToUpper() == "SIS",
                    "MOTOR-SD-BPCS" => t.IOType == "DO"
                                        && t.InstrumentType.ToUpper() == "MOTOR_SD"
                                        && t.SystemType.ToUpper() == "BPCS",
                    "MOTOR-SD-SIS" => t.IOType == "DO"
                                        && t.InstrumentType.ToUpper() == "MOTOR_SD"
                                        && t.SystemType.ToUpper() == "SIS",
                    "MOTOR-SD-SIS-A" => t.IOType == "DO"
                                        && t.InstrumentType.ToUpper() == "MOTOR_SD"
                                        && t.SystemType.ToUpper() == "SIS"
                                        && t.Tag.ToUpper().EndsWith("A"),
                    "MOTOR-SD-SIS-B" => t.IOType == "DO"
                                        && t.InstrumentType.ToUpper() == "MOTOR_SD"
                                        && t.SystemType.ToUpper() == "SIS"
                                        && t.Tag.ToUpper().EndsWith("B"),
                    _ => false
                });

            return tag?.Tag ?? string.Empty;

        }
    }
}
