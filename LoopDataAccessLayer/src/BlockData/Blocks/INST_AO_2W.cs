using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class INST_AO_2W : INST_AI_2W
    {
        public string ValveTag { get; set; } = string.Empty;

        public INST_AO_2W(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
            ValveTag = GetTag(blockMap, tagMap, 1);
        }

        protected override void FetchDBData()
        {
            base.FetchDBData();
            DBLoopData valveData = dataLoader.GetLoopTagData(ValveTag);

            Attributes.Remove("RANGE");
            Attributes["VALVE_FAIL"] = valveData.FailPosition;

        }
    }
}
