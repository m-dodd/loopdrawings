using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataAcessObjects
{
    public class JsonHelper
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
