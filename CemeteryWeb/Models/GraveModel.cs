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
            AttributeThree= v.LocationAttributeThree;
            AttributeFour= v.LocationAttributeFour;
            PersonList = new List<PersonModel>();
        }

        public string AttributeOne { get; set; }
        public string AttributeTwo { get; set; }
        public string AttributeThree { get; set; }
        public string AttributeFour { get; set; }

        public List<PersonModel> PersonList { get; set; }
    }
}