using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    public partial class WTEdgeContext : DbContext
    {
        public WTEdgeContext()
        {
        }

        public WTEdgeContext(DbContextOptions<WTEdgeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tblarss> Tblarsses { get; set; } = null!;
        public virtual DbSet<Tblbominstr> Tblbominstrs { get; set; } = null!;
        public virtual DbSet<Tblindex> Tblindices { get; set; } = null!;
        public virtual DbSet<Tblindexrelation> Tblindexrelations { get; set; } = null!;
        public virtual DbSet<Tblloop> Tblloops { get; set; } = null!;
        public virtual DbSet<Tbllooptemplate> Tbllooptemplates { get; set; } = null!;
        public virtual DbSet<Tblsdkrelation> Tblsdkrelations { get; set; } = null!;
        public virtual DbSet<Tblsystem> Tblsystems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=duco2.wtedgeplatform.com;user=mdodd;password=wtedgepassword;database=nutrien_leal", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.25-mariadb"), options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Tblarss>(entity =>
            {
                entity.ToTable("tblarss");

                entity.HasIndex(e => e.Tags, "fkarsstags")
                    .IsUnique();

                entity.HasIndex(e => e.Calibrangeunits, "fkengunits");

                entity.HasIndex(e => e.Hhalmpriority, "fkhhalmpriority");

                entity.HasIndex(e => e.Hhsddelay, "fkhhsddelay");

                entity.HasIndex(e => e.Hhstdelay, "fkhhstdelay");

                entity.HasIndex(e => e.Hialmdelay, "fkhialmdelay");

                entity.HasIndex(e => e.Hialmpriority, "fkhialmpriority");

                entity.HasIndex(e => e.Llalmpriority, "fkllalmpriority");

                entity.HasIndex(e => e.Llsddelay, "fkllsddelay");

                entity.HasIndex(e => e.Llstdelay, "fkllstdelay");

                entity.HasIndex(e => e.Loalmdelay, "fkloalmdelay");

                entity.HasIndex(e => e.Loalmpriority, "fkloalmpriority");

                entity.HasIndex(e => e.Arssprojectnumber, "fkproject");

                entity.HasIndex(e => e.Arssrev, "fkrev");

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.Arsscomments)
                    .HasColumnType("mediumtext")
                    .HasColumnName("arsscomments");

                entity.Property(e => e.Arssprojectnumber)
                    .HasMaxLength(100)
                    .HasColumnName("arssprojectnumber");

                entity.Property(e => e.Arssrev)
                    .HasMaxLength(25)
                    .HasColumnName("arssrev");

                entity.Property(e => e.Calibrangeunits)
                    .HasMaxLength(50)
                    .HasColumnName("calibrangeunits");

                entity.Property(e => e.Deadband)
                    .HasMaxLength(50)
                    .HasColumnName("deadband");

                entity.Property(e => e.Deviation)
                    .HasMaxLength(50)
                    .HasColumnName("deviation");

                entity.Property(e => e.Hhalarm)
                    .HasMaxLength(50)
                    .HasColumnName("hhalarm");

                entity.Property(e => e.Hhalmpriority)
                    .HasMaxLength(50)
                    .HasColumnName("hhalmpriority");

                entity.Property(e => e.Hhsddelay)
                    .HasMaxLength(50)
                    .HasColumnName("hhsddelay");

                entity.Property(e => e.Hhstdelay)
                    .HasMaxLength(50)
                    .HasColumnName("hhstdelay");

                entity.Property(e => e.Hialarm)
                    .HasMaxLength(50)
                    .HasColumnName("hialarm");

                entity.Property(e => e.Hialmdelay)
                    .HasMaxLength(50)
                    .HasColumnName("hialmdelay");

                entity.Property(e => e.Hialmpriority)
                    .HasMaxLength(50)
                    .HasColumnName("hialmpriority");

                entity.Property(e => e.Highctrl)
                    .HasMaxLength(50)
                    .HasColumnName("highctrl");

                entity.Property(e => e.Histdelay)
                    .HasMaxLength(50)
                    .HasColumnName("histdelay");

                entity.Property(e => e.Latch)
                    .HasPrecision(10, 2)
                    .HasColumnName("latch");

                entity.Property(e => e.Llalarm)
                    .HasMaxLength(50)
                    .HasColumnName("llalarm");

                entity.Property(e => e.Llalmpriority)
                    .HasMaxLength(50)
                    .HasColumnName("llalmpriority");

                entity.Property(e => e.Llsddelay)
                    .HasMaxLength(50)
                    .HasColumnName("llsddelay");

                entity.Property(e => e.Llstdelay)
                    .HasMaxLength(50)
                    .HasColumnName("llstdelay");

                entity.Property(e => e.Loalarm)
                    .HasMaxLength(50)
                    .HasColumnName("loalarm");

                entity.Property(e => e.Loalmdelay)
                    .HasMaxLength(50)
                    .HasColumnName("loalmdelay");

                entity.Property(e => e.Loalmpriority)
                    .HasMaxLength(50)
                    .HasColumnName("loalmpriority");

                entity.Property(e => e.Lostdelay)
                    .HasMaxLength(50)
                    .HasColumnName("lostdelay");

                entity.Property(e => e.Lowctrl)
                    .HasMaxLength(50)
                    .HasColumnName("lowctrl");

                entity.Property(e => e.Maxcalibrange)
                    .HasPrecision(12, 1)
                    .HasColumnName("maxcalibrange");

                entity.Property(e => e.Midcalibrange)
                    .HasPrecision(12, 1)
                    .HasColumnName("midcalibrange");

                entity.Property(e => e.Mincalibrange)
                    .HasPrecision(12, 1)
                    .HasColumnName("mincalibrange");

                entity.Property(e => e.Normalcontrolsp)
                    .HasMaxLength(50)
                    .HasColumnName("normalcontrolsp");

                entity.Property(e => e.Roc)
                    .HasMaxLength(50)
                    .HasColumnName("roc");

                entity.Property(e => e.Tags)
                    .HasMaxLength(50)
                    .HasColumnName("tags");

                entity.Property(e => e.Variance)
                    .HasPrecision(10, 2)
                    .HasColumnName("variance");

                entity.HasOne(d => d.TagsNavigation)
                    .WithOne(p => p.Tblarss)
                    .HasPrincipalKey<Tblindex>(p => p.Tag)
                    .HasForeignKey<Tblarss>(d => d.Tags)
                    .HasConstraintName("tblarss_ibfk_1");
            });

            modelBuilder.Entity<Tblbominstr>(entity =>
            {
                entity.ToTable("tblbominstr");

                entity.HasIndex(e => e.Supplierquotelineno, "FKsupplierquotelineno");

                entity.HasIndex(e => e.Location, "fkbomlocation");

                entity.HasIndex(e => e.Mrqissued, "fkbommrqissued");

                entity.HasIndex(e => e.Project, "fkbomproject");

                entity.HasIndex(e => e.Purchaser, "fkbompurchasere");

                entity.HasIndex(e => e.Quoteby, "fkbomquoteby");

                entity.HasIndex(e => e.Status, "fkbomstatus");

                entity.HasIndex(e => e.Ducolocation, "fkducolocation");

                entity.HasIndex(e => e.Ducopackinglist, "fkducopackinglist");

                entity.HasIndex(e => e.Tag, "fkequiptag")
                    .IsUnique();

                entity.HasIndex(e => e.Inspectionstatus, "fkinspectionstatus");

                entity.HasIndex(e => e.Model, "fkmodel");

                entity.HasIndex(e => e.Mrno, "fkmrno");

                entity.HasIndex(e => e.Mrpissued, "fkmrpissued");

                entity.HasIndex(e => e.Mrrev, "fkmrrev");

                entity.HasIndex(e => e.Pono, "fkpono");

                entity.HasIndex(e => e.Quotereceived, "fkquotereceived");

                entity.HasIndex(e => e.Sitelabel, "fksitelabel1");

                entity.HasIndex(e => e.Supplier, "fksupplier");

                entity.HasIndex(e => e.Suppliershiplocation, "fksuppliershiplocation");

                entity.HasIndex(e => e.Supplierorder, "fktblbomsupplierorder");

                entity.HasIndex(e => e.Mrlineno, "fktpklineno");

                entity.HasIndex(e => e.Polineno, "fktpkpolineno");

                entity.HasIndex(e => e.Vendordocstatus, "fkvendordocstatus");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.Manufacturer, "tblbominstr_manufacturer");

                entity.HasIndex(e => e.Currency, "tblbominstrfkcurrency");

                entity.HasIndex(e => e.Supplierquote, "tblbomsupplierquote");

                entity.Property(e => e.Id)
                    .HasColumnType("int(50)")
                    .HasColumnName("id");

                entity.Property(e => e.Actualdelivery).HasColumnName("actualdelivery");

                entity.Property(e => e.Comments)
                    .HasColumnType("mediumtext")
                    .HasColumnName("comments");

                entity.Property(e => e.Currency)
                    .HasMaxLength(25)
                    .HasColumnName("currency");

                entity.Property(e => e.Description)
                    .HasColumnType("mediumtext")
                    .HasColumnName("description");

                entity.Property(e => e.Ducolocation)
                    .HasMaxLength(100)
                    .HasColumnName("ducolocation");

                entity.Property(e => e.Ducopackinglist)
                    .HasMaxLength(100)
                    .HasColumnName("ducopackinglist");

                entity.Property(e => e.Expecteddelivery).HasColumnName("expecteddelivery");

                entity.Property(e => e.Inspectionstatus)
                    .HasMaxLength(100)
                    .HasColumnName("inspectionstatus");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .HasColumnName("location");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .HasColumnName("manufacturer");

                entity.Property(e => e.Model).HasColumnName("model");

                entity.Property(e => e.Mrlineno)
                    .HasColumnType("int(25)")
                    .HasColumnName("mrlineno");

                entity.Property(e => e.Mrno)
                    .HasMaxLength(100)
                    .HasColumnName("mrno");

                entity.Property(e => e.Mrpissued).HasColumnName("mrpissued");

                entity.Property(e => e.Mrqissued).HasColumnName("mrqissued");

                entity.Property(e => e.Mrrev)
                    .HasMaxLength(25)
                    .HasColumnName("mrrev");

                entity.Property(e => e.Orderdate).HasColumnName("orderdate");

                entity.Property(e => e.Polineno)
                    .HasColumnType("int(25)")
                    .HasColumnName("polineno");

                entity.Property(e => e.Pono)
                    .HasMaxLength(100)
                    .HasColumnName("pono");

                entity.Property(e => e.Productid)
                    .HasMaxLength(100)
                    .HasColumnName("productid");

                entity.Property(e => e.Project)
                    .HasMaxLength(100)
                    .HasColumnName("project");

                entity.Property(e => e.Purchaser)
                    .HasMaxLength(100)
                    .HasColumnName("purchaser");

                entity.Property(e => e.Qtyordered)
                    .HasColumnType("int(10)")
                    .HasColumnName("qtyordered")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Qtyreceived)
                    .HasColumnType("int(10)")
                    .HasColumnName("qtyreceived")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Qtyremaining)
                    .HasColumnType("int(10)")
                    .HasColumnName("qtyremaining");

                entity.Property(e => e.Quoteby)
                    .HasMaxLength(100)
                    .HasColumnName("quoteby");

                entity.Property(e => e.Quotereceived).HasColumnName("quotereceived");

                entity.Property(e => e.Ros).HasColumnName("ros");

                entity.Property(e => e.Sitelabel)
                    .HasMaxLength(50)
                    .HasColumnName("sitelabel");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Supplier)
                    .HasMaxLength(100)
                    .HasColumnName("supplier");

                entity.Property(e => e.Supplierorder)
                    .HasMaxLength(100)
                    .HasColumnName("supplierorder");

                entity.Property(e => e.Supplierquote)
                    .HasMaxLength(100)
                    .HasColumnName("supplierquote");

                entity.Property(e => e.Supplierquotelineno)
                    .HasColumnType("int(25)")
                    .HasColumnName("supplierquotelineno");

                entity.Property(e => e.Suppliershiplocation)
                    .HasMaxLength(100)
                    .HasColumnName("suppliershiplocation");

                entity.Property(e => e.Tag)
                    .HasMaxLength(100)
                    .HasColumnName("tag");

                entity.Property(e => e.Totalpricecad)
                    .HasPrecision(10, 2)
                    .HasColumnName("totalpricecad");

                entity.Property(e => e.Totalpriceusd)
                    .HasPrecision(10, 2)
                    .HasColumnName("totalpriceusd");

                entity.Property(e => e.Unitprice)
                    .HasPrecision(10, 2)
                    .HasColumnName("unitprice");

                entity.Property(e => e.Vendordocstatus)
                    .HasMaxLength(100)
                    .HasColumnName("vendordocstatus");

                entity.HasOne(d => d.TagNavigation)
                    .WithOne(p => p.Tblbominstr)
                    .HasPrincipalKey<Tblindex>(p => p.Tag)
                    .HasForeignKey<Tblbominstr>(d => d.Tag)
                    .HasConstraintName("fkbominstrtag");
            });

            modelBuilder.Entity<Tblindex>(entity =>
            {
                entity.ToTable("tblindex");

                entity.HasIndex(e => e.Area, "fkareanew");

                entity.HasIndex(e => e.Building, "fkbuilding");

                entity.HasIndex(e => e.Commissioningphase, "fkcommissioningphase");

                entity.HasIndex(e => e.Controlsystem, "fkcontrolsystem");

                entity.HasIndex(e => e.Criticaldevice, "fkcriticaldevice");

                entity.HasIndex(e => e.Equipment, "fkequipmentnew");

                entity.HasIndex(e => e.Existingwiringdrawing, "fkexistingwiringdrawing");

                entity.HasIndex(e => e.Failposition, "fkfailposition");

                entity.HasIndex(e => e.Fieldcable, "fkfieldcable");

                entity.HasIndex(e => e.Installdetail, "fkinstalldetail");

                entity.HasIndex(e => e.Installedby, "fkinstalledby");

                entity.HasIndex(e => e.Instrumenttype, "fkinstrtype");

                entity.HasIndex(e => e.Iopanel, "fkiopanel");

                entity.HasIndex(e => e.Ioterminalstrip, "fkiotermstrip");

                entity.HasIndex(e => e.Iotype, "fkiotypenew");

                entity.HasIndex(e => e.Itpmparent, "fkitpmparent");

                entity.HasIndex(e => e.Jb1tag, "fkjb1tag");

                entity.HasIndex(e => e.Jb2tag, "fkjb2tag");

                entity.HasIndex(e => e.Line, "fklinenew");

                entity.HasIndex(e => e.Location, "fklocationnew");

                entity.HasIndex(e => e.Itpmtemplate, "fkloopchecktemplate");

                entity.HasIndex(e => e.Manufacturer, "fkmanufacturer");

                entity.HasIndex(e => e.Marshalingpanel, "fkmarshalingpanel");

                entity.HasIndex(e => e.Model, "fkmodel");

                entity.HasIndex(e => e.Moduletype, "fkmoduletype");

                entity.HasIndex(e => e.Mrno, "fkmrno");

                entity.HasIndex(e => e.Newwiringdrawing, "fknewwiringdrawing");

                entity.HasIndex(e => e.Oldcontrolsystem, "fkoldcontrolsystem");

                entity.HasIndex(e => e.Pid, "fkpidnew");

                entity.HasIndex(e => e.Plant, "fkplantnew");

                entity.HasIndex(e => e.Ponumber, "fkponumber");

                entity.HasIndex(e => e.Project, "fkprojectnew");

                entity.HasIndex(e => e.Parenttag1, "fkpt1");

                entity.HasIndex(e => e.Parenttag2, "fkpt2");

                entity.HasIndex(e => e.Parenttag3, "fkpt3");

                entity.HasIndex(e => e.Rack, "fkrackslotchannel1");

                entity.HasIndex(e => e.Slot, "fkrackslotchannel2");

                entity.HasIndex(e => e.Channel, "fkrackslotchannel3");

                entity.HasIndex(e => e.Resetgroup, "fkresetgroupnew");

                entity.HasIndex(e => e.Rev, "fkrev");

                entity.HasIndex(e => e.Sdkinputgroup, "fksdkingroup");

                entity.HasIndex(e => e.Sdkoutputgroup, "fksdkoutgroup");

                entity.HasIndex(e => e.Signallevel, "fksignallevel");

                entity.HasIndex(e => e.Status, "fkstatus");

                entity.HasIndex(e => e.Subarea, "fksubarea");

                entity.HasIndex(e => e.Suppliedby, "fksuppliedby");

                entity.HasIndex(e => e.System, "fksystem");

                entity.HasIndex(e => e.Template, "fktemplate");

                entity.HasIndex(e => e.Wiringtypical, "fkwiringtypical");

                entity.HasIndex(e => e.Powersupply, "flpowersupply");

                entity.HasIndex(e => e.Loopno, "loopno");

                entity.HasIndex(e => e.Tag, "tag")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasColumnName("area");

                entity.Property(e => e.Building)
                    .HasMaxLength(100)
                    .HasColumnName("building");

                entity.Property(e => e.Channel)
                    .HasColumnType("int(25)")
                    .HasColumnName("channel");

                entity.Property(e => e.Comments)
                    .HasColumnType("mediumtext")
                    .HasColumnName("comments");

                entity.Property(e => e.Commissioningphase)
                    .HasMaxLength(100)
                    .HasColumnName("commissioningphase");

                entity.Property(e => e.Controldesclen)
                    .HasColumnType("int(6)")
                    .HasColumnName("controldesclen")
                    .HasComputedColumnSql("char_length(`controldescription`)", false);

                entity.Property(e => e.Controldescription)
                    .HasMaxLength(255)
                    .HasColumnName("controldescription");

                entity.Property(e => e.Controller)
                    .HasMaxLength(50)
                    .HasColumnName("controller");

                entity.Property(e => e.Controlsystem)
                    .HasMaxLength(50)
                    .HasColumnName("controlsystem");

                entity.Property(e => e.Criticaldevice)
                    .HasMaxLength(100)
                    .HasColumnName("criticaldevice");

                entity.Property(e => e.Equipment)
                    .HasMaxLength(100)
                    .HasColumnName("equipment");

                entity.Property(e => e.Existingwiringdrawing)
                    .HasMaxLength(100)
                    .HasColumnName("existingwiringdrawing");

                entity.Property(e => e.Failposition)
                    .HasMaxLength(50)
                    .HasColumnName("failposition");

                entity.Property(e => e.Fieldcable)
                    .HasMaxLength(50)
                    .HasColumnName("fieldcable");

                entity.Property(e => e.Installdetail)
                    .HasMaxLength(100)
                    .HasColumnName("installdetail");

                entity.Property(e => e.Installedby)
                    .HasMaxLength(100)
                    .HasColumnName("installedby");

                entity.Property(e => e.Instrumenttype)
                    .HasMaxLength(50)
                    .HasColumnName("instrumenttype");

                entity.Property(e => e.Internalcomments)
                    .HasColumnType("mediumtext")
                    .HasColumnName("internalcomments");

                entity.Property(e => e.Iopanel)
                    .HasMaxLength(100)
                    .HasColumnName("iopanel");

                entity.Property(e => e.Ioterminals)
                    .HasMaxLength(50)
                    .HasColumnName("ioterminals");

                entity.Property(e => e.Ioterminalstrip)
                    .HasMaxLength(50)
                    .HasColumnName("ioterminalstrip");

                entity.Property(e => e.Iotype)
                    .HasMaxLength(50)
                    .HasColumnName("iotype");

                entity.Property(e => e.Itpmparent)
                    .HasMaxLength(50)
                    .HasColumnName("itpmparent");

                entity.Property(e => e.Itpmtemplate)
                    .HasMaxLength(50)
                    .HasColumnName("itpmtemplate");

                entity.Property(e => e.Jb1tag)
                    .HasMaxLength(100)
                    .HasColumnName("jb1tag");

                entity.Property(e => e.Jb2tag)
                    .HasMaxLength(100)
                    .HasColumnName("jb2tag");

                entity.Property(e => e.Line)
                    .HasMaxLength(100)
                    .HasColumnName("line");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .HasColumnName("location");

                entity.Property(e => e.Loopno)
                    .HasMaxLength(100)
                    .HasColumnName("loopno");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .HasColumnName("manufacturer");

                entity.Property(e => e.Marshalingpanel)
                    .HasMaxLength(100)
                    .HasColumnName("marshalingpanel");

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .HasColumnName("model");

                entity.Property(e => e.Moduletype)
                    .HasMaxLength(50)
                    .HasColumnName("moduletype");

                entity.Property(e => e.Mrno)
                    .HasMaxLength(100)
                    .HasColumnName("mrno");

                entity.Property(e => e.Newwiringdrawing)
                    .HasMaxLength(100)
                    .HasColumnName("newwiringdrawing");

                entity.Property(e => e.Oldaddress)
                    .HasMaxLength(50)
                    .HasColumnName("oldaddress");

                entity.Property(e => e.Oldcontrolsystem)
                    .HasMaxLength(50)
                    .HasColumnName("oldcontrolsystem");

                entity.Property(e => e.Oldtag)
                    .HasMaxLength(50)
                    .HasColumnName("oldtag");

                entity.Property(e => e.Parenttag1)
                    .HasMaxLength(50)
                    .HasColumnName("parenttag1");

                entity.Property(e => e.Parenttag2)
                    .HasMaxLength(50)
                    .HasColumnName("parenttag2");

                entity.Property(e => e.Parenttag3)
                    .HasMaxLength(50)
                    .HasColumnName("parenttag3");

                entity.Property(e => e.Pid)
                    .HasMaxLength(50)
                    .HasColumnName("pid");

                entity.Property(e => e.Plant)
                    .HasMaxLength(50)
                    .HasColumnName("plant");

                entity.Property(e => e.Ponumber)
                    .HasMaxLength(100)
                    .HasColumnName("ponumber");

                entity.Property(e => e.Port)
                    .HasColumnType("int(25)")
                    .HasColumnName("port");

                entity.Property(e => e.Powersupply)
                    .HasMaxLength(50)
                    .HasColumnName("powersupply");

                entity.Property(e => e.Project)
                    .HasMaxLength(100)
                    .HasColumnName("project");

                entity.Property(e => e.Rack)
                    .HasColumnType("int(25)")
                    .HasColumnName("rack");

                entity.Property(e => e.Resetgroup)
                    .HasMaxLength(100)
                    .HasColumnName("resetgroup");

                entity.Property(e => e.Rev)
                    .HasMaxLength(50)
                    .HasColumnName("rev");

                entity.Property(e => e.Sdkinputgroup)
                    .HasMaxLength(100)
                    .HasColumnName("sdkinputgroup");

                entity.Property(e => e.Sdkoutputgroup)
                    .HasMaxLength(100)
                    .HasColumnName("sdkoutputgroup");

                entity.Property(e => e.Servicedescription)
                    .HasMaxLength(255)
                    .HasColumnName("servicedescription");

                entity.Property(e => e.Signallevel)
                    .HasMaxLength(50)
                    .HasColumnName("signallevel");

                entity.Property(e => e.Slot)
                    .HasColumnType("int(25)")
                    .HasColumnName("slot");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.Subarea)
                    .HasMaxLength(50)
                    .HasColumnName("subarea");

                entity.Property(e => e.Suppliedby)
                    .HasMaxLength(100)
                    .HasColumnName("suppliedby");

                entity.Property(e => e.System)
                    .HasMaxLength(50)
                    .HasColumnName("system");

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .HasColumnName("tag");

                entity.Property(e => e.Template)
                    .HasMaxLength(50)
                    .HasColumnName("template");

                entity.Property(e => e.Wiringtypical)
                    .HasMaxLength(50)
                    .HasColumnName("wiringtypical");

                entity.HasOne(d => d.ItpmparentNavigation)
                    .WithMany(p => p.InverseItpmparentNavigation)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Itpmparent)
                    .HasConstraintName("fkitpmparent");

                entity.HasOne(d => d.LoopnoNavigation)
                    .WithMany(p => p.Tblindices)
                    .HasPrincipalKey(p => p.Loopno)
                    .HasForeignKey(d => d.Loopno)
                    .HasConstraintName("fkloopno");

                entity.HasOne(d => d.Parenttag1Navigation)
                    .WithMany(p => p.InverseParenttag1Navigation)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Parenttag1)
                    .HasConstraintName("fkpt1");

                entity.HasOne(d => d.Parenttag2Navigation)
                    .WithMany(p => p.InverseParenttag2Navigation)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Parenttag2)
                    .HasConstraintName("fkpt2");

                entity.HasOne(d => d.Parenttag3Navigation)
                    .WithMany(p => p.InverseParenttag3Navigation)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Parenttag3)
                    .HasConstraintName("fkpt3");

                entity.HasOne(d => d.SystemNavigation)
                    .WithMany(p => p.Tblindices)
                    .HasPrincipalKey(p => p.System)
                    .HasForeignKey(d => d.System)
                    .HasConstraintName("tblindex_ibfk_25");
            });

            modelBuilder.Entity<Tblindexrelation>(entity =>
            {
                entity.ToTable("tblindexrelation");

                entity.HasIndex(e => e.Destination, "fkdestination");

                entity.HasIndex(e => e.Source, "fksource");

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.Destination)
                    .HasMaxLength(50)
                    .HasColumnName("destination");

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .HasColumnName("source");

                entity.HasOne(d => d.DestinationNavigation)
                    .WithMany(p => p.TblindexrelationDestinationNavigations)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Destination)
                    .HasConstraintName("fkdestination");

                entity.HasOne(d => d.SourceNavigation)
                    .WithMany(p => p.TblindexrelationSourceNavigations)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Source)
                    .HasConstraintName("fksource");
            });

            modelBuilder.Entity<Tblloop>(entity =>
            {
                entity.ToTable("tblloop");

                entity.HasIndex(e => e.Looptemplate, "fklooptemplate");
                entity.HasIndex(e => e.Loopdescription, "fkloopdescription");

                entity.HasIndex(e => e.Loopno, "loopno")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.Loopno)
                    .HasMaxLength(100)
                    .HasColumnName("loopno");

                entity.Property(e => e.Looptemplate)
                    .HasMaxLength(100)
                    .HasColumnName("looptemplate");
                
                entity.Property(e => e.Loopdescription)
                    .HasMaxLength(100)
                    .HasColumnName("loopdescription");
            });

            modelBuilder.Entity<Tbllooptemplate>(entity =>
            {
                entity.ToTable("tbllooptemplate");

                entity.HasIndex(e => e.Looptemplate, "looptemplate");

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnType("mediumtext")
                    .HasColumnName("comment");

                entity.Property(e => e.Description)
                    .HasColumnType("mediumtext")
                    .HasColumnName("description");

                entity.Property(e => e.Looptemplate)
                    .HasMaxLength(100)
                    .HasColumnName("looptemplate");
            });

            modelBuilder.Entity<Tblsdkrelation>(entity =>
            {
                entity.ToTable("tblsdkrelation");

                entity.HasIndex(e => e.Outputtag, "fksdkoutputtag");

                entity.HasIndex(e => e.Parenttags, "fksdkparenttags");

                entity.HasIndex(e => e.Inputtags, "tblsdkrelation_ibfk_1");

                entity.HasIndex(e => e.Sdaction1, "tblsdkrelation_ibfk_10");

                entity.HasIndex(e => e.Sdaction2, "tblsdkrelation_ibfk_11");

                entity.HasIndex(e => e.Projectnumber, "tblsdkrelation_ibfk_6");

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.Inputtags)
                    .HasMaxLength(100)
                    .HasColumnName("inputtags");

                entity.Property(e => e.Notes)
                    .HasColumnType("mediumtext")
                    .HasColumnName("notes");

                entity.Property(e => e.Outputtag)
                    .HasMaxLength(100)
                    .HasColumnName("outputtag");

                entity.Property(e => e.Parenttags)
                    .HasMaxLength(100)
                    .HasColumnName("parenttags");

                entity.Property(e => e.Projectnumber)
                    .HasMaxLength(100)
                    .HasColumnName("projectnumber");

                entity.Property(e => e.Sdaction1)
                    .HasMaxLength(50)
                    .HasColumnName("sdaction1");

                entity.Property(e => e.Sdaction2)
                    .HasMaxLength(50)
                    .HasColumnName("sdaction2");

                entity.Property(e => e.Sdgroup)
                    .HasMaxLength(100)
                    .HasColumnName("sdgroup");

                entity.HasOne(d => d.OutputtagNavigation)
                    .WithMany(p => p.TblsdkrelationOutputtagNavigations)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Outputtag)
                    .HasConstraintName("tblsdkrelation_ibfk_1");

                entity.HasOne(d => d.ParenttagsNavigation)
                    .WithMany(p => p.TblsdkrelationParenttagsNavigations)
                    .HasPrincipalKey(p => p.Tag)
                    .HasForeignKey(d => d.Parenttags)
                    .HasConstraintName("tblsdkrelation_ibfk_2");
            });

            modelBuilder.Entity<Tblsystem>(entity =>
            {
                entity.ToTable("tblsystem");

                entity.HasIndex(e => e.System, "system")
                    .IsUnique();

                entity.HasIndex(e => e.SystemType, "fksystemtype");

                entity.Property(e => e.Id)
                    .HasColumnType("int(25)")
                    .HasColumnName("id");

                entity.Property(e => e.System)
                    .HasMaxLength(50)
                    .HasColumnName("system");

                entity.Property(e => e.SystemType)
                    .HasMaxLength(50)
                    .HasColumnName("systemtype");

                entity.HasOne(d => d.SystemNavigation)
                    .WithOne(p => p.Tblsystem)
                    .HasPrincipalKey<Tblindex>(p => p.System)
                    .HasForeignKey<Tblsystem>(d => d.System)
                    .HasConstraintName("tblarss_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
