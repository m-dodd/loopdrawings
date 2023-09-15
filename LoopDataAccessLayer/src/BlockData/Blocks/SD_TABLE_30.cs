using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class SD_TABLE_30 : SD_TABLE
    {
        public SD_TABLE_30(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap["SDK_TAG"];
        }

        protected override List<SDKData> GetSDData()
        {
            List<SDKData> sdAllData = base.GetSDData();
            List<SDKData> sdBlockData;

            int blockNumber = int.Parse(UID[^1].ToString());
            if (blockNumber == 1)
            {
                sdBlockData = sdAllData.Take(30).ToList();
            }
            else if (blockNumber == 2)
            {
                sdBlockData = sdAllData.Skip(30).Take(30).ToList();
            }
            else
            {
                throw new NotImplementedException("SD Table should have EXACTLY two blocks.");
            }

            return sdBlockData;
        }
    }
}
