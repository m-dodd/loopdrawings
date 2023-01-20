using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class DataLoader
    {
        public DBDataLoader? DBLoader { get; set; }
        public ExcelDataLoader? ExcelLoader { get; set; }

        public DataLoader(ExcelDataLoader excelLoader)
        {
            DBLoader = new();
            ExcelLoader = excelLoader;
        }

        public DataLoader(ExcelDataLoader excelLoader, DBDataLoader dbLoader)
        {
            DBLoader = dbLoader;
            ExcelLoader= excelLoader;
        }

        public DataLoader(string excelFileName)
        {
            if (ExcelDataLoader.IsExcelFile(excelFileName))
            {
                ExcelLoader = new(excelFileName);
            }
            else
            {
                ExcelLoader = null;
                DBLoader = null;
                return;
            }
            DBLoader = new();
        }
    }
}
