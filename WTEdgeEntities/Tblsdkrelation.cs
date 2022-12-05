using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblsdkrelation
    {
        public int Id { get; set; }
        public string? Projectnumber { get; set; }
        public string? Parenttags { get; set; }
        public string? Inputtags { get; set; }
        public string? Sdgroup { get; set; }
        public string? Outputtag { get; set; }
        public string? Sdaction1 { get; set; }
        public string? Sdaction2 { get; set; }
        public string? Notes { get; set; }

        public virtual Tblindex? OutputtagNavigation { get; set; }
        public virtual Tblindex? ParenttagsNavigation { get; set; }
    }
}
