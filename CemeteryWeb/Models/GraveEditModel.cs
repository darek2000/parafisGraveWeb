using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class GraveEditModel
    {
        public GraveEditModel()
        {
        }

        public GraveEditModel(int id, CemeteryGrave g, Person p, Dictionary<int, string> photoList)
        {
            AttributeOne = g.LocationAttributeOne;
            AttributeTwo = g.LocationAttributeTwo;
            AttributeThree = g.LocationAttributeThree;
            AttributeFour = g.LocationAttributeFour;

            PersonGrave = new PersonModel(p);

            PhotoList = photoList;
        }

        public GraveEditModel(VGravePersonDetail v, Dictionary<int, string> photoList)
        {
            IdGrave = (int)v.FkGrave;
            AttributeOne = v.LocationAttributeOne;
            AttributeTwo = v.LocationAttributeTwo;
            AttributeThree = v.LocationAttributeThree;
            AttributeFour = v.LocationAttributeFour;

            PersonGrave = new PersonModel(v);

            PhotoList = photoList;
            FkCemetery = (v.FkCemetery != null) ? (int)v.FkCemetery : 0;
            FkCemeteryUser = FkCemetery;
            FkPersonGrave = v.Id;
            Description = v.Description;
            IsReserved = (v.IsReserved != null) ? (bool)v.IsReserved : false;
            IsForVerification = (v.IsForVerification != null) ? (bool)v.IsForVerification : false;
        }

        public int IdGrave { get; set; }

        [Display(Name = "Atrybut lokalizacji")]
        public string AttributeOne { get; set; }

        [Display(Name = "Sektor")]
        public string AttributeTwo { get; set; }

        [Display(Name = "Rząd")]
        public string AttributeThree { get; set; }

        [Display(Name = "Numer")]
        public string AttributeFour { get; set; }

        public string Filename { get; set; }

        public PersonModel PersonGrave { get; set; }

        public IEnumerable<HttpPostedFileBase> Photos { get; set; }

        public Dictionary<int, string> PhotoList { get; set; }

        public int FkCemetery { get; set; }
        public int FkCemeteryUser { get; set; }

        [Display(Name = "Data urodz.")]
        public string DateBirth { get; set; }

        [Display(Name = "Rok urodz.")]
        public int BirthYear { get; set; }

        [Display(Name = "Data zgonu")]
        public string DateDeath { get; set; }

        [Display(Name = "Rok zgonu")]
        public int BirthDeath { get; set; }

        //[Display(Name = "Data ur. YYYY")]
        //public int DateBirthYear { get; set; }

        //[Display(Name = "Data ur. MM")]
        //public int DateBirthMonth { get; set; }

        //[Display(Name = "Data ur. DD")]
        //public int DateBirthDay { get; set; }

        public int FkPersonGrave { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Rezerwacja")]
        public bool IsReserved { get; set; }

        [Display(Name = "Weryfikacja")]
        public bool IsForVerification { get; set; }
    }
}