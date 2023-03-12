using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelDataLoader
    {
        IXLRow? GetIORow(string tag);
        IXLRows? GetJBRows(string tag);

        IExcelIOData<string>? GetIOData(string tag);
        List<ExcelJBData>? GetJBData(string tag);
        IExcelCableData<string>? GetCableData(string cable);
        IExcelTitleBlockData<string> GetTitleBlockData();
        IExcelJBRowData<int> ExcelJBCols { get; }
        IExcelIOData<int> ExcelIOCols { get; }
    }
}
