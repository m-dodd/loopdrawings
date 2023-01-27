using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelLoader
    {
        public IXLRow? GetIORow(string tag);
        public IXLRows? GetJBRows(string tag);
    }
}
