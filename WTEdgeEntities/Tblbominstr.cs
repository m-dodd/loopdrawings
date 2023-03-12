using System;
using System.Collections.Generic;

namespace WTEdge.Entities
{
    public partial class Tblbominstr
    {
        public int Id { get; set; }
        public string? Sitelabel { get; set; }
        public string? Project { get; set; }
        public string? Tag { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Mrno { get; set; }
        public string? Mrrev { get; set; }
        public int? Mrlineno { get; set; }
        public int? Qtyordered { get; set; }
        public int? Qtyreceived { get; set; }
        public int? Qtyremaining { get; set; }
        public decimal? Unitprice { get; set; }
        public string? Currency { get; set; }
        public decimal? Totalpricecad { get; set; }
        public decimal? Totalpriceusd { get; set; }
        public string? Quoteby { get; set; }
        public DateOnly? Mrqissued { get; set; }
        public DateOnly? Quotereceived { get; set; }
        public DateOnly? Mrpissued { get; set; }
        public string? Purchaser { get; set; }
        public string? Pono { get; set; }
        public int? Polineno { get; set; }
        public DateOnly? Orderdate { get; set; }
        public DateOnly? Expecteddelivery { get; set; }
        public DateOnly? Actualdelivery { get; set; }
        public DateOnly? Ros { get; set; }
        public string? Status { get; set; }
        public string? Supplier { get; set; }
        public string? Supplierorder { get; set; }
        public string? Supplierquote { get; set; }
        public int? Supplierquotelineno { get; set; }
        public string? Suppliershiplocation { get; set; }
        public string? Vendordocstatus { get; set; }
        public string? Inspectionstatus { get; set; }
        public string? Location { get; set; }
        public string? Ducolocation { get; set; }
        public string? Ducopackinglist { get; set; }
        public string? Productid { get; set; }
        public string? Comments { get; set; }

        public virtual Tblindex? TagNavigation { get; set; }
    }
}
