//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using LoopDataAdapterLayer;

//namespace LoopDrawingAcadUI
//{
//    public class DrawingsBuilder
//    {
//        private readonly string configFileName;
//        public List<AcadDrawingData> Drawings { get; set; }

//        public DrawingsBuilder(string configFileName)
//        {
//            this.configFileName = configFileName;
//            Drawings = new List<AcadDrawingData>();
//        }

//        public void LoadDrawingsFromFile()
//        {
//            var json = File.ReadAllText(configFileName);
//            Drawings = JsonConvert.DeserializeObject<List<AcadDrawingData>>(json) ?? new List<DrawingData>();
//        }
//    }



//}
