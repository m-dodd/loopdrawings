using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class MOD_2_TERM_2_BPCS : BlockModBase
    {
        public string ZSCTag { get; set; } = string.Empty;
        public string ZSOTag { get; set; } = string.Empty;
        public MOD_2_TERM_2_BPCS(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader) 
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = string.Empty;
            ZSCTag = GetTag(blockMap, tagMap, 0);
            ZSOTag = GetTag(blockMap, tagMap, 1);
        }

        protected override void FetchDBData()
        { 
            DBLoopData ZSCData = dataLoader.GetLoopTagData(ZSCTag);
            DBLoopData ZSOData = dataLoader.GetLoopTagData(ZSOTag);

            PopulateRackSlotChannel(ZSCData, "1");
            PopulateFourAlarms(ZSCData, "-1");
            PopulateLoopFields(ZSCData, "FUNCTIONAL_ID1", "LOOP_NO1");

            PopulateRackSlotChannel(ZSOData, "2");
            PopulateFourAlarms(ZSOData, "-2");
            PopulateLoopFields(ZSOData, "FUNCTIONAL_ID2", "LOOP_NO2");

            Attributes["DRAWING_NO"] = ZSCData.PidDrawingNumber;
        }

        protected override void FetchExcelData()
        {
            IExcelIOData<string>? ZSCIOData = dataLoader.GetIOData(ZSCTag);
            IExcelIOData<string>? ZSOIOData = dataLoader.GetIOData(ZSOTag);

            if (ZSCIOData is not null && ZSOIOData is not null)
            {
                Attributes["MOD_TERM1"] = ZSCIOData.ModuleTerm01;
                Attributes["MOD_TERM2"] = ZSCIOData.ModuleTerm02;
                Attributes["MOD_TERM3"] = ZSOIOData.ModuleTerm01;
                Attributes["MOD_TERM4"] = ZSOIOData.ModuleTerm02;
                Attributes["WIRE_TAG_IO-1"] = ZSCIOData.ModuleWireTag01;
                Attributes["WIRE_TAG_IO-2"] = ZSCIOData.ModuleWireTag02;
                Attributes["WIRE_TAG_IO-3"] = ZSOIOData.ModuleWireTag01;
                Attributes["WIRE_TAG_IO-4"] = ZSOIOData.ModuleWireTag02;
            }
        }
    }
}
