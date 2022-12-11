using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataAcessObjects
{
    public interface IFileLoaderSaver
    {
        void Save(string path, Dictionary<string, object> data);
        void Save(string path, List<Dictionary<string, object>> data);
        void Save(string path, Dictionary<string, object>[] data);
        object Load(string path);
    }

    //public class JsonLoaderSaver : IFileLoaderSaver
    //{
    //    public void Save(string path, Dictionary<string, object> data)
    //    {
    //        JsonHelper.ToJsonFile(path, data);
    //    }

    //    public void Save(string path, List<Dictionary<string, object>> data)
    //    {
    //        JsonHelper.ToJsonFile(path, data);
    //    }
    //    public void Save(string path, Dictionary<string, object>[] data)
    //    {

    //    }
    //    public object Load(string fileName)
    //    {
    //        var json = File.ReadAllText(fileName);
    //        return JsonHelper.FromJsonArray(json);
    //    }
    //}
}
