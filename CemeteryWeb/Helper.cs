using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb
{
    public static class Helper
    {

        public static GraveModel GetGraveDetails(ParafisDBTestoweEntities db, string personName, string personSurname)
        {
            var a = db.VGravePersonDetail.Where(x => x.Name == personName && x.Surname == personSurname).ToList().FirstOrDefault();

            var g = db.VGravePersonDetail.Where(x =>
                x.LocationAttributeTwo == a.LocationAttributeTwo &&
                x.LocationAttributeThree == a.LocationAttributeThree &&
                x.LocationAttributeFour == a.LocationAttributeFour).ToList();

            var result = new GraveModel(g.FirstOrDefault());

            foreach(var p in g)
            {
                result.PersonList.Add(new PersonModel(p)
                { 
                    Name = p.Name,
                    Surname = p.Surname,
                    DateBirth=((DateTime)p.DateBirth).ToShortDateString(), 
                    DateDeath =((DateTime)p.DateDeath).ToShortDateString()
                });
            }

            return result;
        }
    }
}