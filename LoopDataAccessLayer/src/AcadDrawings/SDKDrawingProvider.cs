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
            if (sdkBlockSize == 0)
            {
                return false;
            }
            else
            {
                return GetNumberOfSDs() > sdkBlockSize;
            }
        }

        private int GetSDKBlockSize()
        {
            Regex regex = new(@"\d+");
            var sdTableBlock = template.BlockMap
                .FirstOrDefault(b => b.Name.Contains("SD_TABLE", StringComparison.OrdinalIgnoreCase));
            
            return
                sdTableBlock != null
                ? int.Parse(regex.Match(sdTableBlock.Name).Value)
                : 0;
        }


        private int GetNumberOfSDs()
        {
            return tagMap.Values.Sum(tag => dataLoader.GetSDs(tag).Count);
        }
    }
}
