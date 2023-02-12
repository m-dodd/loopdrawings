using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelIODeviceCommon<T> : IExcelIODeviceCommon<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public T CableTag { get; set; }
        public T TerminalPlus { get; set; }
        public T TerminalNeg { get; set; }
        public T TerminalShld { get; set; }
        public T WireTagPlus { get; set; }
        public T WireTagNeg { get; set; }
        public T WireColorPlus { get; set; }
        public T WireColorNeg { get; set; }
        public T CorePairPlus { get; set; }
        public T CorePairNeg { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    
}
