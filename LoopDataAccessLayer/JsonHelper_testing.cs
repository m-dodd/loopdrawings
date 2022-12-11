using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoopDataAccessLayer
{
    public class JsonHelperFirstAttempt
    {
        public static string WriteToJson(Dictionary<string, object> dictionary)
        {
            return JsonConvert.SerializeObject(dictionary);
        }

        public static string WriteToJson(List<Dictionary<string, object>> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public static string WriteToJson(Dictionary<string, Dictionary<string, object>> dictOfDict)
        {
            return JsonConvert.SerializeObject(dictOfDict);
        }

        //public static Dictionary<string, object> ReadFromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        //}

        public static List<Dictionary<string, object>> ReadListFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
        }

        public static Dictionary<string, Dictionary<string, object>> ReadDictOfDictFromJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);
        }

        // Overloaded method for reading a dictionary from JSON
        public static Dictionary<string, object>? ReadFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        // Overloaded method for reading a list of dictionaries from JSON
        //public static List<Dictionary<string, object>>? ReadFromJson(string json)
        //{
        //    if (string.IsNullOrEmpty(json))
        //    {
        //        return null;
        //    }

        //    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
        //}
    }


    public class JsonHelperMaybe
    {
        //public static Dictionary<string, object> FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
        //}

        //public static Dictionary<string, object>[] FromJsonArray(string json)
        //{
        //    return JsonConvert.DeserializeObject<Dictionary<string, object>[]>(json) ?? new Dictionary<string, object>[]();
        //}

        public static string ToJson(Dictionary<string, object> value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string ToJson(Dictionary<string, object>[] array)
        {
            return JsonConvert.SerializeObject(array);
        }

        public static string ToJson(List<Dictionary<string, object>> list)
        {
            return JsonConvert.SerializeObject(list);
        }
    }

public class JsonHelperY
    {
        //public static T FromJson<T>(string json)
        //{
        //    JToken token = JToken.Parse(json);

        //    if (token.Type == JTokenType.Array)
        //    {
        //        return token.ToObject<List<T>>();
        //    }
        //    else
        //    {
        //        return token.ToObject<T>();
        //    }
        //}


        private static Dictionary<string, object> FromJsonObject(JToken token)
        {
            return token.ToObject<Dictionary<string, object>>();
        }

        private static List<Dictionary<string, object>> FromJsonArray(JToken token)
        {
            return token.ToObject<List<Dictionary<string, object>>>();
        }

        public static string ToJson(Dictionary<string, object> value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string ToJson(List<Dictionary<string, object>> list)
        {
            return JsonConvert.SerializeObject(list);
        }
    }



    public class JsonHelperGeneral
    {
        public static void WriteToJsonFile(string filePath, object data)
        {
            // Serialize the data to a JSON string
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Write the JSON string to the specified file
            File.WriteAllText(filePath, json);
        }

        //public static object ReadFromJsonFile(string filePath)
        //{
        //    // Read the contents of the file into a string
        //    string json = File.ReadAllText(filePath);

        //    // Try to deserialize the JSON string as both a single dictionary and a list of dictionaries
        //    var singleDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        //    var listOfDictionaries = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);

        //    // Return the result that is not null
        //    return singleDictionary ?? listOfDictionaries;
        //}
    }
}