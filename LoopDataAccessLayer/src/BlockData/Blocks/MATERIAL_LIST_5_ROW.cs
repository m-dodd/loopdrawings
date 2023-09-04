using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class MATERIAL_LIST_5_ROW : BlockDataDB
    {
        public Dictionary<string, string> TagMap { get; set; } = new Dictionary<string, string>();
        public List<string> TagTypeList {get; set; } = new List<string>();
        public MATERIAL_LIST_5_ROW(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader) 
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            TagMap = tagMap;
            TagTypeList = blockMap.Tags;
        }

        protected override void FetchDBData()
        {
            // might be able to just change this to the values
            int i = 1;
            foreach (string tagType in TagTypeList)
            {
                if (i <= 5)
                {

                    DBLoopData data = dataLoader.GetLoopTagData(TagMap[tagType]);
                    string iStr = i.ToString("D2");
                    Attributes["TAG_" + iStr] = data.Tag;
                    Attributes["MFR_" + iStr] = data.Manufacturer;
                    Attributes["MODEL_" + iStr] = data.Model;
                    i++;
                }
            }
        }
    }
}
