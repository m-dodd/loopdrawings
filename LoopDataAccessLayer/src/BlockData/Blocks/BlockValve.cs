using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockValve: BlockDataDB
    {
        public BlockValve(IDataLoader dataLoader) : base(dataLoader) { }

        protected static string GetValveType(string instrument)
        {
            var instrumentSplit = instrument.Split('-');
            return
                ((instrumentSplit.Length > 1) ? instrumentSplit[1].ToUpper() : instrumentSplit[0].ToUpper())
                + "_DIAPHRAGM";
        }
        
    }
}
