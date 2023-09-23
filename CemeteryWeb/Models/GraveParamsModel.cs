using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class GraveParamsModel
    {
        public GraveParamsModel()
        {

        }

        public GraveParamsModel(VGravePersonDetail v, List<string> dead)
        {
            Id = (int)v.FkGrave;
            LocationAttributeOne = v.LocationAttributeOne;
            LocationAttributeTwo = v.LocationAttributeTwo;
            LocationAttributeThree = v.LocationAttributeThree;
            LocationAttributeFour = v.LocationAttributeFour;
            FkCemetery = (v.FkCemetery != null) ? (int)v.FkCemetery : 0;
            Dead = dead;
        }

        public GraveParamsModel(CemeteryGrave v, List<string> dead)
        {
            Id = v.Id;
            LocationAttributeOne = v.LocationAttributeOne;
            LocationAttributeTwo = v.LocationAttributeTwo;
            LocationAttributeThree = v.LocationAttributeThree;
            LocationAttributeFour = v.LocationAttributeFour;
            FkCemetery = (v.FkCemetery != null) ? (int)v.FkCemetery : 0;
            Dead = dead;
        }

        public int Id { get; set; }

        [Display(Name = "Atrybut lokalizacji")]
        public string LocationAttributeOne { get; set; }

        [Display(Name = "Sektor")]
        public string LocationAttributeTwo { get; set; }

        [Display(Name = "Rząd")]
        public string LocationAttributeThree { get; set; }

        [Display(Name = "Numer")]
        public string LocationAttributeFour { get; set; }

        public int FkCemetery { get; set; }

        public List<string> Dead { get; set; }

        [Display(Name = "Rezerwacja")]
        public bool IsReserved { get; set; }

        [Display(Name = "Weryfikacja")]
        public bool IsForVerification { get; set; }
    }
}