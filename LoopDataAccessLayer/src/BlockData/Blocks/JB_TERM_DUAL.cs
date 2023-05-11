﻿using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class JB_TERM_DUAL : BlockJBBase
    {
        public JB_TERM_DUAL(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader, blockMap, tagMap)
        {
        }
        protected override void FetchExcelData() => PopulateAttributesForDualJB(isAnalog: false);
    }
}
