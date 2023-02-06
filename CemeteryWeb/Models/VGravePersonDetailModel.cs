using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb.Models
{
    public class VGravePersonDetailModel
    {
        public VGravePersonDetailModel()
        {

        }

        public VGravePersonDetailModel(VGravePersonDetail v)
        {
            Id = v.Id;
            FkGrave = v.FkGrave;
            FkPerson = v.FkPerson;
            Name = v.Name;
            Surname = v.Surname;
            DateBirth = v.DateBirth;
            DateDeath = v.DateDeath;
            YearBirth = v.YearBirth;
            YearDeath = v.YearDeath;
            LocationAttributeOne = v.LocationAttributeOne;
            LocationAttributeTwo = v.LocationAttributeTwo;
            LocationAttributeThree = v.LocationAttributeThree;
            LocationAttributeFour = v.LocationAttributeFour;
        }

        public int Id { get; set; }
        public Nullable<int> FkGrave { get; set; }
        public Nullable<int> FkPerson { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> DateBirth { get; set; }
        public Nullable<System.DateTime> DateDeath { get; set; }
        public Nullable<int> YearBirth { get; set; }
        public Nullable<int> YearDeath { get; set; }
        public string LocationAttributeOne { get; set; }
        public string LocationAttributeTwo { get; set; }
        public string LocationAttributeThree { get; set; }
        public string LocationAttributeFour { get; set; }
    }
}