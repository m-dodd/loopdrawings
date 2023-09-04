using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class INST_AI_3W : INST_AI_2W
    {
        public INST_AI_3W(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap) 
        {
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
                Attributes["CLR1"] = IOData.WireColor1;
                Attributes["CLR2"] = IOData.WireColor2;
                Attributes["CLR3"] = IOData.WireColor3;
                Attributes["TRI_NO"] = IOData.CorePair1;
                Attributes["WIRE_TAG_FIELD"] = IOData.WireTag1;
                Attributes["CABLE_TAG_FIELD"] = IOData.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
            }
        }
    }
}
