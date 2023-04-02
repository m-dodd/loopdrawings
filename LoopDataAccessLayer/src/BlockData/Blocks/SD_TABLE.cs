using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class SD_TABLE : BlockDataDB
    {
        protected bool deleteSDTable;
        protected string sdDrawingName;
        protected BlockMapData blockMap;
        public SD_TABLE(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader) 
        {
            this.blockMap = blockMap;
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
            deleteSDTable = Boolean.Parse(tagMap["DELETE_SD"]);
            sdDrawingName = tagMap["DRAWING_NAME_SD"];

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
            List<List<SDKData>> allSDs = new();
            foreach(string tag in blockMap.Tags)
            {
                List<SDKData> sds = dataLoader.GetSDs(Tag);
                allSDs.Add(sds);
            }
            //return dataLoader.GetSDs(Tag);
            return allSDs.SelectMany(list => list).ToList();
        }
    }
}
