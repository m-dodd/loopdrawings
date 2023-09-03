using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class MOD_2_BPCS_ETH : BlockModBase
    {
        public string ControllerTag { get; set; } = string.Empty;

        public MOD_2_BPCS_ETH(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopTagData(Tag);
            PopulateLoopFields(data, "FUNCTIONAL_ID1", "LOOP_NO1");
            PopulateLoopFields(data, "FUNCTIONAL_ID2", "LOOP_NO2");
        }

        protected override void FetchExcelData()
        {
            Attributes["SIG1-1"] = "RN";
            Attributes["SIG1-2"] = "ST";

            // now just clear out the other attribute lines
            for(int i=1; i<=2; i++)
            {
                for(int j=2; j<=4; j++)
                {
                    string att = $"SIG{j}-{i}";
                    Attributes[att] = string.Empty;
                }
            }
        }
    }
}
