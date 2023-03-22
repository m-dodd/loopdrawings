﻿using LoopDataAdapterLayer;
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

        public (AcadDrawingDataMappable?, AcadDrawingDataMappable?) BuildDrawings(LoopNoTemplatePair loop)
        {
            TemplateConfig sdkTemplate = GetTemplate("SDK") ?? throw new NullReferenceException("SDK is missing from configuration.");
            TemplateConfig? template = GetTemplate(loop);
            if (template is null)
            {
                return (null, null);
            }

            Dictionary<string, string> tagMap = GetLoopTagMap(loop, template);
            if (tagMap.Count == 0)
            {
                return (null, null);
            }

            TemplateConfig? correctTemplate = templatePicker.GetCorrectTemplate(template, tagMap);
            if (correctTemplate is null)
            {
                return (null, null);
            }

            SDKDrawingProvider sdk = new SDKDrawingProvider(dataLoader, template, tagMap, loopConfig);
            if (sdk.NewDrawingRequired())
            {
                tagMap["DRAWING_NAME"] = tagMap["DRAWING_NAME"] + "-1";
                var drawingMain = ConstructDrawing(loop, correctTemplate, tagMap);


                // so this is my idea right now
                // figure out how to get the right tag to get the shutdown data
                // map the sd data to tagmap
                // blocks will know what to do with tagmap data
                // oh i guess it doesn't work because tagmap is a dict<string, string>
                // and I'd need list.... stupid type checking
                List<Tblsdkrelation> sdkData = dataLoader.GetSDs(tagMap[]);
                tagMap["DRAWING_NAME"] = tagMap["DRAWING_NAME"] + "-2";
                tagMap["SDK1"] = sdkData.Take(30);
                tagMap["SDK2"] = sdkData.Skip(30).Take(30);
                // need sdk data here
                var drawingSDK = ConstructDrawing(loop, sdkTemplate, tagMap);
                return (drawingMain, drawingSDK);
            }
            else
            {
                return (ConstructDrawing(loop, correctTemplate, tagMap), null);
            }
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
