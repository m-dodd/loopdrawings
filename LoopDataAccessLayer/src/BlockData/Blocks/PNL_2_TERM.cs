﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class PNL_2_TERM : PNL_2_TERM_24VDC
    {
        public PNL_2_TERM(
            ILogger logger,
            IDataLoader dataLoader, 
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap) { }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
            Attributes.Remove("BREAKER_NO");
        }
    }
}
