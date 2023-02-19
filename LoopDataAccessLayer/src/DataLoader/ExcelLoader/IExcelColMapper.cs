using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelColMapper
    {
        public IExcelIOData<int> GetIOColMap();
        public IExcelJBRowData<int> GetJBColMap();
        public IExcelTitleBlockData<int> GetTitleBlockColMap();
    }
}
