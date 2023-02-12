using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelJBCols : IExcelJBRowData<int>
    {
        public int JBTag { get; set; } = -1;
        public int TerminalStrip { get; set; } = -1;
        public int Terminal { get; set; } = -1;
        public int SignalType { get; set; } = -1;
        public int DeviceTag { get; set; } = -1;
#pragma warning disable CS8618
        public IExcelJBRowSide<int> LeftSide { get; set; }
        public IExcelJBRowSide<int> RightSide { get; set; }
#pragma warning restore CS8618
        //public int LeftCable { get; set; } = -1;
        //public int LeftCore { get; set; } = -1;
        //public int LeftColor { get; set; } = -1;
        //public int LeftWireTag { get; set; } = -1;
        //public int RightCable { get; set; } = -1;
        //public int RightCore { get; set; } = -1;
        //public int RightColor { get; set; } = -1;
        //public int RightTag { get; set; } = -1;
    }
}
