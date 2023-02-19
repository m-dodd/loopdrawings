using ClosedXML.Excel;
using Irony.Parsing;
using LoopDataAccessLayer.src.DataLoader.ExcelLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    

    public class ExcelDataLoader : IExcelLoader, IDisposable
    {
        private readonly IXLWorksheet IOws, JBws, TitleBlockWS;
        private readonly XLWorkbook wb;
        private IExcelColMapper colMapper;
        
        private readonly IDictionary<string, IXLRow?> ioRowData;
        private readonly IDictionary<string, IXLRows?> jbRowsData;
        private readonly IDictionary<string, IExcelIOData<string>?> ioData;
        private readonly IDictionary<string, List<ExcelJBData>?> jbData;
        
        private IExcelTitleBlockData<string> titleBlockData;
        private bool titleBlockDataPopulated;

        public IExcelJBRowData<int> ExcelJBCols { get; private set; }
        public IExcelIOData<int> ExcelIOCols { get; private set; }
        public IExcelTitleBlockData<int> ExcelTitleBlockCols { get; private set; }
        

        public ExcelDataLoader(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be null or empty", nameof(fileName));

            if (!ExcelHelper.IsExcelFile(fileName))
                throw new ArgumentException("File is not an excel file", nameof(fileName));
            
            this.wb = new XLWorkbook(fileName);
            this.IOws = wb.Worksheet("IO_Wiring_Devices");
            this.JBws = wb.Worksheet("JB Wiring");
            this.TitleBlockWS = wb.Worksheet("Titleblock");

            colMapper = new ExcelColMapper(IOws, JBws, TitleBlockWS);
            ExcelIOCols = colMapper.GetIOColMap();
            ExcelJBCols = colMapper.GetJBColMap();
            ExcelTitleBlockCols = colMapper.GetTitleBlockColMap();

            ioRowData = new Dictionary<string, IXLRow?>();
            jbRowsData = new Dictionary<string, IXLRows?>();
            ioData = new Dictionary<string, IExcelIOData<string>?>();
            jbData = new Dictionary<string, List<ExcelJBData>?>();

            titleBlockData = new ExcelTitleBlockData<string>();
            titleBlockDataPopulated = false;
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
                    data = new ExcelIOData<string>()
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

                        Device = new ExcelIODeviceCommon<string>()
                        {
                            CableTag = ExcelHelper.GetRowString(row, ExcelIOCols.Device.CableTag),
                            TerminalPlus = ExcelHelper.GetRowString(row, ExcelIOCols.Device.TerminalPlus),
                            TerminalNeg = ExcelHelper.GetRowString(row, ExcelIOCols.Device.TerminalNeg),
                            TerminalShld = ExcelHelper.GetRowString(row, ExcelIOCols.Device.TerminalShld),
                            WireTagPlus = ExcelHelper.GetRowString(row, ExcelIOCols.Device.WireTagPlus),
                            WireTagNeg = ExcelHelper.GetRowString(row, ExcelIOCols.Device.WireTagNeg),
                            WireColorPlus = ExcelHelper.GetRowString(row, ExcelIOCols.Device.WireColorPlus),
                            WireColorNeg = ExcelHelper.GetRowString(row, ExcelIOCols.Device.WireColorNeg),
                            CorePairPlus = ExcelHelper.GetRowString(row, ExcelIOCols.Device.CorePairPlus),
                            CorePairNeg = ExcelHelper.GetRowString(row, ExcelIOCols.Device.CorePairNeg)
                        },
                        IO = new ExcelIODeviceCommon<string>()
                        {
                            CableTag = ExcelHelper.GetRowString(row, ExcelIOCols.IO.CableTag),
                            TerminalPlus = ExcelHelper.GetRowString(row, ExcelIOCols.IO.TerminalPlus),
                            TerminalNeg = ExcelHelper.GetRowString(row, ExcelIOCols.IO.TerminalNeg),
                            TerminalShld = ExcelHelper.GetRowString(row, ExcelIOCols.IO.TerminalShld),
                            WireTagPlus = ExcelHelper.GetRowString(row, ExcelIOCols.IO.WireTagPlus),
                            WireTagNeg = ExcelHelper.GetRowString(row, ExcelIOCols.IO.WireTagNeg),
                            WireColorPlus = ExcelHelper.GetRowString(row, ExcelIOCols.IO.WireColorPlus),
                            WireColorNeg = ExcelHelper.GetRowString(row, ExcelIOCols.IO.WireColorNeg),
                            CorePairPlus = ExcelHelper.GetRowString(row, ExcelIOCols.IO.CorePairPlus),
                            CorePairNeg = ExcelHelper.GetRowString(row, ExcelIOCols.IO.CorePairNeg)
                        }
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

        public IExcelTitleBlockData<string> GetTitleBlockData()
        {
            return titleBlockDataPopulated ? titleBlockData : FetchTitleBlockData();
        }

        private IXLRow? GetTitleBlockDataRow()
        {
            return TitleBlockWS
                ?.Column(ExcelTitleBlockCols.SiteNumber)
                ?.CellsUsed()
                ?.LastOrDefault()
                ?.WorksheetRow();
        }

        private IExcelTitleBlockData<string> FetchTitleBlockData()
        {
            IXLRow? row = GetTitleBlockDataRow();
            if (row is null)
            {
                throw new NullReferenceException("Cannot find titleblock row data.");
            }

            titleBlockData = new ExcelTitleBlockData<string>()
            {
                SiteNumber = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.SiteNumber),
                Sheet = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.Sheet),
                MaxSheets = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.MaxSheets),
                Project = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.Project),
                Scale = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.Scale),
                GeneralRevData = new ExcelTitleBlockRevData<string>()
                {
                    Rev = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.GeneralRevData.Rev),
                    Description = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.GeneralRevData.Description),
                    //Date = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.GeneralRevData.Date),
                    Date = DateTime.Today.ToString("ddMMyy"),
                    DrawnBy = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.GeneralRevData.DrawnBy),
                    CheckedBy = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.GeneralRevData.CheckedBy),
                    ApprovedBy = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.GeneralRevData.ApprovedBy),
                },
                RevBlockRevData = new ExcelTitleBlockRevData<string>()
                {
                    Rev = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.RevBlockRevData.Rev),
                    Description = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.RevBlockRevData.Description),
                    Date = DateTime.Today.ToString("ddMMyy"),
                    DrawnBy = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.RevBlockRevData.DrawnBy),
                    CheckedBy = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.RevBlockRevData.CheckedBy),
                    ApprovedBy = ExcelHelper.GetRowString(row, ExcelTitleBlockCols.RevBlockRevData.ApprovedBy),
                },
            };

            titleBlockDataPopulated = true;
            
            return titleBlockData;
        }


        public void Dispose()
        {
            wb.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
