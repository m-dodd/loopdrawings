using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class MOD_2_TERM_1_BPCS : MOD_1_TERM_1_BPCS
    {
        public string AITag { get; set; } = string.Empty;
        public MOD_2_TERM_1_BPCS(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        { 
            base.FetchDBData();
            DBLoopData dataAI = dataLoader.GetLoopData(AITag);

            Attributes["ALARM1"] = dataAI.HiHiAlarm;
            Attributes["ALARM2"] = dataAI.HiAlarm;
            Attributes["ALARM3"] = dataAI.LoAlarm;
            Attributes["ALARM4"] = dataAI.LoLoAlarm;
            Attributes["ALARM5"] = dataAI.HiControl;
            Attributes["ALARM6"] = dataAI.LoControl;
        }

        protected override void FetchExcelData()
        {
            var IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["MOD_TERM1"] = IOData.ModuleTerm01;
                Attributes["MOD_TERM2"] = IOData.ModuleTerm02;
                Attributes["WIRE_TAG_IO-1"] = IOData.ModuleWireTag01;
                Attributes["WIRE_TAG_IO-2"] = IOData.ModuleWireTag02;
            }
        }
    }
}
