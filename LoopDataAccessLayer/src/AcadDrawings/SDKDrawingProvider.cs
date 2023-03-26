using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class SDKDrawingProvider
    {
        IDataLoader dataLoader;
        TemplateConfig template;
        IDictionary<string, string> tagMap;
        LoopDataConfig loopConfig;

        public SDKDrawingProvider(
            IDataLoader dataLoader,
            TemplateConfig template,
            IDictionary<string, string> tagMap,
            LoopDataConfig loopConfig
            )
        {
            this.dataLoader = dataLoader;
            this.template = template;
            this.tagMap = tagMap;
            this.loopConfig = loopConfig;
        }

        public bool NewDrawingRequired()
        {
            // the following code doesn't work for drawings like the XV drawing as that doesn't have a table on it
            // but let's start with this and get it to work and then handle the XV drawing edge case
            int sdkBlockSize = GetSDKBlockSize();
            sdkBlockSize--; // for testing purposes
            return sdkBlockSize != 0 && GetNumberOfSDs() > sdkBlockSize;
        }

        public string GetSDTag()
        {
            var sdTableBlock = GetSDBlock();
            return
                sdTableBlock is not null
                ? tagMap[sdTableBlock.Tags[0]]
                : string.Empty;
        }

        private int GetSDKBlockSize()
        {
            var sdTableBlock = GetSDBlock();
            Regex regex = new(@"\d+");

            return
                sdTableBlock is not null
                ? int.Parse(regex.Match(sdTableBlock.Name).Value)
                : 0;
        }

        private int GetNumberOfSDs()
        {
            string tag = GetSDTag();
            return string.IsNullOrEmpty(tag)
                   ? 0
                   : dataLoader.GetSDs(tag).Count;
        }

        private BlockMapData? GetSDBlock()
        {
            BlockMapData? sdTableBlock = template.BlockMap
                .FirstOrDefault(b => b.Name.Contains("SD_TABLE", StringComparison.OrdinalIgnoreCase));
            return sdTableBlock;
        }
    }
}
