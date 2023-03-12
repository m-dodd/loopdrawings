using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelColMaps
    {
        IExcelIOData<int> IOColMap { get; set; }
        IExcelJBRowData<int> JBColMap { get; set; }
        IExcelTitleBlockData<int> TitleBlockColMap { get; set; }
        IExcelCableData<int> CableColMap { get; set; }
    }
}
