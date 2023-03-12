using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class MOD_1_TERM_1 : BlockModBase
    {
        public string ControllerTag { get; set; } = string.Empty;

        public MOD_1_TERM_1(IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
            ControllerTag = tagMap[blockMap.Tags[1]];
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            PopulateRackSlotChannel(data);
            PopulateTag1Tag2(ControllerTag, "FUNCTIONAL_ID", "LOOP_NO");
            PopulateAlarms(data);

            Attributes["DRAWING_NO"] = data.PidDrawingNumber;
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
