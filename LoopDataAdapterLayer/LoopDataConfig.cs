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
        public string DrawingFilename { get; set; }
        public List<string> TagTypes;
        public List<BlockMapData> BlockMap { get; set; }
    }

    public class BlockMapData
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
