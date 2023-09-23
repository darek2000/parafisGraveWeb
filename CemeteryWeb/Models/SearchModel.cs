using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class SearchModel
    {
        public SearchModel()
        {

        }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Sektor")]
        public string Sector { get; set; }

        [Display(Name = "Rząd")]
        public string Row { get; set; }

        [Display(Name = "Miejsce")]
        public string Number { get; set; }

        [Display(Name = "Data urodz.")]
        public string DateBirth { get; set; }

        [Display(Name = "Data śmierci")]
        public string DateDeath { get; set; }

        [Display(Name = "Rok urodz.")]
        public int? YearBirth { get; set; }

        [Display(Name = "Rok śmierci")]
        public int? YearDeath { get; set; }

        [Display(Name = "Rezerwacja")]
        public bool IsReserved { get; set; }

        [Display(Name = "Weryfikacja")]
        public bool IsForVerification { get; set; }
    }
}