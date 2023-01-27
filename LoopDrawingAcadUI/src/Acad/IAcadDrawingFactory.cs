using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDrawingAcadUI
{
    public interface IAcadDrawingFactory
    {
        AcadDrawing CreateDrawing(AcadDrawingData drawingData);
    }
}
