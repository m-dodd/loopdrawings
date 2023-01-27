using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LoopDataAdapterLayer;

namespace LoopDrawingAcadUI
{
    internal class AcadController
    {
        private List<AcadDrawingData> drawingsData;
        public void LoadDrawingsFromFile(string filename)
        {
            // SHOULD ADD ERROR HANDLING HERE
            var json = File.ReadAllText(filename);
            drawingsData = JsonConvert.DeserializeObject<List<AcadDrawingData>>(json);
        }

        public void BuildDrawings()
        {
            AcadDrawingFactory drawingFactory = new AcadDrawingFactory();
            AcadDrawingProcessor drawingProcessor = new AcadDrawingProcessor();
            foreach (AcadDrawingData drawingData in drawingsData)
            {
                using (AcadDrawing drawing = drawingFactory.CreateDrawing(drawingData))
                {
                    drawingProcessor.ProcessDrawing(drawing);
                }
            }
        }

    }
}
