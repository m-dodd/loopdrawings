using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class INST_AI_2W_ELMT_4W : INST_AI_2W
    {
        public string TE { get; set; } = string.Empty;
        public INST_AI_2W_ELMT_4W(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap) 
        {
            TE = GetTag(blockMap, tagMap, 1);
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopTagData(Tag);

            Attributes["RANGE"] = data.Range;
            PopulateTag1Tag2(Tag, "TAG1-1", "TAG2-1");
            PopulateTag1Tag2(TE, "TAG2-1", "TAG2-1");
        }

        protected override void FetchExcelData()
        {
            var IOData = dataLoader.GetIOData(Tag)?.Device;
            var TEData = dataLoader.GetIOData(TE)?.Device;

            if (IOData is not null && TEData is not null)
            {
                var AICableData = dataLoader.GetCableData(IOData.CableTag);

                Attributes["TERM1-1"] = IOData.Terminal1;
                Attributes["TERM2-1"] = IOData.Terminal2;
                Attributes["TERM3-1"] = IOData.Terminal3;
                Attributes["TERM4-1"] = IOData.Terminal4;
                Attributes["TERM5-1"] = IOData.Terminal5;
                Attributes["TERM6-1"] = IOData.Terminal6;

                Attributes["CLR1-1"] = IOData.WireColor1;
                Attributes["CLR2-1"] = IOData.WireColor2;

                Attributes["PAIR_NO"] = IOData.CorePair1;
                Attributes["WIRE_TAG_FIELD-1"] = IOData.WireTag1;
                Attributes["CABLE_TAG_FIELD-1"] = IOData.CableTag;
                
                Attributes["CABLE_SIZE-1"] = AICableData?.CableSizeType ?? string.Empty;

            }

            if (TEData is not null)
            {
                var TECableData = dataLoader.GetCableData(TEData.CableTag);

                Attributes["TERM1-2"] = TEData.Terminal1;
                Attributes["TERM2-2"] = TEData.Terminal2;
                Attributes["TERM3-2"] = TEData.Terminal3;
                Attributes["TERM4-2"] = TEData.Terminal4;

                Attributes["CLR1-2"] = TEData.WireColor1;
                Attributes["CLR2-2"] = TEData.WireColor2;
                Attributes["CLR3-2"] = TEData.WireColor3;
                Attributes["CLR4-2"] = TEData.WireColor4;

                Attributes["WIRE_TAG_FIELD-2"] = TEData.WireTag1;
                Attributes["CABLE_TAG_FIELD-2"] = TEData.CableTag;

                Attributes["CABLE_SIZE-2"] = TECableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}
