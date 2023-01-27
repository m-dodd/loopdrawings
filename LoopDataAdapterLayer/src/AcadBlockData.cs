using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAdapterLayer
{
    public class AcadBlockData
    {
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
    }
}
