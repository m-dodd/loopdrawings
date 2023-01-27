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
        
        private readonly LoopDataConfig loopConfig;
        private readonly DataLoader dataLoader;
        private TitleBlockData titleBlock;

        public List<AcadDrawingDataMappable> Drawings { get; set; }

        public AcadDrawingController(DataLoader dataLoader, LoopDataConfig loopConfig, TitleBlockData titleBlock)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            this.titleBlock = titleBlock;
            Drawings = new List<AcadDrawingDataMappable>();
        }

        public void BuildDrawings()
        {
            AcadDrawingBuilder drawingBuilder = new(dataLoader, loopConfig, titleBlock);
            foreach (LoopNoTemplatePair loop in dataLoader.GetLoops())
            {
                AcadDrawingDataMappable? drawing = drawingBuilder.BuildDrawing(loop);
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
