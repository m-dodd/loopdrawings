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
        T PowerTerminalStrip { get; set; }
        T PowerVolts { get; set; }
        T PowerTerm1 { get; set; }
        T PowerTerm2 { get; set; }
        T PowerWireTag1 { get; set; }
        T PowerWireTag2 { get; set; }
        T PowerCore1 { get; set; }
        T PowerCore2 { get; set; }
        T PowerCable { get; set; }
        IExcelIODeviceCommon<T> Device { get; set; }
        IExcelIODeviceCommon<T> IO { get; set; }
        IExcelIORelay<T> Relay { get; set; }

    }

    public interface IExcelIODeviceCommon<T>
    {
        T CableTag { get; set; }
        T Terminal1 { get; set; }
        T Terminal2 { get; set; }
        T Terminal3 { get; set; }
        T Terminal4 { get; set; }
        T WireTag1 { get; set; }
        T WireTag2 { get; set; }
        T WireTag3 { get; set; }
        T WireTag4 { get; set; }
        T WireColor1 { get; set; }
        T WireColor2 { get; set; }
        T WireColor3 { get; set; }
        T CorePair1 { get; set; }
        T CorePair2 { get; set; }
        T CorePair3 { get; set; }
        T CorePair4 { get; set; }
    }

    public interface IExcelIORelay<T>
    {
        T Tag { get; set; }
        T PanelTerminalStrip { get; set; }
        T Term1 { get; set; }
        T Term2 { get; set; }
        T ContactTag { get; set; }
        T ContactTerm1 { get; set; }
        T ContactTerm2 { get; set; }
    }
    
}
