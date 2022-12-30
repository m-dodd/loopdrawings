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


    //internal static class ExcelColumnMaps
    //{
        //internal static readonly Dictionary<string, int> IO = new Dictionary<string, int>()
        //{
        //    {"MOD_TERM_01", 4},
        //    {"MOD_TERM_02", 5},
        //    {"Cabinet", 6},
        //    {"BREAKER_01", 7},
        //    {"TAG_01", 8},
        //    {"PNL_TS_01", 9},
        //    {"PNL_TB_01", 10},
        //    {"PNL_TB_02", 11},
        //    {"PNL_TB_03", 12},
        //    {"IOWireTagPlus", 13},
        //    {"IOWireTagNeg", 14},
        //    {"IOClrPlus", 15},
        //    {"IOClrNeg", 16},
        //    {"IOCorePairPlus", 17},
        //    {"IOCorePairPlus", 18},
        //    {"IOCableTag", 19},
        //    {"JB1", 20},
        //    {"JB2", 21},
        //    {"JB3", 22},
        //    {"CableTag", 23},
        //    {"TerminalPlus", 24},
        //    {"TerminalNeg", 25},
        //    {"WireTagPlus", 26},
        //    {"WireTagNeg", 27},
        //    {"WireColorPlus", 28},
        //    {"WireColorNeg", 29},
        //    {"CorePairPlus", 30},
        //    {"CorePairNeg", 31},
        //};
        //internal static readonly Dictionary<string, int> JB = new Dictionary<string, int>()
        //{
        //    {"JBTag", 1},
        //    {"TerminalStrip", 2 },
        //    {"Terminal", 3},
        //    {"SignalType", 4},
        //    {"TAG_01", 5},
        //    {"LeftCable", 6},
        //    {"LeftCore", 7},
        //    {"LeftColor", 8},
        //    {"LeftWireTag", 9},
        //    {"RightCable", 10},
        //    {"RightCore", 11},
        //    {"RightColor", 12},
        //    {"RightTag", 13},
        //};
    //}


    //internal class ExcelJBTagResult
    //{
    //    private readonly IXLRows? rows;
    //    public ExcelJBTagResult(IXLRows? rows)
    //    {
    //        this.rows = rows;
    //    }

    //    public Dictionary<string, string> ToDict()
    //    {
    //        if (rows is null) { return new Dictionary<string, string>(); }
    //        else
    //        {

    //            int i = 1;
    //            Dictionary<string, string> result = new()
    //            {
    //                ["JB_TS_01"] = GetJBRowString(rows.First(), "TerminalStrip")
    //            };
    //            foreach (var row in rows.OrderBy(r => GetJBRowString(r, "TAG_01")))
    //            {
    //                string i_str = i.ToString("00");
    //                result["JB_TB_" + i_str] = GetJBRowString(row, "Terminal");
    //                if (!GetJBRowString(row, "SignalType").ToLower().Contains("shld"))
    //                {
    //                    result["LeftColor_" + i_str] = GetJBRowString(row, "LeftColor");
    //                    result["RightColor_" + i_str] = GetJBRowString(row, "RightColor");
    //                }
    //                i++;
    //            }
    //            return result;
    //        }
    //    }

    //    private static string GetJBRowString(IXLRow row, string col)
    //    {
    //        return row.Cell(ExcelColumnMaps.JB[col]).GetString();
    //    }
    //}
}
