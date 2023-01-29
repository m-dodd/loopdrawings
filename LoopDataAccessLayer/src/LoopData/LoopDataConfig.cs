using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LoopDataAccessLayer;

namespace LoopDataAccessLayer
{
    public class LoopDataConfig
    {
        private readonly string configFile;

        public string TemplateDrawingPath { get; set; } = string.Empty;
        public string OutputDrawingPath { get; set; } = string.Empty;
        public string SiteID { get; set; } = string.Empty;

        public Dictionary<string, TemplateConfig> TemplateDefs { get; set; } = 
            new Dictionary<string, TemplateConfig>();


        public LoopDataConfig()
        {
            this.configFile= string.Empty;
        }

        public LoopDataConfig(string configFile)
        {
            this.configFile = configFile;
        }


        public void LoadConfig()
        {
            var json = File.ReadAllText(this.configFile);
            TemplateDefs = 
                JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json) ??
                new Dictionary<string, TemplateConfig>();
        }

        public void LoadConfig(string configFile)
        {
            var json = File.ReadAllText(configFile);
            TemplateDefs =
                JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json) ??
                new Dictionary<string, TemplateConfig>();
        }
    }

    public class TemplateConfig
    {
        public string TemplateName { get; set; } = string.Empty;
        public string TemplateFileName { get; set; } = string.Empty;
        public List<BlockMapData> BlockMap { get; set; } = new List<BlockMapData>();
    }

    public class BlockMapData
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
}
