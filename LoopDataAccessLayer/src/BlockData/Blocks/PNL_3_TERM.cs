using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class PNL_3_TERM : PNL_3_TERM_24VDC
    {
        public PNL_3_TERM(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
            Attributes.Remove("BREAKER_NO");
        }
    }
}
