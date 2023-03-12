using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_AO_2W : INST_AI_2W
    {
        public string ValveTag { get; set; } = string.Empty;

        public INST_AO_2W(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader, blockMap, tagMap)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
            ValveTag = tagMap[blockMap.Tags[1]];
        }

        protected override void FetchDBData()
        {
            base.FetchDBData();
            DBLoopData valveData = dataLoader.GetLoopData(ValveTag);

            Attributes.Remove("RANGE");
            Attributes["VALVE_FAIL"] = valveData.FailPosition;

        }
    }
}
