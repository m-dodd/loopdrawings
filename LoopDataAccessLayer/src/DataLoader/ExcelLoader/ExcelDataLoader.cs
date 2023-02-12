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
        private readonly IDictionary<string, IXLRow?> ioRowData;
        private readonly IDictionary<string, IXLRows?> jbRowsData;
        private readonly IDictionary<string, IExcelIOData<string>?> ioData;
        private readonly IDictionary<string, List<ExcelJBData>?> jbData;
        //private readonly IExcelIOData<int> excelIOCols;
        
        public IExcelJBRowData<int> ExcelJBCols { get; private set; }
        public IExcelIOData<int> ExcelIOCols { get; private set; }

        public ExcelDataLoader(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be null or empty", nameof(fileName));

            if (!ExcelHelper.IsExcelFile(fileName))
                throw new ArgumentException("File is not an excel file", nameof(fileName));
            
            this.wb = new XLWorkbook(fileName);
            this.IOws = wb.Worksheet("IO_Wiring_Devices");
            this.JBws = wb.Worksheet("JB Wiring");
            ioRowData = new Dictionary<string, IXLRow?>();
            jbRowsData = new Dictionary<string, IXLRows?>();
            ioData = new Dictionary<string, IExcelIOData<string>?>();
            jbData = new Dictionary<string, List<ExcelJBData>?>();


            ExcelColMapper colMapper = new(IOws, JBws);
            ExcelIOCols = colMapper.GetIOColMap();
            ExcelJBCols = colMapper.GetJBColMap();
        }

        public IXLRow? GetIORow(string tag)
        {
            if (ioRowData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = IOws?.Column(ExcelIOCols.Tag)
                           ?.CellsUsed(cell => cell.GetString() == tag)
                           ?.FirstOrDefault()
                           ?.WorksheetRow();
                ioRowData.Add(tag, data);
                return data;
            }
        }

        public IXLRows? GetJBRows(string tag)
        {
            if (jbRowsData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = JBws?.RowsUsed(r => r.Cell(ExcelJBCols.DeviceTag)?.GetString() == tag);
                jbRowsData.Add(tag, data);
                return data;
            }
        }

        public IExcelIOData<string>? GetIOData(string tag)
        {
            if (ioData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                var row = GetIORow(tag);
                if (row is not null)
                {
                    data = new ExcelIOData()
                    {
                        ModuleTerm01 = ExcelHelper.GetRowString(row, ExcelIOCols.ModuleTerm01),
                        ModuleTerm02 = ExcelHelper.GetRowString(row, ExcelIOCols.ModuleTerm02),
                        ModuleWireTag01 = ExcelHelper.GetRowString(row, ExcelIOCols.ModuleWireTag01),
                        ModuleWireTag02 = ExcelHelper.GetRowString(row, ExcelIOCols.ModuleWireTag02),
                        PanelTag = ExcelHelper.GetRowString(row, ExcelIOCols.PanelTag),
                        BreakerNumber = ExcelHelper.GetRowString(row, ExcelIOCols.BreakerNumber),
                        Tag  = ExcelHelper.GetRowString(row, ExcelIOCols.Tag),
                        PanelTerminalStrip = ExcelHelper.GetRowString(row, ExcelIOCols.PanelTerminalStrip),
                        JB1 = ExcelHelper.GetRowString(row, ExcelIOCols.JB1),
                        JB2 = ExcelHelper.GetRowString(row, ExcelIOCols.JB2),
                        JB3 = ExcelHelper.GetRowString(row, ExcelIOCols.JB3),

                        Device = new ExcelIODeviceCommonMapped(row, ExcelIOCols.Device),
                        IO = new ExcelIODeviceCommonMapped(row, ExcelIOCols.IO)
                    };
                }
                else
                {
                    data = null;
                }
                ioData.Add(tag, data);
                return data;
            }
        }


        public List<ExcelJBData>? GetJBData(string tag)
        {
            if (jbData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                var rows = GetJBRows(tag);
                if (rows is null)
                {
                    data = null;
                }
                else
                {
                    data =  rows
                        .Select(r => ExcelHelper.GetRowString(r, ExcelJBCols.JBTag))
                        .Distinct()
                        .Select(jbTag => new ExcelJBData(rows.Where(r => ExcelHelper.GetRowString(r, ExcelJBCols.JBTag) == jbTag), ExcelJBCols))
                        .ToList();
                }
                jbData.Add(tag, data);
                return data;
            }
        }

        //private ExcelJBRowData? GetJBData(IXLRow row, string tag)
        //{

        //    if (row is not null)
        //    {
        //        return new ExcelJBRowData()
        //        {
        //            JBTag = ExcelHelper.GetRowString(row, excelJBCols.JBTag),
        //            TerminalStrip = ExcelHelper.GetRowString(row, excelJBCols.TerminalStrip),
        //            Terminal = ExcelHelper.GetRowString(row, excelJBCols.Terminal),
        //            SignalType = ExcelHelper.GetRowString(row, excelJBCols.SignalType),
        //            DeviceTag = ExcelHelper.GetRowString(row, excelJBCols.DeviceTag),
        //            LeftSide = new ExcelJBRowSideMapped(row, excelJBCols.LeftSide),
        //            RightSide = new ExcelJBRowSideMapped(row, excelJBCols.RightSide),
        //        };
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public void Dispose()
        {
            wb.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
