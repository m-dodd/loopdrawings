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
        internal static string GetRowString<T>(IXLRow row, T col) where T : Enum
        {
            try
            {
                return row.Cell((int)(object)col).GetString();
            }
            catch
            {
                return string.Empty;
            }
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
            this.IOws = wb.Worksheet("IO_Wiring_Devices");
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

        public static bool IsExcelFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string[] validExtensions = { ".xlsx", ".xlsm" };
            foreach (string ext in validExtensions)
            {
                if (extension.ToLower() == ext) return true;
            }
            return false;
        }

        public void Dispose() => wb.Dispose();
    }
}
