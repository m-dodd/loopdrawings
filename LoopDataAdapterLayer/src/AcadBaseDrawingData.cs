using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAdapterLayer
{
    public class AcadBaseDrawingData<T> where T: class
    {
        public string LoopID { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty;
        public string TemplateDrawingFileName { get; set; } = string.Empty;
        public string OutputDrawingFileName { get; set; } = string.Empty;

        public List<T> Blocks { get; set; } = new List<T>();
    }
}
