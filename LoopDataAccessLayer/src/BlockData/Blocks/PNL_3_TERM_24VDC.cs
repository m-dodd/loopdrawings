using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class PNL_3_TERM_24VDC : BlockDataExcel
    {
        public PNL_3_TERM_24VDC(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["PNL_TAG"] = IOData.PanelTag;
                Attributes["PNL_TS"] = IOData.PanelTerminalStrip;
                Attributes["TB1"] = IOData.IO.TerminalPlus;
                Attributes["TB2"] = IOData.IO.TerminalNeg;
                Attributes["TB3"] = IOData.IO.TerminalShld;
                Attributes["CLR1"] = IOData.IO.WireColorPlus;
                Attributes["CLR2"] = IOData.IO.WireColorNeg;
                Attributes["PAIR_NO"] = IOData.IO.CorePairPlus + "PR";
                Attributes["WIRE_TAG_PANEL"] = IOData.IO.WireTagPlus;
                Attributes["CABLE_TAG_PANEL"] = IOData.IO.CableTag;
                Attributes["BREAKER_NO"] = IOData.BreakerNumber;
            }
        }
    }
}
