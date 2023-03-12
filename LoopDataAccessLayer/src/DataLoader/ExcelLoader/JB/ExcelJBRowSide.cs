using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelJBRowSide<T> : IExcelJBRowSide<T>
    {
#pragma warning disable CS8618
        public T Cable { get; set; }
        public T Core { get; set; }
        public T Color { get; set; }
        public T WireTag { get; set; }
#pragma warning restore CS8618
    }
}
