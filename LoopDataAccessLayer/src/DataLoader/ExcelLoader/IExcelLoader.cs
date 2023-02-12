using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelLoader
    {
        IExcelJBRowData<int> ExcelJBCols { get; }
        IExcelIOData<int> ExcelIOCols { get; }

        public IXLRow? GetIORow(string tag);
        public IXLRows? GetJBRows(string tag);

        public IExcelIOData<string>? GetIOData(string tag);
        public List<ExcelJBData>? GetJBData(string tag);
    }
}
