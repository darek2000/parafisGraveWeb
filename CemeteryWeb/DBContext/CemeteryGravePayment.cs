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
    
    public partial class CemeteryGravePayment
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public Nullable<int> FkGrave { get; set; }
        public Nullable<System.DateTime> DatePayment { get; set; }
        public Nullable<decimal> MoneyAmount { get; set; }
        public string PaymentPurpose { get; set; }
        public Nullable<int> FkPayer { get; set; }
        public string PayerName { get; set; }
        public string PayerSurname { get; set; }
        public string Description { get; set; }
        public Nullable<int> FkUser { get; set; }
    }
}