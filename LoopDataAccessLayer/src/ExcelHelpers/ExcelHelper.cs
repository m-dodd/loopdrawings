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
        public static string GetRowString(IXLRow row, int col)
        {
            return row.Cell(col)?.GetString() ?? string.Empty;
        }

        public static bool IsExcelFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string extension = Path.GetExtension(fileName);
                string[] validExtensions = { ".xlsx", ".xlsm" };
                foreach (string ext in validExtensions)
                {
                    if (extension.ToLower() == ext) return true;
                }
            }

            return false;
        }

        
    }
}