using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WTEdge.Entities;
using Serilog;

namespace LoopDataAccessLayer
{
    public class TemplatePicker : ITemplatePicker
    {
        private readonly IDataLoader dataLoader;
        private readonly LoopDataConfig loopConfig;
        private readonly ILogger logger;

        public TemplatePicker(IDataLoader dataLoader, LoopDataConfig loopConfig, ILogger logger)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            this.logger = logger;
        }

        public TemplateConfig? GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            return template.TemplateName.ToUpper() switch
            {
                "XMTR" => GetSimpleTemplate(template, tagMap, "AI"),
                // this makes the assumption AI-1 and AI-2 have the same number of jbs
                "XMTRX2" => GetSimpleTemplate(template, tagMap, "AI-1"),
                "AIN_3W" => GetSimpleTemplate(template, tagMap, "AI"),

                "DIN_2W" => GetSimpleTemplate(template, tagMap, "DI"),
                "DIN_4W" => GetSimpleTemplate(template, tagMap, "DI"),

                "DOUT_2W_RLY" => GetSimpleTemplate(template, tagMap, "DO"),

                "PID_AI_AO" => GetPidTemplate(tagMap),
                "PID_AI_NOAO" => GetPidNoAOTemplate(tagMap),
                "XV_2XY" => GetXV2XYTemplate(tagMap),
                _ => template,
            };
        }

        public IEnumerable<TemplateConfig?> GetCorrectDoubleTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            var templateConfigList = new List<TemplateConfig?>();
            // this is going to get hardcoded as it's a unique template and there isn't much value in making it more general right now
            // in the future this could obviously be improved significantly
            if (template.TemplateName.ToUpper() == "PID_AI_DOX2")
            {
                templateConfigList.Add( GetTemplate("PID_AI_1JB_DOx2_0JB-1") );
                templateConfigList.Add( GetTemplate("PID_AI_1JB_DOx2_0JB-2") );
            }
            return templateConfigList;
        }

        private TemplateConfig? GetTemplate(string templateName)
        {
            
            return loopConfig.TemplateDefs.TryGetValue(templateName.ToUpper(), out TemplateConfig? template)
                ? template
                : null;
        }

        private TemplateConfig? GetSimpleTemplate(TemplateConfig template, Dictionary<string, string> tagMap, string tagType, int MAX_JBS = 2)
        {
            return GetTemplate( BuildSimpleName(template, tagMap, tagType, MAX_JBS) );
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

        private string BuildSimpleName(TemplateConfig template, Dictionary<string, string> tagMap, string tagType, int MAX_JBS = 2)
        {
            int numberOfJbs;
            try
            {
                numberOfJbs = CountNumberJbs(tagMap[tagType]);
            }
            catch (KeyNotFoundException ex)
            {
                string msg = $"For template {template.TemplateName} - Tagtype {tagType} not created as part of tag map. " +
                             $"If the template is configured correctly please contact system designer and create a tag map for {tagType}.";
                logger.Error(msg, ex);
                throw new TemplateTagTypeNotFoundException(msg, tagType, ex);
            }
            string templateName;
            if (0 <= numberOfJbs && numberOfJbs <= MAX_JBS)
            {
                templateName = $"{template.TemplateName}_{numberOfJbs}JB";
            }
            else
            {
                string msg = $"Number of JBs must be between 0 and {MAX_JBS}, not (" + numberOfJbs.ToString() + ").";
                throw new TemplateNumberOfJbsException(msg, MAX_JBS);
            }

            return templateName;
        }

        private string BuildPidName(Dictionary<string, string> tagMap)
        {
            int numberOfAIJbs = CountNumberJbs(tagMap["AI"]);
            int numberOfAOJbs = CountNumberJbs(tagMap["AO"]);
            const int MAX_JBS = 1;
            string templateName;
            if ((0 <= numberOfAIJbs && numberOfAIJbs <= MAX_JBS) & (0 <= numberOfAOJbs && numberOfAOJbs <= MAX_JBS))
            {
                templateName = $"PID_AI_{numberOfAIJbs}JB_AO_{numberOfAOJbs}JB";
            }
            else
            {
                string msg = $"Number of JBs must be between 0 and {MAX_JBS}, not ("
                    + numberOfAIJbs.ToString()
                    + ","
                    + numberOfAOJbs.ToString()
                    + ").";
                throw new TemplateNumberOfJbsException(msg, MAX_JBS);
            }

            return templateName;
        }

        private string BuildXV2XYName(Dictionary<string, string> tagMap)
        {
            int numberOfBPCSJbs = CountNumberJbs(tagMap["SOL-BPCS"]);
            int numberOfSISJbs = CountNumberJbs(tagMap["SOL-SIS"]);
            string templateName;
            const int MAX_JBS = 1;

            if (!(0 <= numberOfBPCSJbs && numberOfBPCSJbs <= MAX_JBS))
            {
                string msg = $"Number of JBs for BPCS must be between 0 and {MAX_JBS}, not {numberOfBPCSJbs}";
                throw new TemplateNumberOfJbsException(msg, MAX_JBS);
            }

            if (!(0 <= numberOfSISJbs && numberOfSISJbs <= MAX_JBS))
            {
                string msg = $"Number of JBs for SIS must be between 0 and {MAX_JBS}, not {numberOfSISJbs}";
                throw new TemplateNumberOfJbsException(msg, MAX_JBS);
            }

            templateName = $"XV_2XY_BPCS_{numberOfBPCSJbs}JB_SIS_{numberOfSISJbs}JB";

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


    public class TemplateTagTypeNotFoundException : Exception
    {
        private const string defaultMessage = "Tagtype {0} not created as part of tag map.";
        public TemplateTagTypeNotFoundException(string key) : base(string.Format(defaultMessage, key))
        {
            Key = key;
        }

        public TemplateTagTypeNotFoundException(string msg, string key) : base(msg)
        {
            Key = key;
        }

        public TemplateTagTypeNotFoundException(string msg, string key, Exception innerException) : base(msg, innerException)
        {
            Key = key;
        }

        public string Key { get; }
    }

    public class TemplateNumberOfJbsException : Exception
    {
        private const string defaultMessage = "Number of JBs found is not supported.";
        public TemplateNumberOfJbsException() : base(defaultMessage) { }
        public TemplateNumberOfJbsException(int maxJBs) : base(defaultMessage)
        {
            MaxJBs = maxJBs;
        }
        public TemplateNumberOfJbsException(string msg) : base(msg) { }
        public TemplateNumberOfJbsException(string msg, int maxJBs) : base(msg)
        {
            MaxJBs = maxJBs;
        }
        

        public int MaxJBs { get; }
    }
}
