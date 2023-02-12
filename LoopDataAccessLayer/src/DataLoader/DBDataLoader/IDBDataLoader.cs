using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IDBLoader
    {
        public List<LoopNoTemplatePair> GetLoops();
        public List<LoopTagData> GetLoopTags(LoopNoTemplatePair loop);
        public DBLoopData GetLoopData(string tag);

    }
}
