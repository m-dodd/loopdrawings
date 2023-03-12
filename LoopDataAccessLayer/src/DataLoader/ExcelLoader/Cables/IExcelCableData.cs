using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelCableData<T>
    {
        T CableTag { get; set; }
        T From { get; set; }
        T To { get; set; }
        T Conductors { get; set; }
        T ConductorSize { get; set; }
        public string CableSizeType { get; }
    }
}
