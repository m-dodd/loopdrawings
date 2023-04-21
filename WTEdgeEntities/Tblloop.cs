using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblloop
    {
        public Tblloop()
        {
            Tblindices = new HashSet<Tblindex>();
        }

        public int Id { get; set; }
        public string Loopno { get; set; } = null!;
        public string? Looptemplate { get; set; }
        public string? Loopdescription { get; set; }

        public virtual ICollection<Tblindex> Tblindices { get; set; }
    }
}
