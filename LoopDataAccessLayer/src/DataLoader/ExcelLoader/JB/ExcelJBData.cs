using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                .OrderBy(r => ConvertToSortableInt(r.GetCellString(jbCols.Terminal)))
                .Select(GetJBData)
                .ToList();
        }

        private static int ConvertToSortableInt(string value)
        {
            string numericPart = Regex.Replace(value, "[^0-9]", "");
            return Convert.ToInt32(numericPart);
        }

        private IExcelJBRowData<string> GetJBData(IXLRow row) => new ExcelJBRowData<string>()
        {
            JBTag = row.GetCellString(jbCols.JBTag),
            ItemTag = row.GetCellString(jbCols.ItemTag),
            TerminalStrip = row.GetCellString(jbCols.TerminalStrip),
            Terminal = row.GetCellString(jbCols.Terminal),
            SignalType = row.GetCellString(jbCols.SignalType),
            DeviceTag = row.GetCellString(jbCols.DeviceTag),
            LeftSide = CreateExcelJBRowSide(row, jbCols.LeftSide),
            RightSide = CreateExcelJBRowSide(row, jbCols.RightSide),
        };

        private static ExcelJBRowSide<string> CreateExcelJBRowSide(IXLRow row, IExcelJBRowSide<int> sideCols)
        {
            return new ExcelJBRowSide<string>
            {
                Cable = row.GetCellString(sideCols.Cable),
                Core = row.GetCellString(sideCols.Core),
                Color = row.GetCellString(sideCols.Color),
                WireTag = row.GetCellString(sideCols.WireTag)
            };
        }
    }
}
