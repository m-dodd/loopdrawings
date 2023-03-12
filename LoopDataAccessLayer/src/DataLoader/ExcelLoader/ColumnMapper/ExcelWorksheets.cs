using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer.src.ExcelHelpers
{
    public class ExcelWorksheets
    {
        public IXLWorksheet IOws { get; }
        public IXLWorksheet JBws { get;}
        public IXLWorksheet TitleBlockWS { get;}
        public IXLWorksheet CableWS { get; }

        public ExcelWorksheets(XLWorkbook wb)
        {
            this.IOws = wb.Worksheet("IO_Wiring_Devices");
            this.JBws = wb.Worksheet("JB Wiring");
            this.TitleBlockWS = wb.Worksheet("Titleblock");
            this.CableWS = wb.Worksheet("Cables");
        }
        
    }
}
