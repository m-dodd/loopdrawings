using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelJBRowData<T> : IExcelJBRowData<T>
    {
#pragma warning disable CS8618
        public T JBTag {get; set;}
        public T TerminalStrip {get; set;}
        public T Terminal {get; set;}
        public T SignalType {get; set;}
        public T DeviceTag {get; set;}
        public IExcelJBRowSide<T> LeftSide { get; set; }
        public IExcelJBRowSide<T> RightSide { get; set; }
#pragma warning restore CS8618
    }

    
}
