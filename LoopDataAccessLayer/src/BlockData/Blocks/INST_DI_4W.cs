using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_DI_4W : BlockFieldDeviceBase
    {
        public INST_DI_4W(
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
            PopulateFourLineDescription(data);
            PopulateTag1Tag2();
        }

        protected override void FetchExcelData()
        {
            var IOData = dataLoader.GetIOData(Tag)?.Device;

            if (IOData is not null)
            {
                var cableData = dataLoader.GetCableData(IOData.CableTag);

                Attributes["TERM1"] = IOData.Terminal1;
                Attributes["TERM2"] = IOData.Terminal2;
                Attributes["TERM3"] = IOData.Terminal3;
                Attributes["TERM4"] = IOData.Terminal4;

                Attributes["COND_NO1"] = IOData.CorePair1;
                Attributes["COND_NO2"] = IOData.CorePair2;
                Attributes["COND_NO3"] = IOData.CorePair3;
                Attributes["COND_NO4"] = IOData.CorePair4;


                Attributes["WIRE_TAG_FIELD1"] = IOData.WireTag1;
                Attributes["WIRE_TAG_FIELD2"] = IOData.WireTag2;
                Attributes["WIRE_TAG_FIELD3"] = IOData.WireTag3;
                Attributes["WIRE_TAG_FIELD4"] = IOData.WireTag4;

                Attributes["CABLE_TAG_FIELD"] = IOData.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}