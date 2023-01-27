using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAdapterLayer
{
    public class AcadDrawingData : AcadBaseDrawingData
    {
        public List<AcadBlockData> Blocks { get; set; } = new List<AcadBlockData>();
    }
}
