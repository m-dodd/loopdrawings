using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelJBRowSideMapped : ExcelJBRowSide<string>
    {
        public ExcelJBRowSideMapped(IXLRow row, IExcelJBRowSide<int> cols)
        {
            Cable = ExcelHelper.GetRowString(row, cols.Cable);
            Core = ExcelHelper.GetRowString(row, cols.Core);
            Color = ExcelHelper.GetRowString(row, cols.Color);
            WireTag = ExcelHelper.GetRowString(row, cols.WireTag);
        }
    }
}
