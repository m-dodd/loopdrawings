using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelTitleBlockData<T>
    {
        T SiteNumber { get; set; }
        T Sheet { get; set; }
        T MaxSheets { get; set; }
        T Project { get; set; }
        T Scale { get; set; }
        T CityTown { get; set; }
        T ProvinceState { get; set; }
        IExcelTitleBlockRevData<T> GeneralRevData { get; set; }
        IExcelTitleBlockRevData<T> RevBlockRevData { get; set; }
    }

    public interface IExcelTitleBlockRevData<T>
    {
        T Rev { get; set; }
        T Description { get; set; }
        T Date { get; set; }
        T DrawnBy { get; set; }
        T CheckedBy { get; set; }
        T ApprovedBy { get; set; }
    }
}
