using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelJBData
    {
        private readonly IExcelJBRowData<int> jbCols;
        public List<IExcelJBRowData<string>> TerminalData { get; private set; }


        public ExcelJBData(IEnumerable<IXLRow> jbRows, IExcelJBRowData<int> jbCols)
        {
            this.jbCols = jbCols;
            TerminalData = jbRows
                .OrderBy(r => ExcelHelper.GetRowString(r, jbCols.Terminal))
                .Select(GetJBData)
                .ToList();
        }

        private IExcelJBRowData<string> GetJBData(IXLRow row)
        {
            return new ExcelJBRowData()
            {
                JBTag = ExcelHelper.GetRowString(row, jbCols.JBTag),
                TerminalStrip = ExcelHelper.GetRowString(row, jbCols.TerminalStrip),
                Terminal = ExcelHelper.GetRowString(row, jbCols.Terminal),
                SignalType = ExcelHelper.GetRowString(row, jbCols.SignalType),
                DeviceTag = ExcelHelper.GetRowString(row, jbCols.DeviceTag),
                LeftSide = new ExcelJBRowSideMapped(row, jbCols.LeftSide),
                RightSide = new ExcelJBRowSideMapped(row, jbCols.RightSide)
            };
        }
    }
}
