using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    

    public class ExcelDataLoader : IExcelLoader, IDisposable
    {
        private readonly IXLWorksheet IOws, JBws;
        private readonly XLWorkbook wb;
        private readonly IDictionary<string, IXLRow?> ioData;
        private readonly IDictionary<string, IXLRows?> jbData;

        public ExcelDataLoader(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be null or empty", nameof(fileName));
            if (!ExcelHelper.IsExcelFile(fileName))
                throw new ArgumentException("File is not an excel file", nameof(fileName));
            this.wb = new XLWorkbook(fileName);
            this.IOws = wb.Worksheet("IO_Wiring_Devices");
            this.JBws = wb.Worksheet("JB Wiring");
            ioData = new Dictionary<string, IXLRow?>();
            jbData = new Dictionary<string, IXLRows?>();
        }

        public IXLRow? GetIORow(string tag)
        {
            if (ioData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = IOws?.Column((int)ExcelIOColumns.TAG_01)
                           ?.CellsUsed(cell => cell.GetString() == tag)
                           ?.FirstOrDefault()
                           ?.WorksheetRow();
                ioData.Add(tag, data);
                return data;
            }
        }

        public IXLRows? GetJBRows(string tag)
        {
            if (jbData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = JBws?.RowsUsed(r => r.Cell((int)ExcelJBColumns.TAG_01)?.GetString() == tag);
                jbData.Add(tag, data);
                return data;
            }
        }

        public void Dispose()
        {
            wb.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
