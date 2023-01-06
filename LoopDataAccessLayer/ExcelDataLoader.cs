using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    internal static class ExcelStringHelper
    {
        internal static string GetJBRowString(IXLRow row, ExcelJBColumns col)
        {
            return row.Cell((int)col).GetString();
        }

        internal static string GetIORowString(IXLRow row, ExcelIOColumns col)
        {
            return row.Cell((int)col).GetString();
        }
    }

    public class ExcelDataLoader : IDisposable
    {
        private readonly string fileName;

        private readonly XLWorkbook wb;
        private readonly IXLWorksheet IOws, JBws;

        public ExcelDataLoader(string fileName)
        {
            this.fileName=fileName;
            this.wb = new XLWorkbook(this.fileName);
            this.IOws = wb.Worksheet("combine_io_devices");
            this.JBws = wb.Worksheet("JB Wiring");
        }

        public IXLRow? GetIORow(string tag)
        {
            return IOws.Column((int)ExcelIOColumns.TAG_01).CellsUsed(cell => cell.GetString() == tag).FirstOrDefault()?.WorksheetRow();
        }

        public IXLRows? GetJBRows(string tag)
        {
            var rows = JBws.RowsUsed(r => r.Cell((int)ExcelJBColumns.TAG_01).GetString() == tag);
            return rows;
        }

        public void Dispose() => wb.Dispose();
    }
}
