using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    

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
                    data = new ExcelIOData<string>()
                    {
                        ModuleTerm01 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.ModuleTerm01),
                        ModuleTerm02 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.ModuleTerm02),
                        ModuleWireTag01 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.ModuleWireTag01),
                        ModuleWireTag02 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.ModuleWireTag02),
                        PanelTag = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PanelTag),
                        BreakerNumber = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.BreakerNumber),
                        Tag  = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Tag),
                        PanelTerminalStrip = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PanelTerminalStrip),
                        JB1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.JB1),
                        JB2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.JB2),
                        JB3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.JB3),

                        PowerTerminalStrip = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerTerminalStrip),
                        PowerVolts = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerVolts),
                        PowerTerm1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerTerm1),
                        PowerTerm2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerTerm2),
                        PowerWireTag1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerWireTag1),
                        PowerWireTag2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerWireTag2),
                        PowerCore1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerCore1),
                        PowerCore2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerCore2),
                        PowerCable = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.PowerCable),

                        Device = new ExcelIODeviceCommon<string>()
                        {
                            CableTag = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.CableTag),
                            Terminal1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.Terminal1),
                            Terminal2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.Terminal2),
                            Terminal3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.Terminal3),
                            Terminal4 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.Terminal4),
                            WireTag1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireTag1),
                            WireTag2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireTag2),
                            WireTag3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireTag3),
                            WireTag4 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireTag4),
                            WireColor1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireColor1),
                            WireColor2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireColor2),
                            WireColor3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.WireColor3),
                            CorePair1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.CorePair1),
                            CorePair2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.CorePair2),
                            CorePair3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.CorePair3),
                            CorePair4 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.CorePair4),
                            PanelTag = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.PanelTag),
                            PanelTerminalStrip = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Device.PanelTerminalStrip),
                        },
                        IO = new ExcelIODeviceCommon<string>()
                        {
                            CableTag = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.CableTag),
                            Terminal1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.Terminal1),
                            Terminal2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.Terminal2),
                            Terminal3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.Terminal3),
                            Terminal4 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.Terminal4),
                            WireTag1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.WireTag1),
                            WireTag2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.WireTag2),
                            WireColor1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.WireColor1),
                            WireColor2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.WireColor2),
                            WireColor3 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.WireColor3),
                            CorePair1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.CorePair1),
                            CorePair2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.IO.CorePair2),
                        },
                        Relay = new ExcelIORelay<string>()
                        {
                            Tag = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.Tag),
                            PanelTerminalStrip = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.PanelTerminalStrip),
                            Term1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.Term1),
                            Term2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.Term2),
                            ContactTag = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.ContactTag),
                            ContactTerm1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.ContactTerm1),
                            ContactTerm2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Relay.ContactTerm2)
                        },
                        Overload = new ExcelOverload<string>
                        {
                            Description1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Overload.Description1),
                            Description2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Overload.Description2),
                            Tag1 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Overload.Tag1),
                            Tag2 = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Overload.Tag2),
                            PortNum = ExcelHelper.GetRowString(row, ColumnMaps.IOColMap.Overload.PortNum),
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
                        .Select(r => ExcelHelper.GetRowString(r, ColumnMaps.JBColMap.JBTag))
                        .Distinct()
                        .Select(jbTag =>
                            new ExcelJBData(
                                rows.Where(r => ExcelHelper.GetRowString(r, ColumnMaps.JBColMap.JBTag) == jbTag),
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
                        CableTag = ExcelHelper.GetRowString(row, ColumnMaps.CableColMap.CableTag),
                        From = ExcelHelper.GetRowString(row, ColumnMaps.CableColMap.From),
                        To = ExcelHelper.GetRowString(row, ColumnMaps.CableColMap.To),
                        Conductors = ExcelHelper.GetRowString(row, ColumnMaps.CableColMap.Conductors),
                        ConductorSize = ExcelHelper.GetRowString(row, ColumnMaps.CableColMap.ConductorSize),
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

            string date = DateTime.Today.ToString("ddMMMyy").ToUpper();
            titleBlockData = new ExcelTitleBlockData<string>()
            {
                SiteNumber = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.SiteNumber),
                Sheet = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.Sheet),
                MaxSheets = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.MaxSheets),
                Project = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.Project),
                Scale = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.Scale),
                CityTown = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.CityTown),
                ProvinceState = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.ProvinceState),

                GeneralRevData = new ExcelTitleBlockRevData<string>()
                {
                    Rev = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.GeneralRevData.Rev),
                    Description = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.GeneralRevData.Description),
                    Date = date,
                    DrawnBy = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.GeneralRevData.DrawnBy),
                    CheckedBy = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.GeneralRevData.CheckedBy),
                    ApprovedBy = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.GeneralRevData.ApprovedBy),
                },
                
                RevBlockRevData = new ExcelTitleBlockRevData<string>()
                {
                    Rev = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.RevBlockRevData.Rev),
                    Description = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.RevBlockRevData.Description),
                    Date = date,
                    DrawnBy = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.RevBlockRevData.DrawnBy),
                    CheckedBy = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.RevBlockRevData.CheckedBy),
                    ApprovedBy = ExcelHelper.GetRowString(row, ColumnMaps.TitleBlockColMap.RevBlockRevData.ApprovedBy),
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
