﻿using DocumentFormat.OpenXml.Wordprocessing;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingBuilder
    {
        
        private DataLoader dataLoader;
        private LoopDataConfig loopConfig;

        public AcadDrawingBuilder(DataLoader dataLoader, LoopDataConfig loopConfig)
        {
            // do I need to check that the dataloader was correctly instantiated?
            //  Yes, it can fail to load the excel or the db and if it does then I can't
            //  do any work. figure this out
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
        }

        public AcadDrawingData? BuildDrawing(LoopNoTemplatePair loop)
        {
            if (loopConfig.TemplateDefs.TryGetValue(loop.Template, out TemplateConfig? template))
            {
                Dictionary<string, string>? tagMap = BuildLoopTagMap(loop, template);
                if (tagMap != null)
                {
                    List<BlockDataMappable> blocks = BuildBlocks(template, tagMap);
                    AcadDrawingData drawing = new AcadDrawingData
                    {
                        Blocks = blocks,
                        LoopID = loop.LoopNo,
                        TemplateName = template.TemplateName,
                        DrawingFileName = Path.Combine(loopConfig.TemplateDrawingPath, template.DrawingFilename)
                    };
                    drawing.MapData();
                    return drawing;
                }
            }
            return null;
        }

        private Dictionary<string, string> BuildLoopTagMap(LoopNoTemplatePair loop, TemplateConfig template)
        {
            List<LoopTagData> tags = dataLoader.DBLoader.GetLoopTags(loop);
            Dictionary<string, string> tagMap = LoopTagMapper.BuildTagMap(tags, template);
            return tagMap;
            
        }

        private List<BlockDataMappable> BuildBlocks(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            List<BlockDataMappable> blocks = new();
            AcadBlockFactory blockFactory = new(dataLoader);
            foreach (BlockMapData blockMap in template.BlockMap)
            {
                //blocks.Add(blockFactory.GetBlock(blockMap, tagMap));
            }

            return blocks;
        }


        public string ToJson()
        {
            throw new NotImplementedException();
        }

        public void ToJson(string fileName)
        {
            throw new NotImplementedException();
        }

        public List<AcadDrawingData> FromJson()
        {
            throw new NotImplementedException();
        }
    }


    
}
