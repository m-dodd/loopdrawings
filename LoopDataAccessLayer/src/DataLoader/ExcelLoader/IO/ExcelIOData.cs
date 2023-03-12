using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelIOData<T> : IExcelIOData<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public T ModuleTerm01 { get; set; }
        public T ModuleTerm02 { get; set; }
        public T ModuleWireTag01 { get; set; }
        public T ModuleWireTag02 { get; set; }
        public T PanelTag { get; set; }
        public T BreakerNumber { get; set; }
        public T Tag { get; set; }
        public T PanelTerminalStrip { get; set; }
        public T JB1 { get; set; }
        public T JB2 { get; set; }
        public T JB3 { get; set; }
        public IExcelIODeviceCommon<T> Device { get; set; }
        public IExcelIODeviceCommon<T> IO { get; set; }
        public IExcelIORelay<T> Relay { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.        
    }
}
