using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class JB_ANALOG_TERM_SINGLE : BlockJBBase
    {
        public JB_ANALOG_TERM_SINGLE(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader, blockMap, tagMap)
        {
        }
        protected override void FetchExcelData() =>  PopulateAttributesForSingleJB(isAnalog: true);
    }
}
