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
    
    public partial class BookBaptism
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public string ActNo { get; set; }
        public string ActPage { get; set; }
        public string ActYear { get; set; }
        public string BaptismPlace { get; set; }
        public Nullable<System.DateTime> BaptismDate { get; set; }
        public string BaptismName { get; set; }
        public string BaptismSurname { get; set; }
        public Nullable<int> BaptismPersonId { get; set; }
        public Nullable<System.DateTime> BaptismPersonBirthDate { get; set; }
        public Nullable<int> YearBirth { get; set; }
        public string BaptismMotherName { get; set; }
        public string BaptismMotherSurname { get; set; }
        public Nullable<short> BaptismMotherFaith { get; set; }
        public string BaptismFatherName { get; set; }
        public string BaptismFatherSurname { get; set; }
        public Nullable<short> BaptismFatherFaith { get; set; }
        public Nullable<short> MarriageType { get; set; }
        public string MarriagePlace { get; set; }
        public Nullable<System.DateTime> MarriageDate { get; set; }
        public string BaptismGodmotherName { get; set; }
        public string BaptismGodmotherSurname { get; set; }
        public Nullable<short> BaptismGodmotherFaith { get; set; }
        public string BaptismGodfatherName { get; set; }
        public string BaptismGodfatherSurname { get; set; }
        public Nullable<short> BaptismGodfatherFaith { get; set; }
        public string SacramentDetails { get; set; }
        public string Remarks { get; set; }
        public string PastorNameSurname { get; set; }
        public Nullable<int> FkUser { get; set; }
        public Nullable<int> FkPerson { get; set; }
        public Nullable<int> FkMother { get; set; }
        public Nullable<int> FkFather { get; set; }
        public Nullable<int> FkGodmother { get; set; }
        public Nullable<int> FkGodfather { get; set; }
        public Nullable<int> YearMarriage { get; set; }
        public Nullable<int> YearBaptism { get; set; }
        public Nullable<bool> IsForVerification { get; set; }
    }
}
