using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    internal enum ExcelIOColumns
    {
        MOD_TERM_01 = 4,
        MOD_TERM_02 = 5,
        MOD_WIRE_TAG_01 = 6,
        MOD_WIRE_TAG_02 = 7,
        PANEL_TAG = 8,
        BREAKER_NUM = 9,
        TAG_01 = 10,
        PNL_TS_01 = 11,
        PNL_TB_01 = 12,
        PNL_TB_02 = 13,
        PNL_TB_03 = 14,
        IOWireTagPlus = 15,
        IOWireTagNeg = 16,
        IOClrPlus = 17,
        IOClrNeg = 18,
        IOCorePairPlus = 19,
        IOCorePairNeg = 20,
        IOCableTag = 21,
        JB1 = 22,
        JB2 = 23,
        JB3 = 24,
        CableTagField = 25,
        DeviceTerminalPlus = 26,
        DeviceTerminalNeg = 27,
        WireTagPlus = 28,
        WireTagNeg = 29,
        WireColorPlus = 30,
        WireColorNeg = 31,
        CorePairPlus = 32,
        CorePairNeg = 33,
    }

    internal enum ExcelJBColumns
    {
        JBTag = 1,
        TerminalStrip = 2,
        Terminal = 3,
        SignalType = 4,
        TAG_01 = 5,
        LeftCable = 6,
        LeftCore = 7,
        LeftColor = 8,
        LeftWireTag = 9,
        RightCable = 10,
        RightCore = 11,
        RightColor = 12,
        RightTag = 13,
    }
}
