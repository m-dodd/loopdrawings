using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class DataLoader
    {
        private readonly DBDataLoader dbLoader;
        private readonly ExcelDataLoader excelLoader;
        private readonly LoopDataCollection data;
        
        public DataLoader(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        
        { 
            this.dbLoader = dbLoader;
            this.excelLoader = excelLoader;
            this.data = new LoopDataCollection();
        }

        public LoopDataCollection Data { get { return data; } }

        private LoopData GetLoopData(string loop)
        {
            // loop is not actual tested or designed yet. I've really just been working with a tag, which is a loop
            // with a single value, but what happens when a loop is more complex?

            // additionally this is really just a way to merge the attribute data
            // need a way to store the drawing template as well as the loop name and the titleblock information
            var excelLoopData = excelLoader.GetLoopData(loop);
            var dbLoopData = dbLoader.GetLoopData(loop).ToDict();
            
            // combine all of the data from Excel and Database
            // then remove the LoopID and DrawingID and build a LoopData object
            var attributes = excelLoopData
                .Concat(dbLoopData.Where(x => !excelLoopData.Keys.Contains(x.Key)))
                .ToDictionary(e => e.Key, e => e.Value);
            _ = attributes.Remove("", out string? loopID);
            _ = attributes.Remove("", out string? drawingID);
            LoopData loopData = new()
            {
                LoopID = loopID ?? String.Empty,
                DrawingType = drawingID ?? String.Empty,
                Attributes = attributes
            };
            return loopData;
        }

        public string GetLoopDataString(string loop) => GetLoopData(loop).ToString() ?? String.Empty;

        public void FetchLoopsData(string[] loops)
        {
            foreach(string loop in loops)
            {
                data.Add( GetLoopData(loop) );
            }
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }
}
