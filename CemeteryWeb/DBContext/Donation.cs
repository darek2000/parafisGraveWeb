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
    
    public partial class Donation
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public Nullable<System.DateTime> DateDonation { get; set; }
        public Nullable<System.DateTime> DateNextDonation { get; set; }
        public Nullable<int> DonationYear { get; set; }
        public Nullable<decimal> AmountMoney { get; set; }
        public string DonationPurposeDesc { get; set; }
        public Nullable<short> DonationPurposeType { get; set; }
        public Nullable<int> FkDonatorPerson { get; set; }
        public Nullable<int> FkDonatorFamily { get; set; }
        public Nullable<int> FkDiary { get; set; }
        public Nullable<int> FkVisitSickPerson { get; set; }
        public Nullable<int> FkCarol { get; set; }
        public Nullable<int> FkCommunion { get; set; }
        public Nullable<bool> BoolFinanceLedger { get; set; }
        public Nullable<int> FkUser { get; set; }
        public Nullable<bool> IsForVerification { get; set; }
    }
}
