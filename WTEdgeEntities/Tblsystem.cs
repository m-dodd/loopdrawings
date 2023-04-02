using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblsystem
    {
        public Tblsystem()
        {
            Tblindices = new HashSet<Tblindex>();
        }

        public int Id { get; set; }
        public string System { get; set; } = null!;
        public string SystemType { get; set; } = null!;

        public virtual ICollection<Tblindex> Tblindices { get; set; }
        public virtual Tblindex? SystemNavigation { get; set; }
    }
}
