using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class PNL_4_TERM_24VDC : BlockDataExcel
    {
        public string ZSOTag { get; set; } = string.Empty;
        public string ZSCTag { get; set; } = string.Empty;

        public PNL_4_TERM_24VDC(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = string.Empty;
            ZSCTag = tagMap[blockMap.Tags[0]];
            ZSOTag = tagMap[blockMap.Tags[1]];
        }


        protected override void FetchExcelData()
        {
            var ZSCIOData = dataLoader.GetIOData(ZSCTag);
            var ZSOIOData = dataLoader.GetIOData(ZSOTag);

            if (ZSCIOData is not null && ZSOIOData is not null)
            {
                var cableData = dataLoader.GetCableData(ZSCIOData.IO.CableTag);

                Attributes["PNL_TAG"] = ZSCIOData.PanelTag;
                Attributes["PNL_TS"] = ZSCIOData.PanelTerminalStrip;

                Attributes["TB1"] = ZSCIOData.IO.Terminal1;
                Attributes["TB2"] = ZSCIOData.IO.Terminal2;
                Attributes["TB3"] = ZSOIOData.IO.Terminal1;
                Attributes["TB4"] = ZSOIOData.IO.Terminal2;

                Attributes["COND_NO1"] = ZSCIOData.IO.CorePair1;
                Attributes["COND_NO2"] = ZSCIOData.IO.CorePair2;
                Attributes["COND_NO3"] = ZSOIOData.IO.CorePair1;
                Attributes["COND_NO4"] = ZSOIOData.IO.CorePair2;

                Attributes["WIRE_TAG_PANEL1"] = ZSCIOData.IO.WireTag1;
                Attributes["WIRE_TAG_PANEL2"] = ZSCIOData.IO.WireTag1;
                Attributes["WIRE_TAG_PANEL3"] = ZSOIOData.IO.WireTag1;
                Attributes["WIRE_TAG_PANEL4"] = ZSOIOData.IO.WireTag1;

                Attributes["CABLE_TAG_PANEL"] = ZSCIOData.IO.CableTag;
                Attributes["BREAKER_NO"] = ZSCIOData.BreakerNumber;

                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}