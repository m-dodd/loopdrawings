using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ProgressReportModel
    {
        public int PercentageComplete {
            get
            {
                return NumberOfLoops == 0 ? 0 : (LoopsComplete.Count * 100) / NumberOfLoops;
            }
        }
        public List<string> LoopsComplete { get; set; } = new List<string>();
        public int NumberOfLoops { get; set; } = 0;

        public bool ErrorsFound { get; set; } = false;
        public int NumberOfCurrentLoop
        {
            get
            {
                int n = LoopsComplete.Count + 1;
                return n <= NumberOfLoops ? n : NumberOfLoops;
            }
        }
    }
}
