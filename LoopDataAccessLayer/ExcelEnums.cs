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
        PANEL_TAG = 6,
        BREAKER_NUM = 7,
        TAG_01 = 8,
        PNL_TS_01 = 9,
        PNL_TB_01 = 10,
        PNL_TB_02 = 11,
        PNL_TB_03 = 12,
        IOWireTagPlus = 13,
        IOWireTagNeg = 14,
        IOClrPlus = 15,
        IOClrNeg = 16,
        IOCorePairPlus = 17,
        IOCorePairNeg = 18,

        IOCableTag = 19,
        JB1 = 20,
        JB2 = 21,
        JB3 = 22,
        CableTagField = 23,
        DeviceTerminalPlus = 24,
        DeviceTerminalNeg = 25,
        WireTagPlus = 26,
        WireTagNeg = 27,
        WireColorPlus = 28,
        WireColorNeg = 29,
        CorePairPlus = 30,
        CorePairNeg = 31,
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
