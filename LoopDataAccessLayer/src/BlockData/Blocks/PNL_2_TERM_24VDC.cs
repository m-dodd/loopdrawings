using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class PNL_2_TERM_24VDC : PNL_3_or_4_TERM_24VDC
    {
        public PNL_2_TERM_24VDC(
            ILogger logger,
            IDataLoader dataLoader, 
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap) { }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();

            IExcelIOData<string>? IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes.Remove("WIRE_TAG_PANEL");
                Attributes["WIRE_TAG_PANEL1"] = IOData.IO.WireTag1;
                Attributes["WIRE_TAG_PANEL2"] = IOData.IO.WireTag2;
                Attributes["COND_NO1"] = IOData.IO.CorePair1;
                Attributes["COND_NO2"] = IOData.IO.CorePair2;
            }
        }
    }
}
