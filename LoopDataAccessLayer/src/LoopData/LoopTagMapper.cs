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
            "SOL-SIS"
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
            switch (tagtype)
            {
                case "AI":
                    return tags.Where(t => t.IOType == "AI")
                               .First().Tag;

                case "AO":
                    return tags.Where(t => t.IOType == "AO")
                               .First().Tag;

                case "DI":
                    return tags.Where(t => t.IOType == "DI")
                               .First().Tag;

                case "CONTROLLER":
                    return tags.Where(t => t.IOType == "SOFT" &&
                                           t.Tag.ToUpper().Contains("IC"))
                               .First().Tag;

                case "VALVE":
                    Regex rg = new(@"ball|gate|globe|butterfly", RegexOptions.IgnoreCase);
                    return tags
                        .Where(t => (t.IOType == "---"
                                     || string.IsNullOrEmpty(t.IOType))
                                    && rg.IsMatch(t.InstrumentType))
                        .First().Tag;

                case "ZSC":
                    return tags.Where(t => t.IOType == "DI" 
                                        && t.Tag.ToUpper().Contains("ZSC"))
                               .First().Tag;

                case "ZSO":
                    return tags.Where(t => t.IOType == "DI" 
                                        && t.Tag.ToUpper().Contains("ZSO"))
                               .First().Tag;

                case "SOL-BPCS":
                    return tags.Where(t => t.IOType == "DO" 
                                        && t.InstrumentType.ToUpper().Contains("SOLENOID")
                                        && t.System.ToUpper().Contains("BPCS"))
                               .First().Tag;

                case "SOL-SIS":
                    return tags.Where(t => t.IOType == "DO"
                                        && t.InstrumentType.ToUpper().Contains("SOLENOID")
                                        && t.System.ToUpper().Contains("SIS"))
                               .First().Tag;

                default:
                    return string.Empty;
            }
        }
    }
}
