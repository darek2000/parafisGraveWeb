using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb.Models
{
    public class PersonModel
    {
        public PersonModel()
        {

        }

        public PersonModel(VGravePersonDetail v)
        {
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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateBirth { get; set; }
        public string DateDeath { get; set; }
        public int? YearBirth { get; set; }
        public int? YearDeath { get; set; }
    }
}