using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_DI_4W_ZS : BlockFieldDeviceBase
    {
        public string ZSOTag { get; set; } = string.Empty;
        public string ZSCTag { get; set; } = string.Empty;
        
        public INST_DI_4W_ZS(
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

        protected override void FetchDBData()
        {
            DBLoopData dataZSC = dataLoader.GetLoopTagData(ZSOTag);
            DBLoopData dataZSO = dataLoader.GetLoopTagData(ZSOTag);
            PopulateFourLineDescription(dataZSC);
            
            // we need to strip the C or O from teh ZSC upper tag
            string[] tagComponents = GetTag1Tag2(ZSCTag);
            if (tagComponents.Length == 2)
            {
                PopulateTag1Tag2(tagComponents[0][..^1], tagComponents[1]);
            }
        }

        protected override void FetchExcelData()
        {
            var ZSCIOData = dataLoader.GetIOData(ZSCTag)?.Device;
            var ZSOIOData = dataLoader.GetIOData(ZSOTag)?.Device;

            if (ZSCIOData is not null && ZSOIOData is not null)
            {
                var cableData = dataLoader.GetCableData(ZSCIOData.CableTag);

                Attributes["TERM1"] = ZSCIOData.Terminal1;
                Attributes["TERM2"] = ZSCIOData.Terminal2;
                Attributes["TERM3"] = ZSOIOData.Terminal1;
                Attributes["TERM4"] = ZSOIOData.Terminal2;

                Attributes["COND_NO1"] = ZSCIOData.CorePair1;
                Attributes["COND_NO2"] = ZSCIOData.CorePair2;
                Attributes["COND_NO3"] = ZSOIOData.CorePair1;
                Attributes["COND_NO4"] = ZSOIOData.CorePair2;


                Attributes["WIRE_TAG_FIELD1"] = ZSCIOData.WireTag1;
                Attributes["WIRE_TAG_FIELD2"] = ZSCIOData.WireTag2;
                Attributes["WIRE_TAG_FIELD3"] = ZSOIOData.WireTag1;
                Attributes["WIRE_TAG_FIELD4"] = ZSOIOData.WireTag2;

                Attributes["CABLE_TAG_FIELD"] = ZSCIOData.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}