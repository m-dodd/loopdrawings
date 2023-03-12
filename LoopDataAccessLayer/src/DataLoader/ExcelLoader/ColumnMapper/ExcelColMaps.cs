using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelColMaps : IExcelColMaps
    {
#pragma warning disable CS8618
        public IExcelIOData<int> IOColMap { get; set; }
        public IExcelJBRowData<int> JBColMap { get; set; }
        public IExcelTitleBlockData<int> TitleBlockColMap { get; set; }
        public IExcelCableData<int> CableColMap { get; set; }
#pragma warning restore CS8618
    }
}
