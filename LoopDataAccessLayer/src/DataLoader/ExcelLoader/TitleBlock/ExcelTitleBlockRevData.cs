using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer.src.DataLoader.ExcelLoader
{
    public class ExcelTitleBlockRevData<T> : IExcelTitleBlockRevData<T>
    {
#pragma warning disable CS8618
        public T Rev { get; set; }
        public T Description { get; set; }
        public T Date { get; set; }
        public T DrawnBy { get; set; }
        public T CheckedBy { get; set; }
        public T ApprovedBy { get; set; }
#pragma warning restore CS8618
    }
}
