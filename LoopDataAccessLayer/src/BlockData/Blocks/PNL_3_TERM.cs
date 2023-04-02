﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class PNL_3_TERM : PNL_3_or_4_TERM_24VDC
    {
        public PNL_3_TERM(
            IDataLoader dataLoader, 
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader, blockMap, tagMap) { }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
            Attributes.Remove("BREAKER_NO");
        }
    }
}
