using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WTEdge.Entities;
using Serilog;
using System.Diagnostics.Tracing;

namespace LoopDataAccessLayer
{
    public interface ITemplatePicker
    {
        TemplateConfig? GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap);
        IEnumerable<TemplateConfig?> GetCorrectDoubleTemplate(TemplateConfig template, Dictionary<string, string> tagMap);

    }


    public class TemplatePicker : ITemplatePicker
    {
        private readonly IDataLoader dataLoader;
        private readonly LoopDataConfig loopConfig;
        private readonly ILogger logger;
        private TemplateConfig template;
        private Dictionary<string, string> tagMap;

        public TemplatePicker(IDataLoader dataLoader, LoopDataConfig loopConfig, ILogger logger)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            this.logger = logger;

            // these will be set by the public functions
            this.template = new TemplateConfig();
            this.tagMap = new Dictionary<string, string>();
        }

        public TemplateConfig? GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            this.template = template;
            this.tagMap = tagMap;
            string templateName = template.TemplateName.ToUpper() switch
            {
                "XMTR" or
                "AIN_3W" => BuildSimpleName("AI"),

                "AOUT_2W" => BuildSimpleName("AO"),

                // this makes the assumption AI-1 and AI-2 have the same number of jbs
                "XMTRX2" => BuildSimpleName("AI-1"),

                "DIN_2W" or
                "DIN_4W" => BuildSimpleName("DI"),

                // this makes the assumption DI-1 and DI-2 have the same number of jbs
                "DINX2_2W" or
                "DINX2_2W_SIS" => BuildSimpleName("DI-1"),
                "DINX2_2W_SIS_RLY" => BuildSimpleName("DI-BPCS", MAX_JBS: 1),

                "DOUT_2W_RLY" => BuildSimpleName("DO"),
                "DOUTX2_2W_RLY" => BuildSimpleName("DO-1"),

                "PID_AI_AO" => BuildPidName(),
                "PID_AI_NOAO" => BuildPidName().Replace("AO", "noAO"),
                "XV_1XY" => BuildSimpleName("SOL-BPCS"),
                "XV_2XY" => BuildXV2XYName(),
                _ => string.Empty,
            };

            return string.IsNullOrEmpty(templateName) ? template : GetTemplateFromName(templateName);
        }

        public IEnumerable<TemplateConfig?> GetCorrectDoubleTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            this.template = template;
            this.tagMap = tagMap;
            var templateConfigList = new List<TemplateConfig?>();
            // this is going to get hardcoded as it's a unique template and there isn't much value in making it more general right now
            // FUTURE: in the future this could obviously be improved significantly
            if (template.TemplateName.ToUpper() == "PID_AI_DOX2")
            {
                templateConfigList.Add( GetTemplateFromName("PID_AI_1JB_DOx2_0JB-1") );
                templateConfigList.Add( GetTemplateFromName("PID_AI_1JB_DOx2_0JB-2") );
            }
            return templateConfigList;
        }

        private TemplateConfig? GetTemplateFromName(string templateName)
        {
            
            return loopConfig.TemplateDefs.TryGetValue(templateName.ToUpper(), out TemplateConfig? template)
                ? template
                : null;
        }

        private string TryGetTag(string tagType)
        {
            try
            {
                return tagMap[tagType];
            }
            catch (KeyNotFoundException ex)
            {
                throw new TemplateTagTypeNotFoundException(template.TemplateName, tagType, ex);
            }
        }

        private string BuildSimpleName(string tagType, int MAX_JBS = 2)
        {
            string tag = TryGetTag(tagType);
            int numberOfJbs = CountNumberJbs(tag);
         
            if (0 <= numberOfJbs && numberOfJbs <= MAX_JBS)
            {
                return $"{template.TemplateName}_{numberOfJbs}JB";
            }
            else
            {
                string msg = $"Number of JBs must be between 0 and {MAX_JBS}, not (" + numberOfJbs.ToString() + ").";
                throw new TemplateNumberOfJbsException(msg, MAX_JBS);
            }
        }

        private string BuildPidName()
        {
            string ai = TryGetTag("AI");
            string ao = TryGetTag("AO");
            int numberOfAIJbs = CountNumberJbs(ai);
            int numberOfAOJbs = CountNumberJbs(ao);
            
            const int MAX_JBS = 1;
            if ((0 <= numberOfAIJbs && numberOfAIJbs <= MAX_JBS) & (0 <= numberOfAOJbs && numberOfAOJbs <= MAX_JBS))
            {
                return $"PID_AI_{numberOfAIJbs}JB_AO_{numberOfAOJbs}JB";
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

        }

        private string BuildXV2XYName()
        {
            string solBPCS = TryGetTag("SOL-BPCS");
            string solSIS = TryGetTag("SOL-SIS");
            int numberOfBPCSJbs = CountNumberJbs(solBPCS);
            int numberOfSISJbs = CountNumberJbs(solSIS);

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

            return $"XV_2XY_BPCS_{numberOfBPCSJbs}JB_SIS_{numberOfSISJbs}JB";
        }

        private int CountNumberJbs(string tag)
        {
            var rows = dataLoader.GetJBRows(tag);
            if (rows is not null)
            {
                return rows.Select(r => r.GetCellString(dataLoader.ExcelJBCols.JBTag))
                           .Distinct()
                           .Count();
            }

            return 0;
        }
    }


    public class TemplateTagTypeNotFoundException : Exception
    {
        private const string defaultMessage = "Tagtype {0} not created as part of tag map.";
        private const string descriptiveTemplateMessage = 
            "For template {0} - Tagtype {1} not created as part of tag map. " +
            "If the template is configured correctly please contact system designer and create a tag map for {1}.";
        public TemplateTagTypeNotFoundException(string key) : base(string.Format(defaultMessage, key))
        {
            Key = key;
        }

        public TemplateTagTypeNotFoundException(string key, Exception innerException) : base(string.Format(defaultMessage, key), innerException)
        {
            Key = key;
        }

        public TemplateTagTypeNotFoundException(string template, string key, Exception innerException) : base(string.Format(descriptiveTemplateMessage, template, key), innerException)
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
