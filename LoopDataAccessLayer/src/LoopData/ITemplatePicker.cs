using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface ITemplatePicker
    {
        TemplateConfig GetCorrectTemplate(TemplateConfig template, Dictionary<string, string> tagMap);
    }
}
