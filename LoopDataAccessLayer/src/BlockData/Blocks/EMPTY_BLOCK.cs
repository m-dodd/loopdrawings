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
    public class EMPTY_BLOCK : BlockDataMappable
    {
        public EMPTY_BLOCK(IDataLoader dataLoader) : base(dataLoader) { }
        public override void MapData() { }
    }
}
