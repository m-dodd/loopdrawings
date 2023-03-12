using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelJBRowData<T>
    {
        T JBTag { get; set; }
        T TerminalStrip { get; set; }
        T Terminal { get; set; }
        T SignalType { get; set; }
        T DeviceTag { get; set; }

        IExcelJBRowSide<T> LeftSide { get; set; }
        IExcelJBRowSide<T> RightSide { get; set; }
    }

    public interface IExcelJBRowSide<T>
    {
        T Cable { get; set; }
        T Core { get; set; }
        T Color { get; set; }
        T WireTag { get; set; }
    }
}
