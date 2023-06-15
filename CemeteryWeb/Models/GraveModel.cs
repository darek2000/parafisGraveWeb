using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb.Models
{
    public class GraveModel
    {
        public GraveModel()
        {

        }

        public GraveModel(VGravePersonDetail v)
        {
            AttributeOne = v.LocationAttributeOne;
            AttributeTwo = v.LocationAttributeTwo;
            AttributeThree = v.LocationAttributeThree;
            AttributeFour = v.LocationAttributeFour;
            PersonList = new List<PersonModel>();
        }

        public GraveModel(VGravePersonDetail v, List<Person> person, string fileName)
        {
            AttributeOne = v.LocationAttributeOne;
            AttributeTwo = v.LocationAttributeTwo;
            AttributeThree= v.LocationAttributeThree;
            AttributeFour= v.LocationAttributeFour;

            PersonList = new List<PersonModel>();

            foreach(var p in person)
            {
                PersonList.Add(new PersonModel(p));
            }
            Filename = fileName;
        }

        public string AttributeOne { get; set; }
        public string AttributeTwo { get; set; }
        public string AttributeThree { get; set; }
        public string AttributeFour { get; set; }

        public string Filename { get; set; }

        public List<PersonModel> PersonList { get; set; }
    }
}