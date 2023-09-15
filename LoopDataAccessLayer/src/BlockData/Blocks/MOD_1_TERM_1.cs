using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class MOD_1_TERM_1 : BlockModBase
    {
        public string ControllerTag { get; set; } = string.Empty;

        public MOD_1_TERM_1(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
            ControllerTag = GetTag(blockMap, tagMap, 1);
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopTagData(Tag);
            DBLoopData controllerData = dataLoader.GetLoopTagData(ControllerTag);

            PopulateRackSlotChannel(data);
            PopulateLoopFields(controllerData, "FUNCTIONAL_ID", "LOOP_NO");
            PopulateAlarms(data);

            Attributes["DRAWING_NO"] = data.PidDrawingNumber;
            // Control the visibility of the dynamic block (symbol display)
            //Attributes["SYMBOL_TYPE"] = GetSymbolType(data.SystemType);
            Attributes["Visibility1"] = GetSymbolType(data.SystemType);
        }

        protected override void FetchExcelData()
        {
            IExcelIOData<string>? IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["MOD_TERM"] = IOData.ModuleTerm01;
                Attributes["WIRE_TAG_IO"] = IOData.ModuleWireTag01;
            }
        }
    }
}
