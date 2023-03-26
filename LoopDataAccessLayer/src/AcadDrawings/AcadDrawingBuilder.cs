using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTEdge.Entities;

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

        public IEnumerable<AcadDrawingDataMappable> BuildDrawings(LoopNoTemplatePair loop)
        {
            TemplateConfig sdkTemplate = GetTemplate("SDK") ?? throw new NullReferenceException("SDK is missing from configuration.");
            TemplateConfig? template = GetTemplate(loop);
            List<AcadDrawingDataMappable> drawings = new();
            if (template is null)
            {
                return drawings;
            }

            Dictionary<string, string> tagMap = GetLoopTagMap(loop, template);
            if (tagMap.Count == 0)
            {
                return drawings;
            }

            TemplateConfig? correctTemplate = templatePicker.GetCorrectTemplate(template, tagMap);
            if (correctTemplate is null)
            {
                return drawings;
            }

            SDKDrawingProvider sdk = new SDKDrawingProvider(dataLoader, correctTemplate, tagMap, loopConfig);
            // still need to figure out how to flag the original drawing to delete the SDK block
            // I know I have an idea to write an attribute to the block attributes and then that can be checked
            // by teh acad portion, but right now I don't have a way to pass that down
            if (sdk.NewDrawingRequired())
            {
                tagMap["DRAWING_NAME"] = tagMap["DRAWING_NAME"] + "-1";
                drawings.Add(ConstructDrawing(loop, correctTemplate, tagMap));


                // replace the last two characters of the name
                tagMap["DRAWING_NAME"] = tagMap["DRAWING_NAME"][..^2] + "-2";

                // get the tag to use for the blocks
                tagMap["SDK_TAG"] = sdk.GetSDTag();
                if (!string.IsNullOrEmpty(tagMap["SDK_TAG"]))
                {
                    drawings.Add(ConstructDrawing(loop, sdkTemplate, tagMap));
                }
            }
            else
            {
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

        //private AcadDrawingDataMappable ConstructSDKDrawing(
        //    LoopNoTemplatePair loop,
        //    TemplateConfig template,
        //    Dictionary<string, string> tagMap,
        //    IEnumerable<Tblsdkrelation> sds)
        //{

        //     ok, not done
        //     need to figure out how to populate the shutdown blocks
        //     aslo need to get that shutdown key data, but it is already needed as part of the shutdown key provider
        //     so maybe that class can have an internal member with a property or something
        //    string drawingName = tagMap.TryGetValue("DRAWING_NAME", out string? d) ? d : string.Empty;
        //    var blocks = BuildBlocks(template, tagMap);
        //    var block1 = 
        //    var drawing = new AcadDrawingDataMappable
        //    {
        //        Blocks = blocks,
        //        LoopID = loop.LoopNo,
        //        TemplateName = template.TemplateName,
        //        TemplateDrawingFileName = BuildTemplateDrawingName(template),
        //        OutputDrawingFileName = BuildOutputDrawingName(drawingName)
        //    };
        //    drawing.MapData();

        //    return drawing;
        //}


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
            List<LoopTagData> tags = (List<LoopTagData>)dataLoader.GetLoopTags(loop);
            if (tags.Count > 0) 
            {
                Dictionary<string, string> tagMap = LoopTagMapper.BuildTagMap(tags, template);
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
