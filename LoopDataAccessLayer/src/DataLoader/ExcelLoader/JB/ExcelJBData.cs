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
                .OrderBy(r => ConvertToSortableInt(ExcelHelper.GetRowString(r, jbCols.Terminal)))
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
            JBTag = ExcelHelper.GetRowString(row, jbCols.JBTag),
            ItemTag = ExcelHelper.GetRowString(row, jbCols.ItemTag),
            TerminalStrip = ExcelHelper.GetRowString(row, jbCols.TerminalStrip),
            Terminal = ExcelHelper.GetRowString(row, jbCols.Terminal),
            SignalType = ExcelHelper.GetRowString(row, jbCols.SignalType),
            DeviceTag = ExcelHelper.GetRowString(row, jbCols.DeviceTag),
            LeftSide = CreateExcelJBRowSide(row, jbCols.LeftSide),
            RightSide = CreateExcelJBRowSide(row, jbCols.RightSide),
        };

        private static ExcelJBRowSide<string> CreateExcelJBRowSide(IXLRow row, IExcelJBRowSide<int> sideCols)
        {
            return new ExcelJBRowSide<string>
            {
                Cable = ExcelHelper.GetRowString(row, sideCols.Cable),
                Core = ExcelHelper.GetRowString(row, sideCols.Core),
                Color = ExcelHelper.GetRowString(row, sideCols.Color),
                WireTag = ExcelHelper.GetRowString(row, sideCols.WireTag)
            };
        }
    }
}
