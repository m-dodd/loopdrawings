using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IAcadDrawingBuilder
    {
        public (AcadDrawingDataMappable?, AcadDrawingDataMappable?) BuildDrawings(LoopNoTemplatePair loop);
    }
}
