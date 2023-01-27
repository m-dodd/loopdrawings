using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    public class AcadDrawingDataMappable : AcadBaseDrawingData, IMappableDrawing
    {
        public List<IMappableBlock> Blocks { get; set; } = new List<IMappableBlock>();

        public AcadDrawingDataMappable() { }

        public AcadDrawingDataMappable(List<IMappableBlock> blocks) 
        {
            this.Blocks = blocks;
        }

        private void MapBlocks()
        {
            foreach (IMappableBlock block in Blocks)
            {
                block.MapData();
            }
        }
        
        public void MapData()
        {
            MapBlocks();
        }
    }
}
