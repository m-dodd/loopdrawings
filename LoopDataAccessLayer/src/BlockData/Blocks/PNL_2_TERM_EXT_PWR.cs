using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class PNL_2_TERM_EXT_PWR : BlockDataExcel
    {
        public PNL_2_TERM_EXT_PWR(
            IDataLoader dataLoader, 
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
        }

        protected override void FetchExcelData()
        {
            IExcelIOData<string>? IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                var cableData = dataLoader.GetCableData(IOData.PowerCable);

                Attributes["PNL_TAG"] = IOData.PanelTag;
                Attributes["PNL_TS"] = IOData.PowerTerminalStrip;
                Attributes["CB1"] = IOData.PowerTerm1;
                Attributes["TB1"] = IOData.PowerTerm2;
                Attributes["COND_NO1"] = IOData.PowerCore1;
                Attributes["COND_NO2"] = IOData.PowerCore2;
                Attributes["WIRE_TAG_PANEL1"] = IOData.PowerWireTag1;
                Attributes["WIRE_TAG_PANEL2"] = IOData.PowerWireTag2;
                Attributes["CABLE_TAG_PANEL"] = IOData.PowerCable;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
                Attributes["VOLTS"] = IOData.PowerVolts;
            }
        }
    }
}
