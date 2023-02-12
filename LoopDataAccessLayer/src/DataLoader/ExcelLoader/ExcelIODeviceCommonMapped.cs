using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelIODeviceCommonMapped : ExcelIODeviceCommon<string>
    {
        public ExcelIODeviceCommonMapped(IXLRow row, IExcelIODeviceCommon<int> cols)
        {
            CableTag = ExcelHelper.GetRowString(row, cols.CableTag);
            TerminalPlus = ExcelHelper.GetRowString(row, cols.TerminalPlus);
            TerminalNeg = ExcelHelper.GetRowString(row, cols.TerminalNeg);
            TerminalShld = ExcelHelper.GetRowString(row, cols.TerminalShld);
            WireTagPlus = ExcelHelper.GetRowString(row, cols.WireTagPlus);
            WireTagNeg = ExcelHelper.GetRowString(row, cols.WireTagNeg);
            WireColorPlus = ExcelHelper.GetRowString(row, cols.WireColorPlus);
            WireColorNeg = ExcelHelper.GetRowString(row, cols.WireColorNeg);
            CorePairPlus = ExcelHelper.GetRowString(row, cols.CorePairPlus);
            CorePairNeg = ExcelHelper.GetRowString(row, cols.CorePairNeg);
        }
    }
}
