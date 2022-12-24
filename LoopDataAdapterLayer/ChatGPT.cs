using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;

namespace LoopDataAdapterLayer
{
    public class AutoCadData
    {
        public string TemplateFile { get; set; }
        public string OutputPath { get; set; }
        public string BlockName { get; set; }
        public Dictionary<string, string> AttributeValues { get; set; }

        public static List<AutoCadData> ReadJson(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<List<AutoCadData>>(json);
                return data;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error reading JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void WriteJson(string filePath, List<AutoCadData> data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error writing JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}