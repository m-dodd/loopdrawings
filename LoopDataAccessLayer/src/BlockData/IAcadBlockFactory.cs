﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IAcadBlockFactory
    {
        BlockDataMappable GetBlock(BlockMapData blockMap, Dictionary<string, string> tagMap);
    }
}
