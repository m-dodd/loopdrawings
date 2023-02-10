using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            switch (template.TemplateName.ToUpper())
            {
                case "XMITER":
                    return GetXmitterTemplate(tagMap);

                case "PID_AI_AO":
                    return GetPidTemplate(tagMap);

                case "PID_AI_NOAO":
                    return GetPidNoAOTemplate(tagMap);

                default:
                    return template;
            }
        }

        private TemplateConfig? GetTemplate(string templateName)
        {
            
            return loopConfig.TemplateDefs.TryGetValue(templateName.ToUpper(), out TemplateConfig? template)
                ? template
                : null;
        }

        private TemplateConfig? GetXmitterTemplate(Dictionary<string, string> tagMap)
        {
            return GetTemplate( BuildXmiterName(tagMap) );
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

        private string BuildXmiterName(Dictionary<string, string> tagMap)
        {
            int numberOfJbs = CountNumberJbs(tagMap["AI"]);
            string templateName;
            if (0 <= numberOfJbs && numberOfJbs <= 2)
            {
                templateName = "XMITER_" + numberOfJbs.ToString() + "JB";
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

        private int CountNumberJbs(string tag)
        {
            var rows = dataLoader.GetJBRows(tag);
            if (rows is not null)
            {
                return rows.Select(r => ExcelHelper.GetRowString(r, ExcelJBColumns.JBTag))
                           .Distinct()
                           .Count();
            }

            return 0;
        }


    }
}
