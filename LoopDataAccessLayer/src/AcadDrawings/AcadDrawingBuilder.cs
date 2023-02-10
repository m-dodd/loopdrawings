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
        
        private readonly IDataLoader dataLoader;
        private readonly ITemplatePicker templatePicker;
        private readonly IAcadBlockFactory blockFactory;
        private readonly LoopDataConfig loopConfig;

        public AcadDrawingBuilder(
            IDataLoader dataLoader,
            LoopDataConfig loopConfig,
            ITemplatePicker templatePicker,
            IAcadBlockFactory blockFactory
            )
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            this.templatePicker = templatePicker;
            this.blockFactory = blockFactory;
        }

        public AcadDrawingDataMappable? BuildDrawing(LoopNoTemplatePair loop)
        {
            var template = GetTemplate(loop);
            if (template is null)
            {
                return null;
            }

            var tagMap = GetLoopTagMap(loop, template);
            if (tagMap.Count == 0)
            {
                return null;
            }

            var correctTemplate = GetCorrectTemplate(template, tagMap);
            if (correctTemplate is null)
            {
                return null;
            }

            return ConstructDrawing(loop, correctTemplate, tagMap);

        }

        private TemplateConfig? GetTemplate(LoopNoTemplatePair loop)
        {
            return loopConfig.TemplateDefs.TryGetValue(loop.Template, out TemplateConfig? template)
                ? template
                : null;
        }

        private TemplateConfig? GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            return templatePicker.GetCorrectTemplate(template, tagMap);
        }

        private AcadDrawingDataMappable ConstructDrawing(LoopNoTemplatePair loop, TemplateConfig template, Dictionary<string, string> tagMap)
        {
            var blocks = BuildBlocks(template, tagMap);
            var drawingName = BuildDrawingIdentifier(loop);

            var drawing = new AcadDrawingDataMappable
            {
                Blocks = blocks,
                LoopID = loop.LoopNo,
                TemplateName = template.TemplateName,
                TemplateDrawingFileName = BuildTemplateDrawingName(template),
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

        private string BuildTemplateDrawingName(TemplateConfig template)
        {
            return Path.Combine(loopConfig.TemplateDrawingPath, template.TemplateFileName);
        }

        private string BuildDrawingIdentifier(LoopNoTemplatePair loop) 
        {
            return loopConfig.SiteID + "-" + loop.LoopNo;
        }

        private Dictionary<string, string> GetLoopTagMap(LoopNoTemplatePair loop, TemplateConfig template)
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
            //List<IMappableBlock> blocks = new();
            //foreach (BlockMapData blockMap in template.BlockMap)
            //{
            //    IMappableBlock block = blockFactory.GetBlock(blockMap, tagMap);

            //    if (block is not EMPTY_BLOCK)
            //    {
            //        blocks.Add(block);
            //    }
            //}

            //return blocks;
            return template.BlockMap
                .Select(blockMap => (IMappableBlock)blockFactory.GetBlock(blockMap, tagMap))
                .Where(block => block is not EMPTY_BLOCK)
                .ToList();
        }
    }
}
