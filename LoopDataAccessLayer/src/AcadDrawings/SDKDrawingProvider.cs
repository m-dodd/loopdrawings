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
        LoopNoTemplatePair loopData;
        List<SDKData> loopSDs;

        public SDKDrawingProvider(
            IDataLoader dataLoader,
            TemplateConfig template,
            LoopNoTemplatePair loopData
            )
        {
            this.dataLoader = dataLoader;
            this.template = template;
            this.loopData = loopData;
            loopSDs = new List<SDKData>();
        }

        public bool NewDrawingRequired()
        {
            // the following code doesn't work for drawings like the XV drawing as that doesn't have a table on it
            // but let's start with this and get it to work and then handle the XV drawing edge case
            int sdkBlockSize = GetSDKBlockSize();
            int numberOfSDs = GetNumberOfSDs();
            return numberOfSDs > sdkBlockSize;
        }
        public string GetSDTag()
        {
            // it no longer matters what tag is returned here as long as a tag is returned - it is used to build the drawing description
            this.loopSDs = dataLoader.GetSDsForLoop(loopData.LoopNo);
            return this.loopSDs.First().ParentTag;
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
            this.loopSDs = dataLoader.GetSDsForLoop(loopData.LoopNo);
            return this.loopSDs.Count;
        }

        private BlockMapData? GetSDBlock()
        {
            BlockMapData? sdTableBlock = template.BlockMap
                .FirstOrDefault(b => b.Name.Contains("SD_TABLE", StringComparison.OrdinalIgnoreCase));
            return sdTableBlock;
        }
    }
}
