using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public abstract class BlockValveBase: BlockDataDB
    {
        public BlockValveBase(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

        protected void PopulateValveDate(DBLoopData data)
        {
            PopulateTag1Tag2();
            Attributes["SIZE/FAIL_POSITION"] = data.FailPosition;
            Attributes["VALVE_TYPE"] = GetValveType(data.InstrumentType);
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
}
