using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LoopDataAccessLayer;
using Serilog;

namespace LoopDataAccessLayer
{
    public class LoopDataConfig
    {
        private readonly string configDirectory;
        private readonly ILogger logger;

        public string TemplateDrawingPath { get; set; } = string.Empty;
        public string OutputDrawingPath { get; set; } = string.Empty;
        public string SiteID { get; set; } = string.Empty;

        public Dictionary<string, TemplateConfig> TemplateDefs { get; private set; }


        public LoopDataConfig(ILogger logger, string configDirectory)
        {
            this.configDirectory = configDirectory;
            this.logger = logger;
            TemplateDefs = new Dictionary<string, TemplateConfig>(StringComparer.OrdinalIgnoreCase);
        }

        public void LoadConfig()
        {
            try
            {
                TemplateDefs = ReadAllTemplateConfigFiles();
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while loading config files: {ex.Message}");
                throw new LoopDataException($"An error occurred while loading config files", ex);
            }
        }

        private Dictionary<string, TemplateConfig> ReadAllTemplateConfigFiles()
        {
            var templateConfigsList = new List<Dictionary<string, TemplateConfig>>();
            foreach (var file in Directory.GetFiles(configDirectory, "*.json"))
            {
                try
                {
                    var templates = ReadTemplateConfigFile(file);
                    templateConfigsList.Add(templates);
                }
                catch (Exception ex)
                {
                    logger.Error($"Error reading template config file {file}: {ex}");
                    throw new LoopDataException("Unexpected error", ex);
                }
            }

            var mergedTemplateConfigs = templateConfigsList
                .SelectMany(dict => dict)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value,
                    StringComparer.OrdinalIgnoreCase);

            return mergedTemplateConfigs;
        }
        private Dictionary<string, TemplateConfig> ReadTemplateConfigFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json);
            config ??= new Dictionary<string, TemplateConfig>();
            var lookupBaseKey = Path.GetFileNameWithoutExtension(filePath);
            if (config.TryGetValue(lookupBaseKey, out var baseTemplate))
            {
                if (config.TryGetValue("COMMON_BLOCKS", out var commonTemplate))
                {
                    var commonBlocks = commonTemplate.BlockMap;
                    foreach (var template in config.Values)
                    {
                        if (template == baseTemplate)
                            continue;

                        template.BlockMap ??= new List<BlockMapData>();

                        template.BlockMap.AddRange(commonBlocks);
                    }
                    config.Remove("COMMON_BLOCKS");
                }
            }
            return config;
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

    public class LoopDataException : Exception
    {
        public LoopDataException() : base() { }
        public LoopDataException(string message) : base(message) { }
        public LoopDataException(string message, Exception innerException) : base(message, innerException) { }
    }

}
