using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class MOD_2_TERM_1_DISCRETE : BlockModBase
    {
        public MOD_2_TERM_1_DISCRETE(
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
            DBLoopData data = dataLoader.GetLoopData(Tag);

            PopulateRackSlotChannel(data);
            PopulateTag1Tag2(Tag, "FUNCTIONAL_ID", "LOOP_NO");
            PopulateAlarms(data);

            Attributes["DRAWING_NO"] = data.PidDrawingNumber;
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
