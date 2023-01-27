using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;

namespace LoopDrawingAcadUI
{
    public interface IAcadBlockProcessor
    {
        void ProcessBlock(AcadBlockData block);
        void ProcessBlocks(IEnumerable<AcadBlockData> blocks);
    }
}
