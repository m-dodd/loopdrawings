using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using Irony.Parsing;
using Org.BouncyCastle.Security;
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
    public class ExcelDataLoader : IExcelDataLoader, IDisposable
    {
        private readonly XLWorkbook wb;
        private readonly ExcelWorksheets workSheets;
        private readonly IExcelColMaps ColumnMaps;
        
        private readonly IDictionary<string, IXLRow?> ioRowData;
        private readonly IDictionary<string, IExcelIOData<string>?> ioData;
        
        private readonly IDictionary<string, IXLRows?> jbRowsData;
        private readonly IDictionary<string, List<ExcelJBData>?> jbData;

        private readonly IDictionary<string, IXLRow?> cableRowData;
        private readonly IDictionary<string, IExcelCableData<string>?> cableData;

        private IExcelTitleBlockData<string> titleBlockData;
        private bool titleBlockDataPopulated;

        public IExcelJBRowData<int> ExcelJBCols { get; private set; }
        public IExcelIOData<int> ExcelIOCols { get; private set; }

        public ExcelDataLoader(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be null or empty", nameof(fileName));

            if (!ExcelHelper.IsExcelFile(fileName))
                throw new ArgumentException("File is not an excel file", nameof(fileName));
            
            this.wb = new XLWorkbook(fileName);
            this.workSheets = new ExcelWorksheets(wb);

            ExcelColMapper colMapper = new(workSheets);
            ColumnMaps = colMapper.GetColumnMaps();
            ExcelIOCols = ColumnMaps.IOColMap;
            ExcelJBCols = ColumnMaps.JBColMap;

            ioRowData = new Dictionary<string, IXLRow?>();
            ioData = new Dictionary<string, IExcelIOData<string>?>();
            
            jbRowsData = new Dictionary<string, IXLRows?>();
            jbData = new Dictionary<string, List<ExcelJBData>?>();

            cableRowData = new Dictionary<string, IXLRow?>();
            cableData = new Dictionary<string, IExcelCableData<string>?>();

            titleBlockData = new ExcelTitleBlockData<string>();
            titleBlockDataPopulated = false;
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
                    var ioMap = ColumnMaps.IOColMap;
                    data = new ExcelIOData<string>()
                    {
                        ModuleTerm01 = row.GetCellString(ioMap.ModuleTerm01),
                        ModuleTerm02 = row.GetCellString(ioMap.ModuleTerm02),
                        ModuleWireTag01 = row.GetCellString(ioMap.ModuleWireTag01),
                        ModuleWireTag02 = row.GetCellString(ioMap.ModuleWireTag02),
                        PanelTag = row.GetCellString(ioMap.PanelTag),
                        BreakerNumber = row.GetCellString(ioMap.BreakerNumber),
                        Tag  = row.GetCellString(ioMap.Tag),
                        PanelTerminalStrip = row.GetCellString(ioMap.PanelTerminalStrip),
                        JB1 = row.GetCellString(ioMap.JB1),
                        JB2 = row.GetCellString(ioMap.JB2),
                        JB3 = row.GetCellString(ioMap.JB3),

                        PowerTerminalStrip = row.GetCellString(ioMap.PowerTerminalStrip),
                        PowerVolts = row.GetCellString(ioMap.PowerVolts),
                        PowerTerm1 = row.GetCellString(ioMap.PowerTerm1),
                        PowerTerm2 = row.GetCellString(ioMap.PowerTerm2),
                        PowerWireTag1 = row.GetCellString(ioMap.PowerWireTag1),
                        PowerWireTag2 = row.GetCellString(ioMap.PowerWireTag2),
                        PowerCore1 = row.GetCellString(ioMap.PowerCore1),
                        PowerCore2 = row.GetCellString(ioMap.PowerCore2),
                        PowerCable = row.GetCellString(ioMap.PowerCable),
                        ESDDrawing = row.GetCellString(ioMap.ESDDrawing),

                        Device = CreateExcelIODeviceCommon(row, ioMap.Device),
                        IO = CreateExcelIODeviceCommon(row, ioMap.IO),

                        Relay = new ExcelIORelay<string>()
                        {
                            Tag = row.GetCellString(ioMap.Relay.Tag),
                            PanelTerminalStrip = row.GetCellString(ioMap.Relay.PanelTerminalStrip),
                            Term1 = row.GetCellString(ioMap.Relay.Term1),
                            Term2 = row.GetCellString(ioMap.Relay.Term2),
                            ContactTag = row.GetCellString(ioMap.Relay.ContactTag),
                            ContactTerm1 = row.GetCellString(ioMap.Relay.ContactTerm1),
                            ContactTerm2 = row.GetCellString(ioMap.Relay.ContactTerm2)
                        },
                        Overload = new ExcelOverload<string>
                        {
                            Description1 = row.GetCellString(ioMap.Overload.Description1),
                            Description2 = row.GetCellString(ioMap.Overload.Description2),
                            Tag1 = row.GetCellString(ioMap.Overload.Tag1),
                            Tag2 = row.GetCellString(ioMap.Overload.Tag2),
                            PortNum = row.GetCellString(ioMap.Overload.PortNum),
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

        private static ExcelIODeviceCommon<string> CreateExcelIODeviceCommon(IXLRow row, IExcelIODeviceCommon<int> ioDeviceCols)
        {
            return new ExcelIODeviceCommon<string>
            {
                CableTag = row.GetCellString(ioDeviceCols.CableTag),
                Terminal1 = row.GetCellString(ioDeviceCols.Terminal1),
                Terminal2 = row.GetCellString(ioDeviceCols.Terminal2),
                Terminal3 = row.GetCellString(ioDeviceCols.Terminal3),
                Terminal4 = row.GetCellString(ioDeviceCols.Terminal4),
                Terminal6 = row.GetCellString(ioDeviceCols.Terminal5),
                Terminal5 = row.GetCellString(ioDeviceCols.Terminal6),
                WireTag1 = row.GetCellString(ioDeviceCols.WireTag1),
                WireTag2 = row.GetCellString(ioDeviceCols.WireTag2),
                WireTag3 = row.GetCellString(ioDeviceCols.WireTag3),
                WireTag4 = row.GetCellString(ioDeviceCols.WireTag4),
                WireColor1 = row.GetCellString(ioDeviceCols.WireColor1),
                WireColor2 = row.GetCellString(ioDeviceCols.WireColor2),
                WireColor3 = row.GetCellString(ioDeviceCols.WireColor3),
                WireColor4 = row.GetCellString(ioDeviceCols.WireColor4),
                CorePair1 = row.GetCellString(ioDeviceCols.CorePair1),
                CorePair2 = row.GetCellString(ioDeviceCols.CorePair2),
                CorePair3 = row.GetCellString(ioDeviceCols.CorePair3),
                CorePair4 = row.GetCellString(ioDeviceCols.CorePair4),
                PanelTag = row.GetCellString(ioDeviceCols.PanelTag),
                PanelTerminalStrip = row.GetCellString(ioDeviceCols.PanelTerminalStrip),
            };
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
                    data = rows
                        .GroupBy(r => r.GetCellString(ColumnMaps.JBColMap.JBTag))
                        .Select(group =>
                            new ExcelJBData(
                                group,
                                ColumnMaps.JBColMap
                            )
                        )
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

        public IExcelCableData<string>? GetCableData(string tag)
        {
            if (cableData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                var row = GetCableRow(tag);
                if (row is not null)
                {
                    data = new ExcelCableData<string>()
                    {
                        CableTag = row.GetCellString(ColumnMaps.CableColMap.CableTag),
                        From = row.GetCellString(ColumnMaps.CableColMap.From),
                        To = row.GetCellString(ColumnMaps.CableColMap.To),
                        Conductors = row.GetCellString(ColumnMaps.CableColMap.Conductors),
                        ConductorSize = row.GetCellString(ColumnMaps.CableColMap.ConductorSize),
                    };
                }
                else
                {
                    data = null;
                }
                cableData.Add(tag, data);
                return data;
            }
        }

        public IXLRow? GetIORow(string tag)
        {
            if (ioRowData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = workSheets.IOws
                    ?.Column(ColumnMaps.IOColMap.Tag)
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
                data = workSheets.JBws
                    ?.RowsUsed(r => r.Cell(ColumnMaps.JBColMap.DeviceTag)?.GetString() == tag);
                jbRowsData.Add(tag, data);
                return data;
            }
        }

        private IXLRow? GetCableRow(string cable)
        {
            if (cableRowData.TryGetValue(cable, out var data))
            {
                return data;
            }
            else
            {
                data = workSheets.CableWS
                    ?.Column(ColumnMaps.CableColMap.CableTag)
                    ?.CellsUsed(cell => cell.GetString() == cable)
                    ?.FirstOrDefault()
                    ?.WorksheetRow();
                cableRowData.Add(cable, data);
                return data;
            }
        }

        private IXLRow? GetTitleBlockDataRow()
        {
            return workSheets.TitleBlockWS
                ?.Column(ColumnMaps.TitleBlockColMap.SiteNumber)
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
                SiteNumber = row.GetCellString(ColumnMaps.TitleBlockColMap.SiteNumber),
                Sheet = row.GetCellString(ColumnMaps.TitleBlockColMap.Sheet),
                MaxSheets = row.GetCellString(ColumnMaps.TitleBlockColMap.MaxSheets),
                Project = row.GetCellString(ColumnMaps.TitleBlockColMap.Project),
                Scale = row.GetCellString(ColumnMaps.TitleBlockColMap.Scale),
                CityTown = row.GetCellString(ColumnMaps.TitleBlockColMap.CityTown),
                ProvinceState = row.GetCellString(ColumnMaps.TitleBlockColMap.ProvinceState),
                GeneralRevData = CreateExcelTitleBlockRevData(row, ColumnMaps.TitleBlockColMap.GeneralRevData),
                RevBlockRevData = CreateExcelTitleBlockRevData(row, ColumnMaps.TitleBlockColMap.RevBlockRevData),
            };

            titleBlockDataPopulated = true;
            
            return titleBlockData;
        }

        private static ExcelTitleBlockRevData<string> CreateExcelTitleBlockRevData(IXLRow row, IExcelTitleBlockRevData<int> revdata)
        {
            string date = row.GetCellString(revdata.Date);
            
            return new ExcelTitleBlockRevData<string>()
            {
                Rev = row.GetCellString(revdata.Rev),
                Description = row.GetCellString(revdata.Description),
                Date = date,
                DrawnBy = row.GetCellString(revdata.DrawnBy),
                CheckedBy = row.GetCellString(revdata.CheckedBy),
                ApprovedBy = row.GetCellString(revdata.ApprovedBy),
            };
        }


        public void Dispose()
        {
            wb.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
