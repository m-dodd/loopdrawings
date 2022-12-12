using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoopDataAdapterLayer
{
    public static class JsonLoopHelper
    {
        public static void WriteToFile(LoopData loop, string filePath)
        {
            string json = JsonConvert.SerializeObject(loop, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        public static LoopData ReadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<LoopData>(json);
        }

        public static void WriteLoopsToFile(List<LoopData> loops, string filePath)
        {
            string json = JsonConvert.SerializeObject(loops, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        public static List<LoopData> ReadLoopsFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<LoopData>>(json);
        }
    }


    public class JsonHelperDictStrings
    {
        public static Dictionary<string, object> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        public static Dictionary<string, object>[] FromJsonArray(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>[]>(json);
        }

        public static void ToJsonFile(string path, Dictionary<string, string> value)
        {
            string json = JsonConvert.SerializeObject(value, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static void ToJsonFile(string path, Dictionary<string, string>[] array)
        {
            string json = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static void ToJsonFile(string path, List<Dictionary<string, string>> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(path, json); 
        }
    }
}
