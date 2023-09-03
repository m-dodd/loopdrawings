using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_AI_2W : BlockFieldDeviceBase
    {
        public INST_AI_2W(
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

            Attributes["RANGE"] = data.Range;
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
                Attributes["CLR1"] = IOData.WireColor1;
                Attributes["CLR2"] = IOData.WireColor2;
                Attributes["PAIR_NO"] = IOData.CorePair1;
                Attributes["WIRE_TAG_FIELD"] = IOData.WireTag1;
                Attributes["CABLE_TAG_FIELD"] = IOData.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}
