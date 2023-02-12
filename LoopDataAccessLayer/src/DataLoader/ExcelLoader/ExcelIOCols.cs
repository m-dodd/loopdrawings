using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelIOCols : IExcelIOData<int>
    {
        public int ModuleTerm01 { get; set; } = -1;
        public int ModuleTerm02 { get; set; } = -1;
        public int ModuleWireTag01 { get; set; } = -1;
        public int ModuleWireTag02 { get; set; } = -1;
        public int PanelTag { get; set; } = -1;
        public int BreakerNumber { get; set; } = -1;
        public int Tag { get; set; } = -1;
        public int PanelTerminalStrip { get; set; } = -1;
        public int JB1 { get; set; } = -1;
        public int JB2 { get; set; } = -1;
        public int JB3 { get; set; } = -1;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IExcelIODeviceCommon<int> Device { get; set; }
        public IExcelIODeviceCommon<int> IO { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
