using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using LoopDataAdapterLayer;
using Org.BouncyCastle.Asn1.Pkcs;
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
        private readonly Dictionary<string, Func<LoopTagData, bool>> tagTypePredicateMap;

        public LoopTagMapper()
        {
            tagTypePredicateMap = new Dictionary<string, Func<LoopTagData, bool>>
                {
                    { "AI", tagData => tagData.IsAI() },
                    { "AI-1", tagData => tagData.IsAI() && tagData.EndsWith("A") },
                    { "AI-2", tagData => tagData.IsAI() && tagData.EndsWith("B") },
                    { "AO", tagData => tagData.IsAO() },
                    { "DI", tagData => tagData.IsDI() },
                    { "DO", tagData => tagData.IsDO() },
                    { "CONTROLLER", tagData => tagData.IsIOType("SOFT") && tagData.TagContains("IC")},
                    { "VALVE", tagData => tagData.IsValve() },
                    { "ZSC", tagData => tagData.IsDI() && tagData.TagContains("ZSC") },
                    { "ZSO", tagData => tagData.IsDI() && tagData.TagContains("ZSO") },
                    { "SOL-BPCS", tagData => tagData.IsSolenoid() && tagData.IsSystemType("BPCS") },
                    { "SOL-SIS", tagData => tagData.IsSolenoid() && tagData.IsSystemType("SIS") },
                    { "MOTOR-SD-BPCS", tagData => tagData.IsMotor() && tagData.IsSystemType("BPCS") },
                    { "MOTOR-SD-SIS", tagData => tagData.IsMotor() && tagData.IsSystemType("SIS") },
                    { "MOTOR-SD-SIS-A", tagData => tagData.IsMotor() && tagData.IsSystemType("SIS") && tagData.EndsWith("A") },
                    { "MOTOR-SD-SIS-B", tagData => tagData.IsMotor() && tagData.IsSystemType("SIS") && tagData.EndsWith("B") }
                };
        }

       public Dictionary<string, string> BuildTagMap(IEnumerable<LoopTagData> tags, TemplateConfig templateConfig)
        {
            var tagMap = new Dictionary<string, string>();
            var tagTypes = templateConfig.BlockMap.SelectMany(block => block.Tags).Intersect(tagTypePredicateMap.Keys);

            foreach (var tagType in tagTypes)
            {
                if (tagTypePredicateMap.TryGetValue(tagType, out var predicate))
                {
                    var tag = tags.FirstOrDefault(predicate);
                    if (tag != null && !string.IsNullOrEmpty(tag.Tag))
                    {
                        tagMap[tagType] = tag.Tag;
                    }
                }
            }
            
            return tagMap;
        }
    }


    public static class LoopTagDataExtensions
    {
        public static bool EqualsCaseInsensitive(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }
        
        public static bool IsIOType(this LoopTagData tag, string ioType)
        {
            return tag.IOType.EqualsCaseInsensitive(ioType);
        }

        public static bool IsAI(this LoopTagData tag)
        {
            return tag.IsIOType("AI");
        }

        public static bool IsAO(this LoopTagData tag)
        {
            return tag.IsIOType("AO");
        }

        public static bool IsDI(this LoopTagData tag)
        {
            return tag.IsIOType("DI");
        }

        public static bool IsDO(this LoopTagData tag)
        {
            return tag.IsIOType("DO");
        }

        public static bool IsEmptyIOType(this LoopTagData tag)
        {
            return string.IsNullOrEmpty(tag.IOType) || tag.IOType == "---";
        }

        public static bool IsSystemType(this LoopTagData tag, string systemType)
        {
            return tag.SystemType.EqualsCaseInsensitive(systemType);
        }

        public static bool IsInstrumentType(this LoopTagData tag, string instrumentType)
        {
            string pattern = @"^\w+[-_]?.*$";

            return Regex.IsMatch(tag.InstrumentType, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValve(this LoopTagData tag)
        {
            return tag.IsEmptyIOType() && Regex.IsMatch(tag.InstrumentType, @"ball|gate|globe|butterfly", RegexOptions.IgnoreCase);
        }

        public static bool IsSolenoid(this LoopTagData tag)
        {
            return tag.IsIOType("DO") && tag.IsInstrumentType("SOLENOID");
        }

        public static bool IsMotor(this LoopTagData tag)
        {
            return tag.IsIOType("DO") && tag.IsInstrumentType("MOTOR-SD");
        }

        public static bool EndsWith(this LoopTagData tag, string suffix)
        {
            return tag.Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }

        public static bool TagContains(this LoopTagData tag, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            return tag.Tag.Contains(value, comparisonType);
        }

        public static bool IsIOInstrumentSystem(this LoopTagData tag, string ioType, string instrumentType, string systemType)
        {
            return tag.IsIOType(ioType) && tag.IsInstrumentType(instrumentType) && tag.IsSystemType(systemType);
        }
    }
}
