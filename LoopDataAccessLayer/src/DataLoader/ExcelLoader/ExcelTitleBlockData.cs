using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer.src.DataLoader.ExcelLoader
{
    public class ExcelTitleBlockData<T> : IExcelTitleBlockData<T>
    {
#pragma warning disable CS8618
        public T SiteNumber { get; set; }
        public T Sheet { get; set; }
        public T MaxSheets { get; set; }
        public T Project { get; set; }
        public T Scale { get; set; }
        public IExcelTitleBlockRevData<T> GeneralRevData { get; set; }
        public IExcelTitleBlockRevData<T> RevBlockRevData { get; set; }
#pragma warning restore CS8618
    }
}
