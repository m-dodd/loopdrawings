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
        private readonly IDataLoader dataLoader;

        public List<AcadDrawingDataMappable> Drawings { get; set; }

        public AcadDrawingController(IDataLoader dataLoader, LoopDataConfig loopConfig)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            Drawings = new List<AcadDrawingDataMappable>();
        }

        public void BuildDrawings()
        {
            AcadBlockFactory blockFactory = new(dataLoader);
            TemplatePicker templatePicker = new(dataLoader, loopConfig);
            AcadDrawingBuilder drawingBuilder = new(dataLoader, loopConfig, templatePicker, blockFactory);
            IEnumerable<LoopNoTemplatePair> loops = dataLoader.GetLoops();
            foreach (LoopNoTemplatePair loop in loops)
            {
                (AcadDrawingDataMappable? drawing1, AcadDrawingDataMappable? drawing2)  = drawingBuilder.BuildDrawings(loop);
                if (drawing1 != null)
                {
                    Drawings.Add(drawing1);
                }
                if (drawing2 != null)
                {
                    Drawings.Add(drawing2);
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
