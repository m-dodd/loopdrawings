using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblindex
    {
        public Tblindex()
        {
            InverseItpmparentNavigation = new HashSet<Tblindex>();
            InverseParenttag1Navigation = new HashSet<Tblindex>();
            InverseParenttag2Navigation = new HashSet<Tblindex>();
            InverseParenttag3Navigation = new HashSet<Tblindex>();
            TblindexrelationDestinationNavigations = new HashSet<Tblindexrelation>();
            TblindexrelationSourceNavigations = new HashSet<Tblindexrelation>();
            TblsdkrelationOutputtagNavigations = new HashSet<Tblsdkrelation>();
            TblsdkrelationParenttagsNavigations = new HashSet<Tblsdkrelation>();
        }

        public int Id { get; set; }
        public string? Project { get; set; }
        public string? Criticaldevice { get; set; }
        public string Tag { get; set; } = null!;
        public string? Oldtag { get; set; }
        public string? Loopno { get; set; }
        public string? Parenttag1 { get; set; }
        public string? Parenttag2 { get; set; }
        public string? Parenttag3 { get; set; }
        public string? Servicedescription { get; set; }
        public string? Controldescription { get; set; }
        public int? Controldesclen { get; set; }
        public string? Pid { get; set; }
        public string? Line { get; set; }
        public string? Plant { get; set; }
        public string? Area { get; set; }
        public string? Subarea { get; set; }
        public string? Location { get; set; }
        public string? Building { get; set; }
        public string? Equipment { get; set; }
        public string? Instrumenttype { get; set; }
        public string? Failposition { get; set; }
        public string? Template { get; set; }
        public string? Resetgroup { get; set; }
        public string? System { get; set; }
        public string? Controlsystem { get; set; }
        public string? Iopanel { get; set; }
        public string? Marshalingpanel { get; set; }
        public string? Controller { get; set; }
        public string? Iotype { get; set; }
        public int? Rack { get; set; }
        public int? Slot { get; set; }
        public int? Channel { get; set; }
        public int? Port { get; set; }
        public string? Jb1tag { get; set; }
        public string? Jb2tag { get; set; }
        public string? Fieldcable { get; set; }
        public string? Moduletype { get; set; }
        public string? Itpmparent { get; set; }
        public string? Itpmtemplate { get; set; }
        public string? Oldaddress { get; set; }
        public string? Oldcontrolsystem { get; set; }
        public string? Existingwiringdrawing { get; set; }
        public string? Signallevel { get; set; }
        public string? Powersupply { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Mrno { get; set; }
        public string? Ponumber { get; set; }
        public string? Installdetail { get; set; }
        public string? Suppliedby { get; set; }
        public string? Installedby { get; set; }
        public string? Newwiringdrawing { get; set; }
        public string? Status { get; set; }
        public string? Comments { get; set; }
        public string? Internalcomments { get; set; }
        public string? Rev { get; set; }
        public string? Commissioningphase { get; set; }
        public string? Sdkinputgroup { get; set; }
        public string? Sdkoutputgroup { get; set; }
        public string? Wiringtypical { get; set; }
        public string? Ioterminalstrip { get; set; }
        public string? Ioterminals { get; set; }

        public virtual Tblindex? ItpmparentNavigation { get; set; }
        public virtual Tblloopno? LoopnoNavigation { get; set; }
        public virtual Tblindex? Parenttag1Navigation { get; set; }
        public virtual Tblindex? Parenttag2Navigation { get; set; }
        public virtual Tblindex? Parenttag3Navigation { get; set; }
        public virtual Tblsystem? SystemNavigation { get; set; }
        public virtual Tblarss? Tblarss { get; set; }
        public virtual ICollection<Tblindex> InverseItpmparentNavigation { get; set; }
        public virtual ICollection<Tblindex> InverseParenttag1Navigation { get; set; }
        public virtual ICollection<Tblindex> InverseParenttag2Navigation { get; set; }
        public virtual ICollection<Tblindex> InverseParenttag3Navigation { get; set; }
        public virtual ICollection<Tblindexrelation> TblindexrelationDestinationNavigations { get; set; }
        public virtual ICollection<Tblindexrelation> TblindexrelationSourceNavigations { get; set; }
        public virtual ICollection<Tblsdkrelation> TblsdkrelationOutputtagNavigations { get; set; }
        public virtual ICollection<Tblsdkrelation> TblsdkrelationParenttagsNavigations { get; set; }
    }
}
