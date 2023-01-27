using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    public static class ExcelHelper
    {
        internal static string GetRowString(IXLRow row, ExcelIOColumns col)
        {
            return GetRowStringHelper(row, (int)col);
        }

        internal static string GetRowString(IXLRow row, ExcelJBColumns col)
        {
            return GetRowStringHelper(row, (int)col);
        }

        private static string GetRowStringHelper(IXLRow row, int col)
        {
            return row.Cell(col)?.GetString() ?? string.Empty;
        }

        public static bool IsExcelFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string[] validExtensions = { ".xlsx", ".xlsm" };
            foreach (string ext in validExtensions)
            {
                if (extension.ToLower() == ext) return true;
            }
            return false;
        }
    }
}