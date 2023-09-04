using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class JB_TERM : BlockJBBase
    {
        public JB_TERM(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
        }

        protected override void FetchExcelData() => PopulateAttributesForSingleJB(isAnalog: false);
    }


    public class JB_TERM_DUAL : BlockJBBase
    {
        public JB_TERM_DUAL(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
        }
        protected override void FetchExcelData() => PopulateAttributesForDualJB(isAnalog: false);
    }


    public class JB_ANALOG_TERM_SINGLE : BlockJBBase
    {
        public JB_ANALOG_TERM_SINGLE(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
        }
        protected override void FetchExcelData() => PopulateAttributesForSingleJB(isAnalog: true);
    }
    
    
    public class JB_ANALOG_TERM_DUAL : BlockJBBase
    {
        public JB_ANALOG_TERM_DUAL(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
        }
        protected override void FetchExcelData() => PopulateAttributesForDualJB(isAnalog: true);
    }


    public class JB_TERM_ISOLATOR : BlockJBBase
    {
        public JB_TERM_ISOLATOR(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
        }

        protected override void FetchExcelData() => PopulateAttributesForSingleJB(isAnalog: false, isIsolator: true);
    }
}
