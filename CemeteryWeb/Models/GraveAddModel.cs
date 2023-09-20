using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class GraveAddModel
    {
        public GraveAddModel()
        {
        }

        public int? IdGrave { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }


        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }


        [Display(Name = "Aleja")]
        public string AttributeOne { get; set; }

        [Display(Name = "Sektor")]
        public string AttributeTwo { get; set; }

        [Display(Name = "Rząd")]
        public string AttributeThree { get; set; }

        [Display(Name = "Numer")]
        public string AttributeFour { get; set; }

        public int? FkCemetery { get; set; }

        public int? FkPersonGrave { get; set; }

        public int? FkPerson { get; set; }

        [Display(Name = "Data urodz.")]
        public string DateBirth { get; set; }

        [Display(Name = "Rok urodz.")]
        public int? BirthYear { get; set; }

        [Display(Name = "Data zgonu")]
        public string DateDeath { get; set; }

        [Display(Name = "Rok zgonu")]
        public int? DeathYear { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Rezerwacja")]
        public bool IsReserved { get; set; }

        [Display(Name = "Weryfikacja")]
        public bool IsForVerification { get; set; }

        public Dictionary<int, string> PhotoList { get; set; }

        public byte LocLength { get; set; }

        public IEnumerable<HttpPostedFileBase> Photos { get; set; }
    }
}