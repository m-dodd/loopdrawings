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
            var mergedTemplateConfigs = new Dictionary<string, TemplateConfig>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in Directory.GetFiles(configDirectory, "*.json"))
            {
                try
                {
                    var templates = ReadTemplateConfigFile(file);
                    mergedTemplateConfigs.AddRange(templates, logger.Warning);
                }
                catch (Exception ex)
                {
                    logger.Error($"Error reading template config file {file}: {ex}");
                    throw new LoopDataException("Unexpected error", ex);
                }
            }
            return mergedTemplateConfigs;
        }

        private Dictionary<string, TemplateConfig> ReadTemplateConfigFile(string filePath)
        {
            var fileNameForDebugging = Path.GetFileName(filePath);
            logger.Debug($"Reading config data from {fileNameForDebugging}");

            try
            {
                string json = File.ReadAllText(filePath).ToUpper();
                var jsonData = JsonConvert.DeserializeObject<Dictionary<string, TemplateConfig>>(json);

                var config = jsonData != null
                    ? new Dictionary<string, TemplateConfig>(jsonData, StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, TemplateConfig>();

                string lookupBaseKey = Path.GetFileNameWithoutExtension(filePath).ToUpper();
                logger.Debug($"Lookup key is {lookupBaseKey}");

                ProcessCommonBlocks(config, lookupBaseKey);

                return config;
            }
            catch (JsonReaderException ex)
            {
                string msg = $"Error trying to deserialize '{fileNameForDebugging}': {ex}";
                logger.Error(msg);
                throw new LoopDataException(msg, ex);
            }
        }

        private void ProcessCommonBlocks(Dictionary<string, TemplateConfig> config, string lookupBaseKey)
        {
            if (config.TryGetValue("COMMON_BLOCKS", out var commonTemplate))
            {
                config.Remove("COMMON_BLOCKS");

                if (config.TryGetValue(lookupBaseKey, out var baseTemplate))
                {
                    var commonBlocks = commonTemplate.BlockMap;
                    foreach (var template in config.Values.Where(template => template != baseTemplate))
                    {
                        template.BlockMap ??= new List<BlockMapData>();
                        template.BlockMap.AddRange(commonBlocks);
                    }
                }
                else
                {
                    logger.Debug("Base key was found, but no COMMON_BLOCKS");
                }
            }
            else
            {
                logger.Debug("Base key was not found");
            }

        }
    }


    public class TemplateConfig
    {
        public string TemplateName { get; set; } = string.Empty;
        public string TemplateFileName { get; set; } = string.Empty;
        public bool TemplateRequiresTwoDrawings { get; set; } = false;
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
