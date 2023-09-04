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
    public class EMPTY_BLOCK : BlockDataMappable
    {
        public EMPTY_BLOCK(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }
        public override void MapData() { }
    }
}
