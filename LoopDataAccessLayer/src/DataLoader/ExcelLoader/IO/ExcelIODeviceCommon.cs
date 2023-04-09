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
        public T Terminal1 { get; set; }
        public T Terminal2 { get; set; }
        public T Terminal3 { get; set; }
        public T Terminal4 { get; set; }
        public T WireTag1 { get; set; }
        public T WireTag2 { get; set; }
        public T WireTag3 { get; set; }
        public T WireTag4 { get; set; }
        public T WireColor1 { get; set; }
        public T WireColor2 { get; set; }
        public T WireColor3 { get; set; }
        public T CorePair1 { get; set; }
        public T CorePair2 { get; set; }
        public T CorePair3 { get; set; }
        public T CorePair4 { get; set; }
        public T PanelTag { get; set; }
        public T PanelTerminalStrip { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }


}
