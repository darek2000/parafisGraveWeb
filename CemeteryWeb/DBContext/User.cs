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
    
    public partial class User
    {
        public int IDUser { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public string NameSurname { get; set; }
        public Nullable<short> Permission { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
