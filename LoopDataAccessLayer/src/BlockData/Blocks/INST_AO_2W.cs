using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_AO_2W : INST_AI_2W
    {
        public string ValveTag { get; set; } = string.Empty;

        public INST_AO_2W(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            base.FetchDBData();
            DBLoopData valveData = dataLoader.GetLoopData(ValveTag);

            Attributes.Remove("RANGE");
            Attributes["VALVE_FAIL"] = valveData.FailPosition;

        }

        //protected override void FetchExcelData()
        //{
        //    base.FetchExcelData();
        //}
    }
}
