using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class PersonModel
    {
        public PersonModel()
        {

        }

        public PersonModel(Person v)
        {
            IdPerson = v.Id;
            Name = v.Name;
            Surname = v.Surname;

            if (v.DateBirth != null)
                DateBirth = ((DateTime)v.DateBirth).ToShortDateString();

            if (v.DateDeath != null)
                DateDeath = ((DateTime)v.DateDeath).ToShortDateString();

            if (YearBirth != null)
                YearBirth = (int)YearBirth;

            if (YearDeath != null)
                YearDeath = (int)YearDeath;
        }

        public PersonModel(VGravePersonDetail v)
        {
            IdPerson = v.FkPerson;
            Name = v.Name;
            Surname = v.Surname;

            if (v.DateBirth != null)
                DateBirth = ((DateTime)v.DateBirth).ToShortDateString();

            if (v.DateDeath != null)
                DateDeath = ((DateTime)v.DateDeath).ToShortDateString();

            if (YearBirth != null)
                YearBirth = (int)YearBirth;

            if (YearDeath != null)
                YearDeath = (int)YearDeath;
        }

        public int? IdPerson { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }


        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }


        [Display(Name = "Data urodzenia")]
        public string DateBirth { get; set; }

        [Display(Name = "Data śmierci")]
        public string DateDeath { get; set; }

        [Display(Name = "Rok urodzenia")]
        public int? YearBirth { get; set; }

        [Display(Name = "Rok śmierci")]
        public int? YearDeath { get; set; }
    }
}