using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingBuilder
    {
        public List<LoopDrawingData> Drawings { get; set; } = new List<LoopDrawingData>();
        private AcadDrawingDataFactory _drawingFactory;
        private LoopDataConfig _config;

        public AcadDrawingBuilder(string configFileName)
        {
            _config = new LoopDataConfig();
            _config.LoadConfig(configFileName);
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }

        public void ToJson(string fileName)
        {
            throw new NotImplementedException();
        }

        public List<LoopDrawingData> FromJson()
        {
            throw new NotImplementedException();
        }
    }


    
}
