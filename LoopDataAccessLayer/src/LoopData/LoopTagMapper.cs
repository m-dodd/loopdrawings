using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using LoopDataAdapterLayer;
using Microsoft.VisualBasic;
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
                    { "TE", tagData => tagData.IsTE },

                    { "AO", tagData => tagData.IsAO },

                    { "DI", tagData => tagData.IsDI },
                    { "DI-BPCS", tagData => tagData.IsDI && tagData.IsBPCS },
                    { "DI-ESD", tagData => tagData.IsESDLoop},
                    // DI-1 / DI-2 are a bit complicated
                    // we can have a loop that has tags ending in A and B, or ST/SP, but also have loops where tag1 is BPCS and tag2 is SIS
                    { "DI-1", tagData => tagData.IsDI && tagData.IsBPCS && (tagData.IsESDButton || tagData.EndsWith("A", "ST"))},
                    { "DI-2", tagData => tagData.IsDI && ((tagData.IsSIS && tagData.IsESDButton) || (tagData.IsBPCS && tagData.EndsWith("B", "SP")))},
                    { "ZSC", tagData => tagData.IsDI && tagData.TagContains("ZSC") },
                    { "ZSO", tagData => tagData.IsDI && tagData.TagContains("ZSO") },
                    
                    { "DO", tagData => tagData.IsDO },
                    // beacons and horns got a little... more complex, but pretty sure this logic holds
                    //{ "DO-1", tagData => tagData.IsHorn || (tagData.IsBeacon && tagData.EndsWith("A"))},
                    { "DO-1", tagData => tagData.IsHorn || tagData.EndsWith("A")},
                    { "DO-2", tagData => tagData.EndsWith("B") || (tagData.IsBeacon && !tagData.EndsWith("A"))},
                    { "SOL-BPCS", tagData => tagData.IsSolenoid && tagData.IsBPCS },
                    { "SOL-SIS", tagData => tagData.IsSolenoid && tagData.IsSIS },
                    { "SOL-UNLOAD", tagData => tagData.IsDO && tagData.EndsWith("UN", "DEC") }, // unload solenoid
                    { "SOL-LOAD", tagData => tagData.IsDO && tagData.EndsWith("LD", "INC") }, // load solenoid
                    { "MOTOR-SD-BPCS", tagData => tagData.IsMotor && tagData.IsBPCS },
                    { "MOTOR-SD-SIS", tagData => tagData.IsMotor && tagData.IsSIS },
                    { "MOTOR-SD-SIS-A", tagData => tagData.IsMotor && tagData.IsSIS && tagData.EndsWith("A") },
                    { "MOTOR-SD-SIS-B", tagData => tagData.IsMotor && tagData.IsSIS && tagData.EndsWith("B") },

                    { "CONTROLLER", tagData => tagData.IsSoft && tagData.TagContains("IC")},
                    { "VALVE", tagData => tagData.IsValve },
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
                    var matchingTags = tags.Where(predicate).ToList();

                    if (matchingTags.Count > 1)
                    {
                        string msg = $"Multiple matching tags found for tag type '{tagType}'. " +
                                      "Do you have the right template configured for this loop?";
                        throw new NumberOfTagsForTypeExceededException(msg);
                    }

                    if (matchingTags.Count == 1 && !string.IsNullOrEmpty(matchingTags[0].Tag))
                    {
                        tagMap[tagType] = matchingTags[0].Tag;
                    }
                }
            }

            return tagMap;
        }

    }

    public class NumberOfTagsForTypeExceededException : Exception
    {
        public NumberOfTagsForTypeExceededException(string? message) : base(message)
        {
        }
        public NumberOfTagsForTypeExceededException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
