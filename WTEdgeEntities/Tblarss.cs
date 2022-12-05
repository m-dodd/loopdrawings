using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblarss
    {
        public int Id { get; set; }
        public string? Arssrev { get; set; }
        public string? Arssprojectnumber { get; set; }
        public string? Tags { get; set; }
        public decimal? Mincalibrange { get; set; }
        public decimal? Maxcalibrange { get; set; }
        public decimal? Midcalibrange { get; set; }
        public string? Calibrangeunits { get; set; }
        public string? Llalarm { get; set; }
        public string? Llsddelay { get; set; }
        public string? Llstdelay { get; set; }
        public string? Llalmpriority { get; set; }
        public string? Loalarm { get; set; }
        public string? Loalmdelay { get; set; }
        public string? Lostdelay { get; set; }
        public string? Loalmpriority { get; set; }
        public string? Lowctrl { get; set; }
        public string? Highctrl { get; set; }
        public string? Hialarm { get; set; }
        public string? Hialmdelay { get; set; }
        public string? Histdelay { get; set; }
        public string? Hialmpriority { get; set; }
        public string? Hhalarm { get; set; }
        public string? Hhsddelay { get; set; }
        public string? Hhstdelay { get; set; }
        public string? Hhalmpriority { get; set; }
        public string? Deadband { get; set; }
        public string? Normalcontrolsp { get; set; }
        public string? Roc { get; set; }
        public string? Deviation { get; set; }
        public string? Arsscomments { get; set; }
        public decimal? Latch { get; set; }
        public decimal? Variance { get; set; }

        public virtual Tblindex? TagsNavigation { get; set; }
    }
}
