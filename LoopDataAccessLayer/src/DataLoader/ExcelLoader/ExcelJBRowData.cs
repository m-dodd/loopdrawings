using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelJBRowData : IExcelJBRowData<string>
    {
        public string JBTag {get; set;} = string.Empty;
        public string TerminalStrip {get; set;} = string.Empty;
        public string Terminal {get; set;} = string.Empty;
        public string SignalType {get; set;} = string.Empty;
        public string DeviceTag {get; set;} = string.Empty;
#pragma warning disable CS8618
        public IExcelJBRowSide<string> LeftSide { get; set; }
        public IExcelJBRowSide<string> RightSide { get; set; }
#pragma warning restore CS8618
    }

    
}
