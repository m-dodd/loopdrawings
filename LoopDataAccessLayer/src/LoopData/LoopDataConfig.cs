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

        public Dictionary<string, TemplateConfig> TemplateDefs { get; set; }
            


        public LoopDataConfig()
        {
            this.configFile= string.Empty;
            TemplateDefs = new Dictionary<string, TemplateConfig>(StringComparer.OrdinalIgnoreCase);
        }

        public LoopDataConfig(string configFile)
        {
            this.configFile = configFile;
            TemplateDefs = new Dictionary<string, TemplateConfig>(StringComparer.OrdinalIgnoreCase);
        }

        public void LoadConfig()
        {
            LoadConfig(this.configFile);
        }

        public void LoadConfig(string configFile)
        {
            var json = File.ReadAllText(configFile);
            var tempDefinitions =
                JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json)
                ?? new Dictionary<string, TemplateConfig>(StringComparer.OrdinalIgnoreCase);
            TemplateDefs = new Dictionary<string, TemplateConfig>(tempDefinitions, StringComparer.OrdinalIgnoreCase);
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
        public string UID { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
}
