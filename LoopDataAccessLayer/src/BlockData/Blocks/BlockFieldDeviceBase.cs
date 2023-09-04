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
    public abstract class BlockFieldDeviceBase : BlockDataExcelDB
    {
        public BlockFieldDeviceBase(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

        protected void PopulateFourLineDescription(IDBLoopData data)
        {
            IEnumerable<string> descriptions = data.FourLineDescription;
            for (int i = 0; i < 4; i++)
            {
                Attributes["DESCRIPTION_LINE" + (i + 1).ToString()] = descriptions.ElementAt(i);
            }
        }
    }
}
