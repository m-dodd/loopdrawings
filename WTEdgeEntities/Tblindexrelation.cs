using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblindexrelation
    {
        public int Id { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }

        public virtual Tblindex? DestinationNavigation { get; set; }
        public virtual Tblindex? SourceNavigation { get; set; }
    }
}
