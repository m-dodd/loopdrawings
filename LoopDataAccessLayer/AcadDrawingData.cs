using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    public class AcadDrawingData : IMappable
    {
        public string LoopID { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty; 
        public string DrawingFileName { get; set; } = string.Empty;
        public List<BlockDataMappable> Blocks { get; set; } = new List<BlockDataMappable>();

        public AcadDrawingData() { }

        private void MapBlocks()
        {
            foreach (BlockDataMappable block in Blocks)
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
