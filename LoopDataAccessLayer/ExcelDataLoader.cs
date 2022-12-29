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
        internal static string GetJBRowString(IXLRow row, string col)
        {
            return row.Cell(ExcelColumnMaps.JB[col]).GetString();
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

        public Dictionary<string, string> GetLoopData(string loop)
        {
            var ioData = GetIOTagData(loop);
            var jbData = GetJBTagData(loop);
            return ioData.Concat(jbData).ToDictionary(e => e.Key, e => e.Value);
        }

        public Dictionary<string, string> GetIOTagData(string tag)
        {
            if (String.IsNullOrEmpty(tag))
            {
                return new Dictionary<string, string>();
            }
            else
            {
                var row = GetIORow(tag);
                if (row is not null)
                {
                    return ExcelColumnMaps.IO.ToDictionary(
                                    dc => dc.Key,
                                    dc => row.Cell(dc.Value).GetString());
                }
                else { return new Dictionary<string, string>(); }
            }
        }

        public Dictionary<string, string> GetJBTagData(string tag)
        {
            if (String.IsNullOrEmpty(tag))
            {
                return new Dictionary<string, string>();
            }
            else
            {
                var rows = GetJBRows(tag);
                if (rows is not null)
                {
                    return new ExcelJBTagResult(rows).ToDict();
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
        }

        private IXLRow? GetIORow(string tag)
        {
            return IOws.Column(ExcelColumnMaps.IO["TAG_01"]).CellsUsed(cell => cell.GetString() == tag).FirstOrDefault()?.WorksheetRow();
        }

        public IXLRows? GetJBRows(string tag)
        {
            //var c = IOws.Column(ExcelColumnMaps.IO["Tag"]).CellsUsed(cell => cell.GetString() == tag).ToList();//.FirstOrDefault()?.WorksheetRow();
            var rows = JBws.RowsUsed(r => r.Cell(ExcelColumnMaps.JB["TAG_01"]).GetString() == tag);
            return rows;
        }

        public void Dispose() => wb.Dispose();
    }
}
