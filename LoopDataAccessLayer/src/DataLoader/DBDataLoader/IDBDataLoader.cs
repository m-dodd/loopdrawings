using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    public interface IDBLoader
    {
        List<LoopNoTemplatePair> GetLoops();
        IEnumerable<LoopTagData> GetLoopTags(LoopNoTemplatePair loop);
        DBLoopData GetLoopData(string tag);
        List<SDKData> GetSDs(string tag);
    }
}
