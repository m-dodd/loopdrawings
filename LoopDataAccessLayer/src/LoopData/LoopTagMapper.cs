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
                    { "AI", tagData => tagData.IsAI },
                    { "AI-1", tagData => tagData.IsAI && tagData.EndsWith("A") },
                    { "AI-2", tagData => tagData.IsAI && tagData.EndsWith("B") },
                    { "AO", tagData => tagData.IsAO },
                    { "DI", tagData => tagData.IsDI },
                    { "DO", tagData => tagData.IsDO },
                    { "CONTROLLER", tagData => tagData.IsSoft && tagData.TagContains("IC")},
                    { "VALVE", tagData => tagData.IsValve },
                    { "ZSC", tagData => tagData.IsDI && tagData.TagContains("ZSC") },
                    { "ZSO", tagData => tagData.IsDI && tagData.TagContains("ZSO") },
                    { "SOL-BPCS", tagData => tagData.IsSolenoid && tagData.IsBPCS },
                    { "SOL-SIS", tagData => tagData.IsSolenoid && tagData.IsSIS },
                    { "MOTOR-SD-BPCS", tagData => tagData.IsMotor && tagData.IsBPCS },
                    { "MOTOR-SD-SIS", tagData => tagData.IsMotor && tagData.IsSIS },
                    { "MOTOR-SD-SIS-A", tagData => tagData.IsMotor && tagData.IsSIS && tagData.EndsWith("A") },
                    { "MOTOR-SD-SIS-B", tagData => tagData.IsMotor && tagData.IsSIS && tagData.EndsWith("B") }
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
}
