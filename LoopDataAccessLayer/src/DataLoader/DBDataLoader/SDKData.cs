using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class SDKData : ISDKData
    {
        public string ParentTag { get; set; } = string.Empty;
        public string InputTag { get; set; } = string.Empty;
        public string SdGroup { get; set; } = string.Empty;
        public string OutputTag { get; set; } = string.Empty;
        public string OutputDescription { get; set; } = string.Empty;
        public string SdAction1 { get; set; } = string.Empty;
        public string SdAction2 { get; set; } = string.Empty;
    }
}
