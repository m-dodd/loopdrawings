﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class VALVE_BODY : BlockValveBase
    {
        public VALVE_BODY(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);
            PopulateValveDate(data);
        }
    }
}
