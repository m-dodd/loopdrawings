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
        private TitleBlockData titleBlock;

        public AcadDrawingBuilder(DataLoader dataLoader, LoopDataConfig loopConfig, TitleBlockData titleBlock)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            this.titleBlock = titleBlock;
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
                string drawingName = BuildDrawingIdentifier(loop);
                titleBlock.DrawingName = drawingName;
                
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
            return null;
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
                IMappableBlock block = blockFactory.GetBlock(blockMap, tagMap, titleBlock);
                
                if (block is not EMPTY_BLOCK)
                {
                    blocks.Add(block);
                }
            }

            return blocks;
        }
    }
}
