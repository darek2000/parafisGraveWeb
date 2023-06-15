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

        public GraveEditModel(int id, CemeteryGrave g, Person p, string[] photoList)
        {
            AttributeOne = g.LocationAttributeOne;
            AttributeTwo = g.LocationAttributeTwo;
            AttributeThree = g.LocationAttributeThree;
            AttributeFour = g.LocationAttributeFour;

            PersonGrave = new PersonModel(p);

            PhotoList = photoList;
        }

        public int Id { get; set; }

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

        public string[] PhotoList { get; set; }
    }
}