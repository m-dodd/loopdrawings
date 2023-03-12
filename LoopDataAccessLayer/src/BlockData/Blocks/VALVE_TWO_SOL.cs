using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class VALVE_TWO_SOL : BlockValveBase
    {
        public string SolenoidBPCS { get; set; } = string.Empty;
        public string SolenoidSIS { get; set; } = string.Empty;
        public VALVE_TWO_SOL(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[2]];
            SolenoidBPCS = tagMap[blockMap.Tags[0]];
            SolenoidSIS = tagMap[blockMap.Tags[1]];
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            PopulateValveDate(data);

            Attributes["SOLTAGBPCS"] = SolenoidBPCS;
            Attributes["SOLTAGSIS"] = SolenoidSIS;
        }
    }
}
