using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTEdge.Entities;
using Serilog;

namespace LoopDataAccessLayer
{
    public class AcadDrawingBuilder : IAcadDrawingBuilder
    {
        
        private readonly IDataLoader dataLoader;
        private readonly ITemplatePicker templatePicker;
        private readonly IAcadBlockFactory blockFactory;
        private readonly LoopDataConfig loopConfig;
        private readonly LoopTagMapper loopTagMapper;
        private readonly ILogger logger;

        public AcadDrawingBuilder(
            IDataLoader dataLoader,
            LoopDataConfig loopConfig,
            ITemplatePicker templatePicker,
            IAcadBlockFactory blockFactory,
            LoopTagMapper loopTagMapper,
            ILogger logger
            )
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            this.templatePicker = templatePicker;
            this.blockFactory = blockFactory;
            this.loopTagMapper = loopTagMapper;
            this.logger = logger;

        }

        public IEnumerable<AcadDrawingDataMappable> BuildDrawings(LoopNoTemplatePair loop)
        {
            TemplateConfig sdkTemplate = GetTemplate("SDK") ?? throw new NullReferenceException("SDK is missing from configuration.");
            TemplateConfig? template = GetTemplate(loop);
            List<AcadDrawingDataMappable> drawings = new();

            // **************************************************************************
            // SHOULD I BE THROWING EXCEPTIONS INSTEAD OF RETURNING??
            // **************************************************************************
            if (template is null)
            {
                logger.Warning("No template found.", loop.LoopNo);
                return drawings;
            }
            // **************************************************************************
            // SHOULD I BE THROWING EXCEPTIONS INSTEAD OF RETURNING??
            // **************************************************************************
            Dictionary<string, string> tagMap = GetLoopTagMap(loop, template);
            if (tagMap.Count == 0)
            {
                logger.Warning("No tags found.", loop.LoopNo);
                return drawings;
            }
            // **************************************************************************
            // SHOULD I BE THROWING EXCEPTIONS INSTEAD OF RETURNING??
            // **************************************************************************
            TemplateConfig? correctTemplate = templatePicker.GetCorrectTemplate(template, tagMap);
            if (correctTemplate is null)
            {
                logger.Warning("Correct template could not be determined - contact system administrator.", loop.LoopNo);
                return drawings;
            }

            SDKDrawingProvider sdk = new(dataLoader, correctTemplate, tagMap, loopConfig);
            if (sdk.NewDrawingRequired())
            {
                string drawingName1 = tagMap["DRAWING_NAME"] + "-1";
                string drawingName2 = tagMap["DRAWING_NAME"] + "-2";
                logger.Information($"SDK drawing required... Drawing will be renamed {drawingName1} and sdk will be named {drawingName2}.");

                tagMap["DRAWING_NAME"] = drawingName1;
                tagMap["DRAWING_NAME_SD"] = drawingName2;
                tagMap["DELETE_SD"] = "true";
                drawings.Add(ConstructDrawing(loop, correctTemplate, tagMap));


                // replace the last two characters of the name
                tagMap["DRAWING_NAME"] = drawingName2;
                tagMap["DRAWING_NAME_SD"] = string.Empty;

                // get the tag to use for the blocks
                tagMap["SDK_TAG"] = sdk.GetSDTag();
                tagMap["DELETE_SD"] = "false";
                if (!string.IsNullOrEmpty(tagMap["SDK_TAG"]))
                {
                    drawings.Add(ConstructDrawing(loop, sdkTemplate, tagMap));
                }
            }
            else
            {
                tagMap["DELETE_SD"] = "false";
                tagMap["DRAWING_NAME_SD"] = string.Empty;
                drawings.Add( ConstructDrawing(loop, correctTemplate, tagMap) );
            }
            return drawings;
        }

        private TemplateConfig? GetTemplate(LoopNoTemplatePair loop)
        {
            return GetTemplate(loop.Template);
        }

        private TemplateConfig? GetTemplate(string templateName)
        {
            return loopConfig.TemplateDefs.TryGetValue(templateName, out TemplateConfig? template)
                ? template
                : null;
        }

        private AcadDrawingDataMappable ConstructDrawing(
            LoopNoTemplatePair loop,
            TemplateConfig template,
            Dictionary<string, string> tagMap
            )
        {
            string drawingName = tagMap.TryGetValue("DRAWING_NAME", out string? d) ? d : string.Empty;
            var blocks = BuildBlocks(template, tagMap);
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
            if (string.IsNullOrEmpty(loopConfig.SiteID))
            {
                return loop.LoopNo;
            }
            return loopConfig.SiteID + "-" + loop.LoopNo;
        }

        private Dictionary<string, string> GetLoopTagMap(LoopNoTemplatePair loop, TemplateConfig template)
        {
            List<LoopTagData> tags = (List<LoopTagData>)dataLoader.GetLoopTags(loop);
            if (tags.Count > 0) 
            {
                Dictionary<string, string> tagMap = loopTagMapper.BuildTagMap(tags, template);
                tagMap["DRAWING_NAME"] = BuildDrawingIdentifier(loop);
                return tagMap;
            }
            else
            {
                return new Dictionary<string, string>();
            }

        }

        private List<IMappableBlock> BuildBlocks(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            return template.BlockMap
                .Select(blockData => blockFactory.GetBlock(blockData, tagMap))
                .Where(block => block is not EMPTY_BLOCK)
                .ToList();
        }
    }
}
