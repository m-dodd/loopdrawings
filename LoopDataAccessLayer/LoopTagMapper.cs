﻿using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class LoopTagMapper
    {
        // this is an idea, but not sure if it is the right idea or not
        // I have a problem whereby the titleblock will be defined in the ui but every drawing will need it
        // so it needs to propagate down to the block builder - but how?
        // public static TitleBlockData TitleBlock { get; set; }
        private static string[] definedTagTypes = { "AI", "AO", "CONTROLLER", "VALVE" };

        public static Dictionary<string, string> BuildTagMap(
            IEnumerable<LoopTagData> tags, TemplateConfig templateConfig
        )
        {
            Dictionary<string, string> tagMap = new();

            foreach(string tagtype in GetTagTypes(templateConfig.BlockMap))
            {
                if (definedTagTypes.Contains( tagtype ))
                {
                    tagMap[tagtype] = GetTag(tags, tagtype);
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

                case "CONTROLLER":
                    return tags.Where(t => t.IOType == "SOFT" &&
                                           t.Tag.ToUpper().Contains("IC"))
                               .First().Tag;

                case "VALVE":
                    string[] valveTypes = { "CV-BALL", "CV-BUTTERFLY", "CV-GLOBE" };
                    return tags.Where(t => valveTypes.Contains(t.InstrumentType.ToUpper()))
                               .First().Tag;

                default:
                    return string.Empty;
            }
        }
    }
}
