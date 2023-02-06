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
    
    public partial class Person
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DateBirth { get; set; }
        public Nullable<System.DateTime> DateBaptism { get; set; }
        public Nullable<System.DateTime> DateConfirmation { get; set; }
        public Nullable<System.DateTime> DateWedding { get; set; }
        public Nullable<System.DateTime> DateDeath { get; set; }
        public string Description { get; set; }
        public Nullable<int> FKMother { get; set; }
        public Nullable<int> FKFather { get; set; }
        public string PlaceBirth { get; set; }
        public string ParishConfirmation { get; set; }
        public Nullable<int> FKUser { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<short> PersonSex { get; set; }
        public Nullable<int> FKSpouse { get; set; }
        public string MotherName { get; set; }
        public string MotherSurname { get; set; }
        public string FatherName { get; set; }
        public string FatherSurname { get; set; }
        public string SpouseName { get; set; }
        public string SpouseSurname { get; set; }
        public Nullable<int> YearBirth { get; set; }
        public Nullable<short> PersonFaith { get; set; }
        public string Profession { get; set; }
        public Nullable<int> YearBaptism { get; set; }
        public Nullable<int> YearCommunion { get; set; }
        public Nullable<System.DateTime> DateCommunion { get; set; }
        public Nullable<bool> IsForVerification { get; set; }
        public Nullable<int> YearDeath { get; set; }
    }
}
