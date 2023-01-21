using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblloop
    {
        public int Id { get; set; }
        public string? Loop { get; set; }
        public string? Looptemplate { get; set; }

        public virtual Tblloopno? LoopNavigation { get; set; }
    }
}
