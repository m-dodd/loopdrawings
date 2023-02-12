using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LoopDataAccessLayer
{
    public class AcadDrawingDataMappable : AcadBaseDrawingData<IMappableBlock>, IMappableDrawing
    {
        public void MapData()
        {
            MapBlocks();
        }

        private void MapBlocks()
        {
            foreach (IMappableBlock block in Blocks)
            {
                block.MapData();
            }
        }
    }
}
