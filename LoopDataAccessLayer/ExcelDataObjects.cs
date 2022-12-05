using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    internal static class ExcelColumnMaps
    {
        internal static readonly Dictionary<string, int> IO = new Dictionary<string, int>()
        {
            {"ModuleTerminal1", 4},
            {"ModuleTerminal2", 5},
            {"Cabinet", 6},
            {"Breaker", 7},
            {"Tag", 8},
            {"IOStrip", 9},
            {"IOTerminalPlus", 10},
            {"IOTerminalNeg", 11},
            {"IOTerminalShield", 12},
            {"IOWireTagPlus", 13},
            {"IOWireTagNeg", 14},
            {"JB1", 15},
            {"JB2", 16},
            {"JB3", 17},
            {"CableTag", 18},
            {"TerminalPlus", 19},
            {"TerminalNeg", 20},
            {"WireTagPlus", 21},
            {"WireTagNeg", 22},
            {"WireColorPlus", 23},
            {"WireColorNeg", 24},
            {"CorePairPlus", 25},
            {"CorePairNeg", 26},
        };
        internal static readonly Dictionary<string, int> JB = new Dictionary<string, int>()
        {
            {"JBTag", 1},
            {"TerminalStrip", 2 },
            {"Terminal", 3},
            {"SignalType", 4},
            {"Tag", 5},
            {"LeftCable", 6},
            {"LeftCore", 7},
            {"LeftColor", 8},
            {"LeftWireTag", 9},
            {"RightCable", 10},
            {"RightCore", 11},
            {"RightColor", 12},
            {"RightTag", 13},
        };
    }


    internal class ExcelJBTagResult
    {
        private readonly IXLRows? rows;
        public ExcelJBTagResult(IXLRows? rows)
        {
            this.rows = rows;
        }

        public Dictionary<string, string> ToDict()
        {
            if (rows is null) { return new Dictionary<string, string>(); }
            else
            {

                int i = 1;
                Dictionary<string, string> result = new()
                {
                    ["JB_TS_01"] = GetJBRowString(rows.First(), "TerminalStrip")
                };
                foreach (var row in rows.OrderBy(r => GetJBRowString(r, "Tag")))
                {
                    string i_str = i.ToString("00");
                    result["JB_TB_" + i_str] = GetJBRowString(row, "Terminal");
                    if (!GetJBRowString(row, "SignalType").ToLower().Contains("shld"))
                    {
                        result["LeftColor_" + i_str] = GetJBRowString(row, "LeftColor");
                        result["RightColor_" + i_str] = GetJBRowString(row, "RightColor");
                    }
                    i++;
                }
                return result;
            }
        }

        private static string GetJBRowString(IXLRow row, string col)
        {
            return row.Cell(ExcelColumnMaps.JB[col]).GetString();
        }
    }
}
