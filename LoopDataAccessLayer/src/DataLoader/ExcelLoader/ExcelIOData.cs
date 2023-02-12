using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelIOData : IExcelIOData<string>
    {
        public string ModuleTerm01 {get; set;} = string.Empty;
        public string ModuleTerm02 {get; set;} = string.Empty;
        public string ModuleWireTag01 {get; set;} = string.Empty;
        public string ModuleWireTag02 {get; set;} = string.Empty;
        public string PanelTag {get; set;} = string.Empty;
        public string BreakerNumber {get; set;} = string.Empty;
        public string Tag {get; set;} = string.Empty;
        public string PanelTerminalStrip { get; set; } = string.Empty;
        public string JB1 {get; set;} = string.Empty;
        public string JB2 {get; set;} = string.Empty;
        public string JB3 {get; set;} = string.Empty;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IExcelIODeviceCommon<string> Device { get; set; } 
        public IExcelIODeviceCommon<string> IO { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.        
    }
}
