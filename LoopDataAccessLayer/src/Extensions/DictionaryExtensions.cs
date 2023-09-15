using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> destination, IEnumerable<KeyValuePair<TKey, TValue>> source, Action<string>? logger)
        {
            foreach (var kvp in source)
            {
                if (destination.ContainsKey(kvp.Key))
                {
                    // Log warning about duplicate key
                    logger?.Invoke($"Duplicate key '{kvp.Key}' found in dictionaries. Using the value from the source dictionary.");
                }
                else
                {
                    destination.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> destination, IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            AddRange(destination, source, null);
        }
    }

    public static class StringExtensions
    {
        public static string AppendDrawingNumber(this string drawingName, string suffix)
        {
            return drawingName.EndsWith("-01") ? drawingName[0..^3] + suffix : drawingName + suffix;
        }
    }
}
