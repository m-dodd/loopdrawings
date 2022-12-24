using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAdapterLayer
{
    public class LoopData
    {
        public string LoopID { get; set; } = String.Empty;
        public string DrawingType { get; set; } = String.Empty;
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return
                "LoopID: " + LoopID
                + System.Environment.NewLine
                + "DrawingType: " + DrawingType
                + System.Environment.NewLine
                + DictToString(Attributes);
            
        }

        private string DictToString(Dictionary<string, string> dict)
        {
            return string.Join(System.Environment.NewLine, dict.Select(x => x.Key + ": " + x.Value.ToString()));
        }

    }

    public class LoopDataCollection
    {
        public List<LoopData> Data { get; set; } = new List<LoopData>();
        public void Add(LoopData data) { Data.Add(data); }
        
        public void Save(string filePath)
        {
            JsonLoopHelper.WriteLoopsToFile(Data, filePath);
        }
        
        public void Load(string filePath)
        {
            Data = JsonLoopHelper.ReadLoopsFromFile(filePath);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var loop in Data)
            {
                sb.Append(loop.ToString());
                sb.Append(System.Environment.NewLine + System.Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
