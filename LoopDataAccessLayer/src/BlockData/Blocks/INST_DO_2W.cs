﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_DO_2W : BlockFieldDeviceBase
    {
        public string ValveTag { get; set; } = string.Empty;

        public INST_DO_2W(
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
                Attributes["COND_NO1"] = IOData.CorePair1;
                Attributes["COND_NO2"] = IOData.CorePair2;
                Attributes["WIRE_TAG_FIELD1"] = IOData.WireTag1;
                Attributes["WIRE_TAG_FIELD2"] = IOData.WireTag2;
                Attributes["CABLE_TAG_FIELD"] = IOData.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}
