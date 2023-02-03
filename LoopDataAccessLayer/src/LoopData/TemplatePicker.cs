using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoopDataAccessLayer
{
    public class TemplatePicker : ITemplatePicker
    {
        private DataLoader dataLoader;

        public TemplatePicker(DataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public TemplateConfig GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            //switch (template.TemplateName)
            //{
            //    case "XMITTER":
            //        return GetXmitterTemplate(template, tagMap);
                
            //    case "PID_LOOP":
            //        return GetPidTemplate(template, tagMap); ;
                
            //    default:
            //        return template;
            //}
            if (template.TemplateName.Contains("XMITER"))
            {
                return GetXmitterTemplate(template, tagMap);
            }
            else
            {
                return template;
            }
        }

        private TemplateConfig GetXmitterTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            // create XMITTER template selection logic
            int numberOfJbs = CountNumberJbs(tagMap["AI"]);
            return template;
        }

        private TemplateConfig GetPidTemplate(TemplateConfig template, Dictionary<string, string> tagMap)
        {
            // create PID_LOOP template selection logic
            // PID_LOOP needs to look at number of JB's as well as valve type
            return template;
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
