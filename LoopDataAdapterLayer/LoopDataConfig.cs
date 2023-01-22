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
        private string configFile;

        public string TemplateDrawingPath { get; set; } = string.Empty;
        public Dictionary<string, TemplateConfig> TemplateDefs { get; set; }

        public LoopDataConfig()
        {
            this.configFile= string.Empty;
        }

        public LoopDataConfig(string configFile)
        {
            this.configFile= configFile;
        }


        public void LoadConfig()
        {
            var json = File.ReadAllText(this.configFile);
            TemplateDefs =  JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json);
        }

        public void LoadConfig(string configFile)
        {
            var json = File.ReadAllText(configFile);
            TemplateDefs =  JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json);
        }

        public static LoopDataConfig FromJSonFile(string configFile)
        {
            var json = File.ReadAllText(configFile);
            return JsonConvert.DeserializeObject<LoopDataConfig>(json);
        }
    }

    public class TemplateConfig
    {
        public string TemplateName { get; set; }
        public string TemplateFileName { get; set; }
        public List<BlockMapData> BlockMap { get; set; }
    }

    public class BlockMapData
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
