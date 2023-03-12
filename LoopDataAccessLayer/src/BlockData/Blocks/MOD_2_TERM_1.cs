using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class MOD_2_TERM_1 : MOD_1_TERM_1
    {
        public string AITag { get; set; } = string.Empty;
        public MOD_2_TERM_1(IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(dataLoader, blockMap, tagMap)
        {
            AITag = tagMap[blockMap.Tags[2]];
        }

        protected override void FetchDBData()
        { 
            base.FetchDBData();
            DBLoopData dataAI = dataLoader.GetLoopData(AITag);
            PopulateAlarms(dataAI);
        }

        protected override void FetchExcelData()
        {
            IExcelIOData<string>? IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["MOD_TERM1"] = IOData.ModuleTerm01;
                Attributes["MOD_TERM2"] = IOData.ModuleTerm02;
                Attributes["WIRE_TAG_IO-1"] = IOData.ModuleWireTag01;
                Attributes["WIRE_TAG_IO-2"] = IOData.ModuleWireTag02;
            }
        }
    }
}
