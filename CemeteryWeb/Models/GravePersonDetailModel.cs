using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb.Models
{
    public class GravePersonDetailModel
    {
        public GravePersonDetailModel()
        {

        }

        public GravePersonDetailModel(VGravePersonDetail v, string filename) : this(v)
        {
            FileName= filename;
        }

        public GravePersonDetailModel(VGravePersonDetail v, List<string> filenameList) : this(v)
        {
            FileName = filenameList.FirstOrDefault();

            PhotoList.AddRange(filenameList);
        }

        public GravePersonDetailModel(VGravePersonDetail v)
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

            //if (locLength == 2)
            //    Location = $"{v.LocationAttributeThree} {v.LocationAttributeFour}";

            //if (locLength == 3)
            //    Location = $"{v.LocationAttributeTwo} {v.LocationAttributeThree} Grób {v.LocationAttributeFour}";

            Location = $"{v.LocationAttributeTwo} {v.LocationAttributeThree} Grób {v.LocationAttributeFour}";

            PhotoList = new List<string>();

            Description = v.Description;
            IsReserved = (v.IsReserved != null) ? (bool)v.IsReserved : false;
            IsForVerification = (v.IsForVerification != null) ? (bool)v.IsForVerification : false;
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
        public string FileName { get; set; }

        public string Location { get; set; }

        public List<string> PhotoList { get; set; }
        public string Description { get; set; }
        public bool IsReserved { get; set; }
        public bool IsForVerification { get; set; }
    }
}