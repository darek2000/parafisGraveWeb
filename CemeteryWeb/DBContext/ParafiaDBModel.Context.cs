﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ParafisDBTestoweEntities : DbContext
    {
        public ParafisDBTestoweEntities()
            : base("name=ParafisDBTestoweEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookBaptism> BookBaptism { get; set; }
        public virtual DbSet<BookCommunion> BookCommunion { get; set; }
        public virtual DbSet<BookConfirmation> BookConfirmation { get; set; }
        public virtual DbSet<BookDeath> BookDeath { get; set; }
        public virtual DbSet<BookMarriage> BookMarriage { get; set; }
        public virtual DbSet<Carol> Carol { get; set; }
        public virtual DbSet<CarolFamily> CarolFamily { get; set; }
        public virtual DbSet<Cemetery> Cemetery { get; set; }
        public virtual DbSet<CemeteryGrave> CemeteryGrave { get; set; }
        public virtual DbSet<CemeteryGravePayment> CemeteryGravePayment { get; set; }
        public virtual DbSet<CemeteryGravePerson> CemeteryGravePerson { get; set; }
        public virtual DbSet<CemeteryGravePhoto> CemeteryGravePhoto { get; set; }
        public virtual DbSet<Diary> Diary { get; set; }
        public virtual DbSet<Donation> Donation { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<FamilyMember> FamilyMember { get; set; }
        public virtual DbSet<Finance> Finance { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VisitSickPerson> VisitSickPerson { get; set; }
        public virtual DbSet<VGravePersonDetail> VGravePersonDetail { get; set; }
    }
}
