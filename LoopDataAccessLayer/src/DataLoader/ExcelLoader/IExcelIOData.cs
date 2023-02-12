using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelIOData<T>
    {
        T ModuleTerm01 { get; set; }
        T ModuleTerm02 { get; set; }
        T ModuleWireTag01 { get; set; }
        T ModuleWireTag02 { get; set; }
        T PanelTag { get; set; }
        T BreakerNumber { get; set; }
        T Tag { get; set; }
        T PanelTerminalStrip { get; set; }
        
        T JB1 { get; set; }
        T JB2 { get; set; }
        T JB3 { get; set; }
        IExcelIODeviceCommon<T> Device { get; set; }
        IExcelIODeviceCommon<T> IO { get; set; }
        
    }

    public interface IExcelIODeviceCommon<T>
    {
        T CableTag { get; set; }
        T TerminalPlus { get; set; }
        T TerminalNeg { get; set; }
        T TerminalShld { get; set; }
        T WireTagPlus { get; set; }
        T WireTagNeg { get; set; }
        T WireColorPlus { get; set; }
        T WireColorNeg { get; set; }
        T CorePairPlus { get; set; }
        T CorePairNeg { get; set; }
    }
    
}
