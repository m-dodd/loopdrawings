using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class VALVE_BODY : BlockValveBase
    {
        public VALVE_BODY(
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
            PopulateValveDate(data);
        }
    }

    public class VALVE_ONE_SOL : BlockValveBase
    {
        public string Solenoid { get; set; } = string.Empty; 
        public VALVE_ONE_SOL(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[1]];
            Solenoid = tagMap[blockMap.Tags[0]];
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopTagData(Tag);

            PopulateValveDate(data);
        }
    }

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
            DBLoopData data = dataLoader.GetLoopTagData(Tag);

            PopulateValveDate(data);

            Attributes["SOLTAGBPCS"] = SolenoidBPCS;
            Attributes["SOLTAGSIS"] = SolenoidSIS;
        }
    }
}
