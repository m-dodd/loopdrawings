using ClosedXML.Excel;
using Irony.Parsing;
using LoopDataAccessLayer.src.DataLoader.ExcelLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelColMapper : IExcelColMapper
    {
        private readonly IXLWorksheet IOws, JBws, titleBlockWS;
        private const int IOHeaderRow = 2;
        private const int JBHeaderRow = 2;
        private const int TitleBlockHeaderRow = 3;
        public int TitleBlockDataRow { get; set; } = 4;

        public ExcelColMapper(IXLWorksheet IOws, IXLWorksheet JBws, IXLWorksheet TitleBlockWS)
        {
            this.IOws = IOws;
            this.JBws = JBws;
            this.titleBlockWS = TitleBlockWS;
        }

        public IExcelIOData<int> GetIOColMap()
        {
            ExcelColumnProvider excelColumnProvider = new(GetIOHeaderRow());
            return new ExcelIOData<int>
            {
                ModuleTerm01 = excelColumnProvider.GetColumnNumber("IO_MOD_TERM_1"),
                ModuleTerm02 = excelColumnProvider.GetColumnNumber("IO_MOD_TERM_2"),
                ModuleWireTag01 = excelColumnProvider.GetColumnNumber("IO_MOD_WIRE_1"),
                ModuleWireTag02 = excelColumnProvider.GetColumnNumber("IO_MOD_WIRE_2"),
                PanelTag = excelColumnProvider.GetColumnNumber("CABINET"),
                BreakerNumber = excelColumnProvider.GetColumnNumber("BREAKER"),
                Tag = excelColumnProvider.GetColumnNumber("TAG"),
                PanelTerminalStrip = excelColumnProvider.GetColumnNumber("IO_TERM_STRIP"),
                JB1 = excelColumnProvider.GetColumnNumber("JB1"),
                JB2 = excelColumnProvider.GetColumnNumber("JB2"),
                JB3 = excelColumnProvider.GetColumnNumber("JB3"),

                Device = new ExcelIODeviceCommon<int>
                {
                    CableTag = excelColumnProvider.GetColumnNumber("DEVICE_CABLE"),
                    TerminalPlus = excelColumnProvider.GetColumnNumber("DEVICE_TERM_PLUS"),
                    TerminalNeg = excelColumnProvider.GetColumnNumber("DEVICE_TERM_NEG"),
                    TerminalShld = 9999, // there is never a shield colunn but this should ensure the string will jsut be mpty  
                    WireTagPlus = excelColumnProvider.GetColumnNumber("DEVICE_WIRE_PLUS"),
                    WireTagNeg = excelColumnProvider.GetColumnNumber("DEVICE_WIRE_NEG"),
                    WireColorPlus = excelColumnProvider.GetColumnNumber("DEVICE_COLOR_PLUS"),
                    WireColorNeg = excelColumnProvider.GetColumnNumber("DEVICE_COLOR_NEG"),
                    CorePairPlus = excelColumnProvider.GetColumnNumber("DEVICE_CORE_PLUS"),
                    CorePairNeg = excelColumnProvider.GetColumnNumber("DEVICE_CORE_NEG"),
                },

                IO = new ExcelIODeviceCommon<int>
                {
                    TerminalPlus = excelColumnProvider.GetColumnNumber("IO_TERM_PLUS"),
                    TerminalNeg = excelColumnProvider.GetColumnNumber("IO_TERM_NEG"),
                    TerminalShld = excelColumnProvider.GetColumnNumber("IO_TERM_SHLD"),
                    WireTagPlus = excelColumnProvider.GetColumnNumber("IO_TAG_PLUS"),
                    WireTagNeg = excelColumnProvider.GetColumnNumber("IO_TAG_NEG"),
                    WireColorPlus = excelColumnProvider.GetColumnNumber("IO_COLOR_PLUS"),
                    WireColorNeg = excelColumnProvider.GetColumnNumber("IO_COLOR_NEG"),
                    CorePairPlus = excelColumnProvider.GetColumnNumber("IO_PAIR_PLUS"),
                    CorePairNeg = excelColumnProvider.GetColumnNumber("IO_PAIR_NEG"),
                    CableTag = excelColumnProvider.GetColumnNumber("IO_CABLE")
                },
            };
        }

        public IExcelJBRowData<int> GetJBColMap()
        {
            ExcelColumnProvider excelColumnProvider = new(GetJBHeaderRow());

            return new ExcelJBRowData<int>
            {

                JBTag = excelColumnProvider.GetColumnNumber("JB_TAG"),
                TerminalStrip = excelColumnProvider.GetColumnNumber("TS"),
                Terminal = excelColumnProvider.GetColumnNumber("TERMINAL"),
                SignalType = excelColumnProvider.GetColumnNumber("SIGNAL_TYPE"),
                DeviceTag = excelColumnProvider.GetColumnNumber("DEVICE_TAG"),
                
                LeftSide = new ExcelJBRowSide<int>
                {
                    Cable = excelColumnProvider.GetColumnNumber("LEFT_CABLE"),
                    Core = excelColumnProvider.GetColumnNumber("LEFT_PAIR"),
                    Color = excelColumnProvider.GetColumnNumber("LEFT_COLOR"),
                    WireTag = excelColumnProvider.GetColumnNumber("LEFT_WIRE_TAG"),
                },
                
                RightSide = new ExcelJBRowSide<int>
                {
                    Cable = excelColumnProvider.GetColumnNumber("RIGHT_CABLE"),
                    Core = excelColumnProvider.GetColumnNumber("RIGHT_PAIR"),
                    Color = excelColumnProvider.GetColumnNumber("RIGHT_COLOR"),
                    WireTag = excelColumnProvider.GetColumnNumber("RIGHT_WIRE_TAG"),
                }
            };
        }

        public IExcelTitleBlockData<int> GetTitleBlockColMap()
        {
            ExcelColumnProvider excelColumnProvider = new(GetTitleBlockHeaderRow());

            return new ExcelTitleBlockData<int>()
            {
                SiteNumber = excelColumnProvider.GetColumnNumber("SITE_NUM"),
                Sheet = excelColumnProvider.GetColumnNumber("SHEET"),
                MaxSheets = excelColumnProvider.GetColumnNumber("MAX_SHEETS"),
                Project = excelColumnProvider.GetColumnNumber("PROJECT"),
                Scale = excelColumnProvider.GetColumnNumber("SCALE"),
                
                GeneralRevData = new ExcelTitleBlockRevData<int>()
                {
                    Rev = excelColumnProvider.GetColumnNumber("GENERAL_REV"),
                    Description = excelColumnProvider.GetColumnNumber("GENERAL_DESCRIPTION"),
                    Date = excelColumnProvider.GetColumnNumber("GENERAL_DATE"),
                    DrawnBy = excelColumnProvider.GetColumnNumber("GENERAL_DRAWNBY"),
                    CheckedBy = excelColumnProvider.GetColumnNumber("GENERAL_CHECKEDBY"),
                    ApprovedBy = excelColumnProvider.GetColumnNumber("GENERAL_APPROVEDBY"),
                },
                RevBlockRevData = new ExcelTitleBlockRevData<int>()
                {
                    Rev = excelColumnProvider.GetColumnNumber("REV_REV"),
                    Description = excelColumnProvider.GetColumnNumber("REV_DESCRIPTION"),
                    Date = excelColumnProvider.GetColumnNumber("REV_DATE"),
                    DrawnBy = excelColumnProvider.GetColumnNumber("REV_DRAWNBY"),
                    CheckedBy = excelColumnProvider.GetColumnNumber("REV_CHECKEDBY"),
                    ApprovedBy = excelColumnProvider.GetColumnNumber("REV_APPROVEDBY"),
                },
            };
        }

        private IXLRow GetIOHeaderRow() => IOws.Row(IOHeaderRow);
        private IXLRow GetJBHeaderRow() => JBws.Row(JBHeaderRow);
        private IXLRow GetTitleBlockHeaderRow() => titleBlockWS.Row(TitleBlockHeaderRow);
    }
    
    public interface IExcelColumnProvider
    {
        int GetColumnNumber(string columnName);
    }
    public class ExcelColumnProvider
    {
        private readonly IXLRow headerRow;
        public ExcelColumnProvider(IXLRow header)
        {
            this.headerRow = header;
        }

        public int GetColumnNumber(string columnName)
        {
            int? colNum = headerRow
                    ?.CellsUsed(cell => cell.GetString().ToUpper() == columnName.ToUpper())
                    ?.FirstOrDefault()
                    ?.WorksheetColumn()
                    ?.ColumnNumber();

            if (colNum is null)
            {
                throw new ExcelColumnNotFoundException(columnName);
            }

            return (int)colNum;
        }
    }

    public class ExcelColumnNotFoundException : Exception
    {
        public ExcelColumnNotFoundException()
        {
        }

        public ExcelColumnNotFoundException(string? message) : base(message)
        {
        }

        public ExcelColumnNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExcelColumnNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
