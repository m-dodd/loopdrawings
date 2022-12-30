using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoopDataAdapterLayer
{
    public class LoopDataConfig
    {
        public Dictionary<string, TemplateConfig> TemplateDefs { get; set; }

        public void LoadConfig(string configFile)
        {
            var json = File.ReadAllText(configFile);
            var data = JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json);
            TemplateDefs = data;
        }
    }

    public class TemplateConfig
    {
        public string TemplateName { get; set; }
        public string Filename { get; set; }
        public List<string> Blocks { get; set; }
    }
}
