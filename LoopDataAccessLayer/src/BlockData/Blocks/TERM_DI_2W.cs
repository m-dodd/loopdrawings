using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class TERM_DI_2W : BlockDataExcel
    {
        public string ValveTag { get; set; } = string.Empty;

        public TERM_DI_2W(
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
            var IOFieldData = dataLoader.GetIOData(Tag)?.Device;

            if (IOFieldData is not null)
            {
                var cableData = dataLoader.GetCableData(IOFieldData.CableTag);

                Attributes["EQUIP_TAG"] = IOFieldData.PanelTag;
                Attributes["SIG_TAG"] = Tag;
                Attributes["EQUIP_TS"] = IOFieldData.PanelTerminalStrip;

                Attributes["TERM1"] = IOFieldData.Terminal1;
                Attributes["TERM2"] = IOFieldData.Terminal2;
                Attributes["COND_NO1"] = IOFieldData.CorePair1;
                Attributes["COND_NO2"] = IOFieldData.CorePair2;
                Attributes["WIRE_TAG_FIELD1"] = IOFieldData.WireTag1;
                Attributes["WIRE_TAG_FIELD2"] = IOFieldData.WireTag2;
                Attributes["CABLE_TAG_FIELD"] = IOFieldData.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}
