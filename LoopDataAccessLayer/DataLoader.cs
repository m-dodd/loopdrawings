using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class DataLoader
    {
        DBDataLoader dbLoader;
        ExcelDataLoader excelLoader;
        List<Dictionary<string, string>> data;
        public DataLoader(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        { 
            this.dbLoader = dbLoader;
            this.excelLoader = excelLoader;
            this.data = new List<Dictionary<string, string>>();
        }

        public Dictionary<string, string> GetLoopData(string loop)
        {
            // loop is not actual tested or designed yet. I've really just been working with a tag, which is a loop
            // with a single value, but what happens when a loop is more complex?

            // additionally this is really just a way to merge the attribute data
            // need a way to store the drawing template as well as the loop name and the titleblock information
            var excelLoopData = excelLoader.GetLoopData(loop);
            var dbLoopData = dbLoader.GetLoopData(loop).ToDict();
            return 
                excelLoopData
                .Concat(dbLoopData.Where(x => !excelLoopData.Keys.Contains(x.Key)))
                .ToDictionary(e => e.Key, e => e.Value);
        }

        public void GetLoopsData(string[] loops)
        {
            foreach(string loop in loops)
            {
                data.Add(GetLoopData(loop));
            }
        }
        public string DataToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var loop in data)
            {
                sb.Append(DictToString(loop));
                sb.Append(System.Environment.NewLine + System.Environment.NewLine);
            }
            return sb.ToString();
        }

        private string DictToString(Dictionary<string, string> dict)
        {
            return string.Join(System.Environment.NewLine, dict.Select(x => x.Key + ": " + x.Value?.ToString()));
        }


    }
}
