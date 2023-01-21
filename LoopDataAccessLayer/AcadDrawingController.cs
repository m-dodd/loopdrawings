using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingController
    {
        
        private LoopDataConfig loopConfig;
        private DataLoader dataLoader;

        public List<LoopDrawingData> Drawings { get; set; }

        public AcadDrawingController(DataLoader dataLoader, string configFileName)
        {
            this.dataLoader = dataLoader;

            // probably need a graceful excel if no config file found or not the right format
            // handle these events
            loopConfig = new(configFileName);
            loopConfig.LoadConfig();
            
            Drawings = new List<LoopDrawingData>();
            
        }

        private IEnumerable<LoopNoTemplatePair> GetLoops()
        {
            //var loops = dataLoader
            //    .DBLoader
            //    .
            // read from database and get the loop / loop template pair objects
            throw new NotImplementedException();
        }

        public void BuildDrawings()
        {
            AcadDrawingBuilder drawingBuilder = new(dataLoader, loopConfig);
            foreach (LoopNoTemplatePair loop in dataLoader.DBLoader.GetLoops()) 
            {
                LoopDrawingData drawing = drawingBuilder.BuildDrawing(loop);
                drawing.MapData();
                Drawings.Add(drawing);
            }
        }
    }
}
