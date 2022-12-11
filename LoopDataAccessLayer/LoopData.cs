using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class LoopData
    {
        public string DrawingType { get; }
        public string LoopName { get; }
        public Dictionary<string, string> Attributes { get; }

        public LoopData(Dictionary<string, string> attributes)
        {
            Attributes = attributes;
            _ =attributes.Remove("LoopName", out string? loopName);
            _ = attributes.Remove("DrawingType", out string? drawingType);

            DrawingType=drawingType ?? "";
            LoopName=loopName ?? "";
        }
    }
}
