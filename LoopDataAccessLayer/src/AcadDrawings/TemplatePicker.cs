using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    public class TemplatePicker : ITemplatePicker
    {
        private readonly IDataLoader dataLoader;
        private readonly LoopDataConfig loopConfig;

        public TemplatePicker(IDataLoader dataLoader, LoopDataConfig loopConfig)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
        }

        public TemplateConfig? GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            return template.TemplateName.ToUpper() switch
            {
                "XMTR" => GetSimpleTemplate(template, tagMap, "AI"),
                "AIN_3W" => GetSimpleTemplate(template, tagMap, "AI"),
                "DIN_4W" => GetSimpleTemplate(template, tagMap, "DI"),

                "PID_AI_AO" => GetPidTemplate(tagMap),
                "PID_AI_NOAO" => GetPidNoAOTemplate(tagMap),
                "XV_2XY" => GetXV2XYTemplate(tagMap),
                _ => template,
            };
        }

        private TemplateConfig? GetTemplate(string templateName)
        {
            
            return loopConfig.TemplateDefs.TryGetValue(templateName.ToUpper(), out TemplateConfig? template)
                ? template
                : null;
        }

        private TemplateConfig? GetSimpleTemplate(TemplateConfig template, Dictionary<string, string> tagMap, string tagType)
        {
            return GetTemplate( BuildSimpleName(template, tagMap, tagType) );
        }

        private TemplateConfig? GetPidTemplate(Dictionary<string, string> tagMap)
        {
            return GetTemplate( BuildPidName(tagMap) );
        }

        private TemplateConfig? GetPidNoAOTemplate(Dictionary<string, string> tagMap)
        {
            string templateName = BuildPidName(tagMap).Replace("AO", "noAO");
            return GetTemplate(templateName);
        }

        private TemplateConfig? GetXV2XYTemplate(Dictionary<string, string> tagMap)
        {
            return GetTemplate(BuildXV2XYName(tagMap));
        }

        private string BuildSimpleName(TemplateConfig template, Dictionary<string, string> tagMap, string tagType)
        {
            int numberOfJbs = CountNumberJbs(tagMap[tagType]);
            string templateName;
            if (0 <= numberOfJbs && numberOfJbs <= 2)
            {
                templateName = template.TemplateName + "_" + numberOfJbs.ToString() + "JB";
            }
            else
            {
                string msg = "Number of JBs must be between 0 and 2, not (" + numberOfJbs.ToString() + ").";
                throw new ArgumentOutOfRangeException(msg);
            }

            return templateName;
        }

        private string BuildPidName(Dictionary<string, string> tagMap)
        {
            int numberOfAIJbs = CountNumberJbs(tagMap["AI"]);
            int numberOfAOJbs = CountNumberJbs(tagMap["AO"]);
            string templateName;
            if ((0 <= numberOfAIJbs && numberOfAIJbs <= 1) & (0 <= numberOfAOJbs && numberOfAOJbs <= 1))
            {
                templateName = "PID_AI_" + numberOfAIJbs.ToString() + "JB_AO_" + numberOfAOJbs.ToString() + "JB";
            }
            else
            {
                string msg = "Number of JBs must be between 0 and 2, not ("
                    + numberOfAIJbs.ToString()
                    + ","
                    + numberOfAOJbs.ToString()
                    + ").";
                throw new ArgumentOutOfRangeException(msg);
            }

            return templateName;
        }

        private string BuildXV2XYName(Dictionary<string, string> tagMap)
        {
            int numberOfBPCSJbs = CountNumberJbs(tagMap["SOL-BPCS"]);
            int numberOfSISJbs = CountNumberJbs(tagMap["SOL-SIS"]);
            string templateName;
            if ((0 <= numberOfBPCSJbs && numberOfBPCSJbs <= 1) & (0 <= numberOfSISJbs && numberOfSISJbs <= 1))
            {
                templateName = "XV_2XY_BPCS_"
                    + numberOfBPCSJbs.ToString()
                    + "JB_SIS_"
                    + numberOfSISJbs.ToString()
                    + "JB";
            }
            else
            {
                string msg = "Number of JBs must be between 0 and 2, not ("
                    + numberOfBPCSJbs.ToString()
                    + ","
                    + numberOfSISJbs.ToString()
                    + ").";
                throw new ArgumentOutOfRangeException(msg);
            }

            return templateName;
        }

        private int CountNumberJbs(string tag)
        {
            var rows = dataLoader.GetJBRows(tag);
            if (rows is not null)
            {
                return rows.Select(r => ExcelHelper.GetRowString(r, dataLoader.ExcelJBCols.JBTag))
                           .Distinct()
                           .Count();
            }

            return 0;
        }


    }
}
