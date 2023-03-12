using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelCableData<T> : IExcelCableData<T>
    {
#pragma warning disable CS8618
        public T CableTag { get; set; }
        public T From { get; set; }
        public T To { get; set; }
        public T Conductors { get; set; }
        public T ConductorSize { get; set; }
#pragma warning restore CS8618
        public string CableSizeType
        {
            get
            {
                if (Conductors is not null && ConductorSize is not null)
                {
                    return Conductors.ToString() + " " + ConductorSize.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
