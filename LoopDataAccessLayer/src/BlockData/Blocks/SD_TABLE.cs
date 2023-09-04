using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class SD_TABLE : BlockDataDB
    {
        protected bool deleteSDTable;
        protected string sdDrawingName;
        protected BlockMapData blockMap;
        protected Dictionary<string, string> tagMap;
        public SD_TABLE(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader) 
        {
            this.blockMap = blockMap;
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
            deleteSDTable = Boolean.Parse(tagMap["DELETE_SD"]);
            sdDrawingName = tagMap["DRAWING_NAME_SD"];
            this.tagMap = tagMap;

        }

        protected override void FetchDBData()
        {
            List<SDKData> sdBlockData = GetSDData();

            Attributes["DELETE_SD"] = deleteSDTable.ToString();
            Attributes["DRAWING_NAME_SD"] = sdDrawingName;
            if (!deleteSDTable)
            {
                for (int i = 0; i < sdBlockData.Count; i++)
                {
                    string sdNumString = (i + 1).ToString("D2");
                    Attributes["INPUT_TAG_" + sdNumString] = sdBlockData[i].InputTag;
                    Attributes["OUTPUT_TAG_" + sdNumString] = sdBlockData[i].OutputTag;
                    Attributes["DESCRIPTION_" + sdNumString] = sdBlockData[i].OutputDescription;
                    Attributes["ACTION_" + sdNumString + "A"] = sdBlockData[i].SdAction1;
                    Attributes["ACTION_" + sdNumString + "B"] = sdBlockData[i].SdAction2;
                }
            }
        }

        protected virtual List<SDKData> GetSDData()
        {
            List<string> tags = blockMap.Tags.Select(tagType => tagMap[tagType]).ToList();
            
            return GetSDData(tags);
        }

        protected virtual List<SDKData> GetSDData(IEnumerable<string> tags)
        {
            return tags.SelectMany(tag => dataLoader.GetSDs(tag)).ToList();
        }
    }
}
