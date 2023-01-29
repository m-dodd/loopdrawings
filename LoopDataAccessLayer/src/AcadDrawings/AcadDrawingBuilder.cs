using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingBuilder : IAcadDrawingBuilder
    {
        
        private readonly DataLoader dataLoader;
        private readonly LoopDataConfig loopConfig;

        public AcadDrawingBuilder(DataLoader dataLoader, LoopDataConfig loopConfig)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
        }

        public AcadDrawingDataMappable? BuildDrawing(LoopNoTemplatePair loop)
        {
            if (loopConfig.TemplateDefs.TryGetValue(loop.Template, out TemplateConfig? template))
            {
                return CreateDrawing(loop, template);
            }
            return null;
        }

        private AcadDrawingDataMappable? CreateDrawing(LoopNoTemplatePair loop, TemplateConfig template)
        {
            Dictionary<string, string> tagMap = BuildLoopTagMap(loop, template);
            
            if (tagMap.Count > 0)
            {
                return ConstructDrawing(loop, 
                                        GetTemplateToUse(template, tagMap),
                                        tagMap);
            }
            return null;
        }

        private TemplateConfig GetTemplateToUse(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            TemplatePicker templatePicker = new(dataLoader);
            return templatePicker.GetCorrectTemplate(template, tagMap);
        }


        private AcadDrawingDataMappable ConstructDrawing(LoopNoTemplatePair loop, TemplateConfig template, Dictionary<string, string> tagMap)
        {
            string drawingName = BuildDrawingIdentifier(loop);
            dataLoader.TitleBlock.DrawingName = drawingName;

            List<IMappableBlock> blocks = BuildBlocks(template, tagMap);
            AcadDrawingDataMappable drawing = new()
            {
                Blocks = blocks,
                LoopID = loop.LoopNo,
                TemplateName = template.TemplateName,
                TemplateDrawingFileName = Path.Combine(loopConfig.TemplateDrawingPath, template.TemplateName + ".dwg"),
                OutputDrawingFileName = BuildOutputDrawingName(drawingName)
            };
            drawing.MapData();

            return drawing;
        }


        private string BuildOutputDrawingName(string drawingName)
        {
            string name = drawingName + ".dwg";
            return Path.Combine(loopConfig.OutputDrawingPath, name);
        }

        private string BuildDrawingIdentifier(LoopNoTemplatePair loop) 
        {
            return loopConfig.SiteID + "-" + loop.LoopNo;
        }

        private Dictionary<string, string> BuildLoopTagMap(LoopNoTemplatePair loop, TemplateConfig template)
        {
            List<LoopTagData> tags = dataLoader.GetLoopTags(loop);
            if (tags.Count > 0) 
            {
                return LoopTagMapper.BuildTagMap(tags, template);
            }
            else
            {
                return new Dictionary<string, string>();
            }

        }

        private List<IMappableBlock> BuildBlocks(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            List<IMappableBlock> blocks = new();
            AcadBlockFactory blockFactory = new(dataLoader);
            foreach (BlockMapData blockMap in template.BlockMap)
            {
                IMappableBlock block = blockFactory.GetBlock(blockMap, tagMap);
                
                if (block is not EMPTY_BLOCK)
                {
                    blocks.Add(block);
                }
            }

            return blocks;
        }
    }
}
