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
            TemplateDefs = JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json);
        }
    }

    public class TemplateConfig
    {
        public string TemplateName { get; set; }
        public string Filename { get; set; }
        public Dictionary<string, string> BlockMap { get; set; }
    }
}
