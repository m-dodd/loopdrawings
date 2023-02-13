using ClosedXML.Excel;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelColMapper
    {
        private readonly IXLWorksheet IOws, JBws;
        private const int IOHeaderRow = 2;
        private const int JBHeaderRow = 2;

        public ExcelColMapper(IXLWorksheet IOws, IXLWorksheet JBws)
        {
            this.IOws = IOws;
            this.JBws = JBws;

        }

        public ExcelIOCols GetIOColMap()
        {
            var header = IOws.Row(IOHeaderRow);
            return new ExcelIOCols
            {
                ModuleTerm01 = GetColNumber(header, "IO_MOD_TERM_1") ?? -1,
                ModuleTerm02 = GetColNumber(header, "IO_MOD_TERM_2") ?? -1,
                ModuleWireTag01 = GetColNumber(header, "IO_MOD_WIRE_1") ?? -1,
                ModuleWireTag02 = GetColNumber(header, "IO_MOD_WIRE_2") ?? -1,
                PanelTag = GetColNumber(header, "CABINET") ?? -1,
                BreakerNumber = GetColNumber(header, "BREAKER") ?? -1,
                Tag = GetColNumber(header, "TAG") ?? -1,
                PanelTerminalStrip = GetColNumber(header, "IO_TERM_STRIP") ?? -1,
                JB1 = GetColNumber(header, "JB1") ?? -1,
                JB2 = GetColNumber(header, "JB2") ?? -1,
                JB3 = GetColNumber(header, "JB3") ?? -1,

                Device = new ExcelIODeviceCommon<int>
                {
                    CableTag = GetColNumber(header, "DEVICE_CABLE") ?? -1,
                    TerminalPlus = GetColNumber(header, "DEVICE_TERM_PLUS") ?? -1,
                    TerminalNeg = GetColNumber(header, "DEVICE_TERM_NEG") ?? -1,
                    //TerminalShld = GetColNumber(header, "DEVICE_TERM_SHLD") ?? -1,
                    TerminalShld = 9999, // there is never a shield colunn but this should ensure the string will jsut be mpty  
                    WireTagPlus = GetColNumber(header, "DEVICE_WIRE_PLUS") ?? -1,
                    WireTagNeg = GetColNumber(header, "DEVICE_WIRE_NEG") ?? -1,
                    WireColorPlus = GetColNumber(header, "DEVICE_COLOR_PLUS") ?? -1,
                    WireColorNeg = GetColNumber(header, "DEVICE_COLOR_NEG") ?? -1,
                    CorePairPlus = GetColNumber(header, "DEVICE_CORE_PLUS") ?? -1,
                    CorePairNeg = GetColNumber(header, "DEVICE_CORE_NEG") ?? -1,
                },

                IO = new ExcelIODeviceCommon<int>
                {
                    TerminalPlus = GetColNumber(header, "IO_TERM_PLUS") ?? -1,
                    TerminalNeg = GetColNumber(header, "IO_TERM_NEG") ?? -1,
                    TerminalShld = GetColNumber(header, "IO_TERM_SHLD") ?? -1,
                    WireTagPlus = GetColNumber(header, "IO_TAG_PLUS") ?? -1,
                    WireTagNeg = GetColNumber(header, "IO_TAG_NEG") ?? -1,
                    WireColorPlus = GetColNumber(header, "IO_COLOR_PLUS") ?? -1,
                    WireColorNeg = GetColNumber(header, "IO_COLOR_NEG") ?? -1,
                    CorePairPlus = GetColNumber(header, "IO_PAIR_PLUS") ?? -1,
                    CorePairNeg = GetColNumber(header, "IO_PAIR_NEG") ?? -1,
                    CableTag = GetColNumber(header, "IO_CABLE") ?? -1
                },
            };
        }

        public ExcelJBCols GetJBColMap()
        {
            var header = JBws.Row(IOHeaderRow);
            return new ExcelJBCols
            {

                JBTag = GetColNumber(header, "JB_TAG") ?? -1,
                TerminalStrip = GetColNumber(header, "TS") ?? -1,
                Terminal = GetColNumber(header, "TERMINAL") ?? -1,
                SignalType = GetColNumber(header, "SIGNAL_TYPE") ?? -1,
                DeviceTag = GetColNumber(header, "DEVICE_TAG") ?? -1,
                LeftSide = new ExcelJBRowSide<int>
                {
                    Cable = GetColNumber(header, "LEFT_CABLE") ?? -1,
                    Core = GetColNumber(header, "LEFT_PAIR") ?? -1,
                    Color = GetColNumber(header, "LEFT_COLOR") ?? -1,
                    WireTag = GetColNumber(header, "LEFT_WIRE_TAG") ?? -1,
                },
                RightSide = new ExcelJBRowSide<int>
                {
                    Cable = GetColNumber(header, "RIGHT_CABLE") ?? -1,
                    Core = GetColNumber(header, "RIGHT_PAIR") ?? -1,
                    Color = GetColNumber(header, "RIGHT_COLOR") ?? -1,
                    WireTag = GetColNumber(header, "RIGHT_WIRE_TAG") ?? -1,
                }
            };
        }

        private int? GetColNumber(IXLRow row, string colName)
        {
            return row
                ?.CellsUsed(cell => cell.GetString().ToUpper() == colName.ToUpper())
                ?.FirstOrDefault()
                ?.WorksheetColumn()
                ?.ColumnNumber();
        }
    }
}
