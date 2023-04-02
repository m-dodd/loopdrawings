using ClosedXML.Excel;
using Irony.Parsing;
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
        private readonly ExcelWorksheets workSheets;
        private const int IOHeaderRow = 2;
        private const int JBHeaderRow = 2;
        private const int CableHeaderRow = 2;
        private const int TitleBlockHeaderRow = 3;
        public int TitleBlockDataRow { get; set; } = 4;

        public ExcelColMapper(ExcelWorksheets workSheets)
        {
            this.workSheets = workSheets;
        }

        public IExcelColMaps GetColumnMaps()
        {
            return new ExcelColMaps()
            {
                IOColMap = GetIOColMap(),
                JBColMap = GetJBColMap(),
                TitleBlockColMap = GetTitleBlockColMap(),
                CableColMap = GetCableColMap(),
            };
        }

        private IExcelIOData<int> GetIOColMap()
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
                PowerTerminalStrip = excelColumnProvider.GetColumnNumber("PWR_TERM_STRIP"),
                PowerVolts = excelColumnProvider.GetColumnNumber("PWR_VOLTS"),
                PowerTerm1 = excelColumnProvider.GetColumnNumber("PWR_TERM_1"),
                PowerTerm2 = excelColumnProvider.GetColumnNumber("PWR_TERM_2"),
                PowerWireTag1 = excelColumnProvider.GetColumnNumber("PWR_TAG_1"),
                PowerWireTag2 = excelColumnProvider.GetColumnNumber("PWR_TAG_2"),
                PowerCore1 = excelColumnProvider.GetColumnNumber("PWR_CORE_1"),
                PowerCore2 = excelColumnProvider.GetColumnNumber("PWR_CORE_2"),
                PowerCable = excelColumnProvider.GetColumnNumber("PWR_CABLE"),

                Device = new ExcelIODeviceCommon<int>
                {
                    CableTag = excelColumnProvider.GetColumnNumber("DEV_CABLE"),
                    Terminal1 = excelColumnProvider.GetColumnNumber("DEV_TERM_1"),
                    Terminal2 = excelColumnProvider.GetColumnNumber("DEV_TERM_2"),
                    Terminal3 = excelColumnProvider.GetColumnNumber("DEV_TERM_3"),
                    Terminal4 = excelColumnProvider.GetColumnNumber("DEV_TERM_4"),
                    WireTag1 = excelColumnProvider.GetColumnNumber("DEV_WIRE_1"),
                    WireTag2 = excelColumnProvider.GetColumnNumber("DEV_WIRE_2"),
                    WireTag3 = excelColumnProvider.GetColumnNumber("DEV_WIRE_3"),
                    WireTag4 = excelColumnProvider.GetColumnNumber("DEV_WIRE_4"),
                    WireColor1 = excelColumnProvider.GetColumnNumber("DEV_COLOR_1"),
                    WireColor2 = excelColumnProvider.GetColumnNumber("DEV_COLOR_2"),
                    WireColor3 = excelColumnProvider.GetColumnNumber("DEV_COLOR_3"),
                    CorePair1 = excelColumnProvider.GetColumnNumber("DEV_CORE_1"),
                    CorePair2 = excelColumnProvider.GetColumnNumber("DEV_CORE_2"),
                    CorePair3 = excelColumnProvider.GetColumnNumber("DEV_CORE_3"),
                    CorePair4 = excelColumnProvider.GetColumnNumber("DEV_CORE_4"),
                },

                IO = new ExcelIODeviceCommon<int>
                {
                    Terminal1 = excelColumnProvider.GetColumnNumber("IO_TERM_1"),
                    Terminal2 = excelColumnProvider.GetColumnNumber("IO_TERM_2"),
                    Terminal3 = excelColumnProvider.GetColumnNumber("IO_TERM_3"),
                    Terminal4 = excelColumnProvider.GetColumnNumber("IO_TERM_4"),
                    WireTag1 = excelColumnProvider.GetColumnNumber("IO_TAG_1"),
                    WireTag2 = excelColumnProvider.GetColumnNumber("IO_TAG_2"),
                    WireColor1 = excelColumnProvider.GetColumnNumber("IO_COLOR_1"),
                    WireColor2 = excelColumnProvider.GetColumnNumber("IO_COLOR_2"),
                    WireColor3 = excelColumnProvider.GetColumnNumber("IO_COLOR_3"),
                    CorePair1 = excelColumnProvider.GetColumnNumber("IO_PAIR_1"),
                    CorePair2 = excelColumnProvider.GetColumnNumber("IO_PAIR_2"),
                    CableTag = excelColumnProvider.GetColumnNumber("IO_CABLE")
                },

                Relay = new ExcelIORelay<int>
                {
                    Tag = excelColumnProvider.GetColumnNumber("RELAY_TAG"),
                    PanelTerminalStrip = excelColumnProvider.GetColumnNumber("RELAY_TS"),
                    Term1 = excelColumnProvider.GetColumnNumber("RELAY_TERM1"),
                    Term2 = excelColumnProvider.GetColumnNumber("RELAY_TERM2"),
                    ContactTag = excelColumnProvider.GetColumnNumber("RELAY_CONTACT_TAG"),
                    ContactTerm1 = excelColumnProvider.GetColumnNumber("RELAY_CONTACT_TERM1"),
                    ContactTerm2 = excelColumnProvider.GetColumnNumber("RELAY_CONTACT_TERM2")
                }
            };
        }

        private IExcelJBRowData<int> GetJBColMap()
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

        private IExcelTitleBlockData<int> GetTitleBlockColMap()
        {
            ExcelColumnProvider excelColumnProvider = new(GetTitleBlockHeaderRow());

            return new ExcelTitleBlockData<int>()
            {
                SiteNumber = excelColumnProvider.GetColumnNumber("SITE_NUM"),
                Sheet = excelColumnProvider.GetColumnNumber("SHEET"),
                MaxSheets = excelColumnProvider.GetColumnNumber("MAX_SHEETS"),
                Project = excelColumnProvider.GetColumnNumber("PROJECT"),
                Scale = excelColumnProvider.GetColumnNumber("SCALE"),
                CityTown = excelColumnProvider.GetColumnNumber("CITY_TOWN"),
                ProvinceState = excelColumnProvider.GetColumnNumber("PROVINCE_STATE"),

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

        private IExcelCableData<int> GetCableColMap()
        {
            ExcelColumnProvider excelColumnProvider = new(GetCableHeaderRow());

            return new ExcelCableData<int>()
            {
                CableTag = excelColumnProvider.GetColumnNumber("CABLE_TAG"),
                From = excelColumnProvider.GetColumnNumber("FROM"),
                To = excelColumnProvider.GetColumnNumber("TO"),
                Conductors = excelColumnProvider.GetColumnNumber("CONDUCTORS"),
                ConductorSize = excelColumnProvider.GetColumnNumber("SIZE"),
            };
        }

        private IXLRow GetIOHeaderRow() => workSheets.IOws.Row(IOHeaderRow);
        private IXLRow GetJBHeaderRow() => workSheets.JBws.Row(JBHeaderRow);
        private IXLRow GetTitleBlockHeaderRow() => workSheets.TitleBlockWS.Row(TitleBlockHeaderRow);
        private IXLRow GetCableHeaderRow() => workSheets.CableWS.Row(CableHeaderRow);
    }
}
