using LoopDataAdapterLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingController
    {
        
        private LoopDataConfig loopConfig;
        private DataLoader dataLoader;

        public List<AcadDrawingData> Drawings { get; set; }

        public AcadDrawingController(DataLoader dataLoader, string configFileName)
        {
            this.dataLoader = dataLoader;

            // probably need a graceful exit if no config file found or not the right format
            // handle these events
            loopConfig = new(configFileName);
            loopConfig.LoadConfig();
            
            Drawings = new List<AcadDrawingData>();
            
        }

        public void BuildDrawings()
        {
            AcadDrawingBuilder drawingBuilder = new(dataLoader, loopConfig);
            foreach (LoopNoTemplatePair loop in dataLoader.DBLoader.GetLoops())
            {
                AcadDrawingData? drawing = drawingBuilder.BuildDrawing(loop);
                if (drawing != null)
                {
                    Drawings.Add(drawing);
                }
            }
        }

        public void SaveDrawingsToFile(string fileName)
        {
            var json = JsonConvert.SerializeObject(Drawings, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }
    }
}
