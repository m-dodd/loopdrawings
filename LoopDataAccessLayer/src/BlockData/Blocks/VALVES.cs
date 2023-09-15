using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public abstract class BlockValveBase : BlockDataDB
    {
        public BlockValveBase(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

        protected void PopulateValveDate(DBLoopData data)
        {
            PopulateTag1Tag2();
            Attributes["SIZE/FAIL_POSITION"] = data.FailPosition;
            // control the visibility of the dynamic block valve display
            Attributes["Visibility1"] = GetValveType(data.InstrumentType);
        }

        protected static string GetValveType(string instrument)
        {
            Regex rg = new(@"ball|gate|globe|butterfly", RegexOptions.IgnoreCase);
            Match m = rg.Match(instrument);

            if (m.Success)
            {
                return m.Value.ToUpper() + "_DIAPHRAGM";
            }
            else
            {
                throw new NotImplementedException(instrument.ToUpper() + " has not been implemented.");
            }
        }
    }


    public class VALVE_BODY : BlockValveBase
    {
        public VALVE_BODY(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
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
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 1);
            Solenoid = GetTag(blockMap, tagMap, 0);
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
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 2);
            SolenoidBPCS = GetTag(blockMap, tagMap, 0);
            SolenoidSIS = GetTag(blockMap, tagMap, 1);
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
