//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CemeteryWeb.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierEmail { get; set; }
        public Nullable<int> FkUser { get; set; }
        public Nullable<short> SupplierType { get; set; }
        public Nullable<short> SupplierCategory { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsForVerification { get; set; }
    }
}