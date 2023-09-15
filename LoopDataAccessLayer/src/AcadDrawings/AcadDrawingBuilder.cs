using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTEdge.Entities;
using Serilog;
using Org.BouncyCastle.Bcpg;

namespace LoopDataAccessLayer
{
    public interface IAcadDrawingBuilder
    {
        public IEnumerable<AcadDrawingDataMappable> BuildDrawings(LoopNoTemplatePair loop);
    }


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
            TemplateConfig? template = GetTemplate(loop);
            if (template is null)
            {
                string msg = $"No template found ({loop.Template}) for loop {loop.LoopNo}.";
                logger.Error(msg, loop.LoopNo);
                throw new DrawingBuilderException(msg);
            }

            Dictionary<string, string> tagMap = GetLoopTagMap(loop, template);
            if (tagMap.Count == 0)
            {
                string msg = "No tags found.";
                logger.Error(msg, loop.LoopNo);
                throw new DrawingBuilderException(msg);
            }

            if (template.TemplateRequiresTwoDrawings)
            {
                return BuildDoubleDrawing(loop, template, tagMap);
            }
            else
            {
                return BuildSingleDrawing(loop, template, tagMap);
            }
        }

        private IEnumerable<AcadDrawingDataMappable> BuildDoubleDrawing(LoopNoTemplatePair loop, TemplateConfig template, Dictionary<string, string> tagMap)
        {
            IEnumerable<TemplateConfig?> correctTemplates = templatePicker.GetCorrectDoubleTemplate(template, tagMap);

            if (correctTemplates.Any(x => x == null) || correctTemplates.Count() != 2)
            {
                string msg = "Correct template could not be determined - contact system administrator.";
                logger.Error(msg, loop.LoopNo);
                throw new DrawingBuilderException(msg);
            }

            TemplateConfig sdkTemplate = GetTemplate("SDK") ?? throw new NullReferenceException("SDK is missing from configuration.");
            List<AcadDrawingDataMappable> drawings = new();

            bool newSDKTemplateDrawing = false;
            string sdkTag = string.Empty;
            foreach (var correctTemplate in correctTemplates)
            {
                if (correctTemplate is not null)
                {
                    SDKDrawingProvider sdk = new(dataLoader, correctTemplate, loop);
                    if (sdk.NewDrawingRequired())
                    {
                        newSDKTemplateDrawing = true;
                        sdkTag = sdk.GetSDTag();
                        break;
                    }
                }
            }
            string drawingName = tagMap["DRAWING_NAME"];
            string drawingName1 = drawingName.AppendDrawingNumber("-01");
            string drawingName2 = drawingName.AppendDrawingNumber("-02");
            if (newSDKTemplateDrawing)
            {
                string drawingName3 = drawingName.AppendDrawingNumber("-03");
                logger.Information($"SDK drawing required... SDK will be named {drawingName3}.");

                // create first drawing of the template
                tagMap["DRAWING_NAME"] = drawingName1;
                tagMap["DRAWING_NAME_SD"] = drawingName3;
                tagMap["DELETE_SD"] = "true";
                drawings.Add(ConstructDrawing(loop, correctTemplates.ElementAt(0)!, tagMap));

                // create second drawing of the template
                tagMap["DRAWING_NAME"] = drawingName2;
                drawings.Add(ConstructDrawing(loop, correctTemplates.ElementAt(1)!, tagMap));

                // modify tagmap for third drawing (SDK) and create
                tagMap["SDK_TAG"] = sdkTag;
                tagMap["DELETE_SD"] = "false";
                tagMap["DRAWING_NAME_SD"] = string.Empty;
                if (!string.IsNullOrEmpty(tagMap["SDK_TAG"]))
                {
                    drawings.Add(ConstructDrawing(loop, sdkTemplate, tagMap));
                }
                else
                {
                    throw new DrawingBuilderException($"Cannot find a SDK Tag - why are we trying to create a SDK drawing? Something is wrong with loop {loop.LoopNo}");
                }
            }
            else
            {
                // create first drawing
                tagMap["DRAWING_NAME"] = drawingName1;
                tagMap["DRAWING_NAME_SD"] = string.Empty;
                tagMap["DELETE_SD"] = "false";
                drawings.Add(ConstructDrawing(loop, correctTemplates.ElementAt(0)!, tagMap));

                // create second drawing
                tagMap["DRAWING_NAME"] = drawingName2;
                drawings.Add(ConstructDrawing(loop, correctTemplates.ElementAt(1)!, tagMap));
            }
            return drawings;
        }

        private IEnumerable<AcadDrawingDataMappable> BuildSingleDrawing(LoopNoTemplatePair loop, TemplateConfig template, Dictionary<string, string> tagMap)
        {
            TemplateConfig? correctTemplate = templatePicker.GetCorrectTemplate(template, tagMap);
            if (correctTemplate is null)
            {
                string msg = "Correct template could not be determined - contact system administrator.";
                logger.Error(msg, loop.LoopNo);
                throw new DrawingBuilderException(msg);
            }

            TemplateConfig sdkTemplate = GetTemplate("SDK") ?? throw new NullReferenceException("SDK is missing from configuration.");
            List<AcadDrawingDataMappable> drawings = new();

            SDKDrawingProvider sdk = new(dataLoader, correctTemplate, loop);
            if (sdk.NewDrawingRequired())
            {
                string drawingName = tagMap["DRAWING_NAME"];
                string drawingName1 = drawingName.AppendDrawingNumber("-01");
                string drawingName2 = drawingName.AppendDrawingNumber("-02"); 
                logger.Information($"SDK drawing required... Drawing will be renamed {drawingName1} and sdk will be named {drawingName2}.");

                tagMap["DRAWING_NAME"] = drawingName1;
                tagMap["DRAWING_NAME_SD"] = drawingName2;
                tagMap["DELETE_SD"] = "true";
                drawings.Add(ConstructDrawing(loop, correctTemplate, tagMap));


                // modify tagmap for second drawing (SDK) and create
                tagMap["DRAWING_NAME"] = drawingName2;
                tagMap["DRAWING_NAME_SD"] = string.Empty;

                // get the tag to use for the blocks
                tagMap["SDK_TAG"] = sdk.GetSDTag(); 
                tagMap["DELETE_SD"] = "false";
                if (!string.IsNullOrEmpty(tagMap["SDK_TAG"]))
                {
                    drawings.Add(ConstructDrawing(loop, sdkTemplate, tagMap));
                }
                else
                {
                    throw new DrawingBuilderException($"Cannot find a SDK Tag - why are we trying to create a SDK drawing? Something is wrong with loop {loop.LoopNo}");
                }
            }
            else
            {
                tagMap["DELETE_SD"] = "false";
                tagMap["DRAWING_NAME_SD"] = string.Empty;
                drawings.Add(ConstructDrawing(loop, correctTemplate, tagMap));
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

        private string BuildDrawingName(LoopNoTemplatePair loop)
        {
            string baseDrawingName;

            if (string.IsNullOrEmpty(loopConfig.SiteID))
            {
                baseDrawingName = loop.LoopNo;
            }
            else
            {
                baseDrawingName = $"{loopConfig.SiteID}-{loop.LoopNo}";
            }

            return baseDrawingName.AppendDrawingNumber("-01");
        }


        private Dictionary<string, string> GetLoopTagMap(LoopNoTemplatePair loop, TemplateConfig template)
        {
            List<LoopTagData> tags = (List<LoopTagData>)dataLoader.GetLoopTags(loop);
            if (tags.Count > 0)
            {
                Dictionary<string, string> tagMap = loopTagMapper.BuildTagMap(tags, template);
                tagMap["DRAWING_NAME"] = BuildDrawingName(loop);
                tagMap["LoopNo"] = loop.LoopNo;
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

    public class DrawingBuilderException : Exception
    {
        public DrawingBuilderException(string? message) : base(message)
        {
        }
        public DrawingBuilderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
