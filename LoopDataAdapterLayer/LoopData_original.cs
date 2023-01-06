//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace LoopDataAdapterLayer
//{
//    public static class JsonLoopHelper
//    {
//        public static void WriteToFile(LoopData_Old loop, string filePath)
//        {
//            string json = JsonConvert.SerializeObject(loop, Formatting.Indented);

//            File.WriteAllText(filePath, json);
//        }

//        public static LoopData_Old ReadFromFile(string filePath)
//        {
//            string json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<LoopData_Old>(json);
//        }

//        public static void WriteLoopsToFile(List<LoopData_Old> loops, string filePath)
//        {
//            string json = JsonConvert.SerializeObject(loops, Formatting.Indented);

//            File.WriteAllText(filePath, json);
//        }

//        public static List<LoopData_Old> ReadLoopsFromFile(string filePath)
//        {
//            string json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<LoopData_Old>>(json);
//        }
//    }
//    public class LoopData_Old
//    {
//        public string LoopID { get; set; } = string.Empty;
//        public string DrawingType { get; set; } = string.Empty;
//        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

//        public override string ToString()
//        {
//            return
//                "LoopID: " + LoopID
//                + System.Environment.NewLine
//                + "DrawingType: " + DrawingType
//                + System.Environment.NewLine
//                + DictToString(Attributes);
            
//        }

//        private string DictToString(Dictionary<string, string> dict)
//        {
//            return string.Join(System.Environment.NewLine, dict.Select(x => x.Key + ": " + x.Value.ToString()));
//        }

//    }

//    public class LoopDataCollection
//    {
//        public List<LoopData_Old> Data { get; set; } = new List<LoopData_Old>();
//        public void Add(LoopData_Old data) { Data.Add(data); }
        
//        public void Save(string filePath)
//        {
//            JsonLoopHelper.WriteLoopsToFile(Data, filePath);
//        }
        
//        public void Load(string filePath)
//        {
//            Data = JsonLoopHelper.ReadLoopsFromFile(filePath);
//        }

//        public override string ToString()
//        {
//            StringBuilder sb = new StringBuilder();
//            foreach (var loop in Data)
//            {
//                sb.Append(loop.ToString());
//                sb.Append(System.Environment.NewLine + System.Environment.NewLine);
//            }
//            return sb.ToString();
//        }
//    }
//}
