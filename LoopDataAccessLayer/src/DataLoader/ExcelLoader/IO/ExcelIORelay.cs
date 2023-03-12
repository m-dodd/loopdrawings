using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ExcelIORelay<T> : IExcelIORelay<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public T Tag { get; set; }
        public T PanelTerminalStrip { get; set; }
        public T Term1 { get; set; }
        public T Term2 { get; set; }
        public T ContactTag { get; set; }
        public T ContactTerm1 { get; set; }
        public T ContactTerm2 { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
