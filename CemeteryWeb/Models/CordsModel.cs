using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class CordsModel
    {
        public CordsModel()
        {

        }

        public CordsModel(VUnassignedCords v)
        {
            Id = v.Id;
            FkGrave = v.FkGrave;
            Cords = v.Coordinate;

            var locParams = v.Coordinate.Split(';');
            var sector = locParams[0];
            var row = locParams[1];
            var partsNumber = locParams[2].Split(' ');
            var number = partsNumber.Last();

            LocationAttributeOne = string.Empty;
            LocationAttributeTwo = sector;
            LocationAttributeThree= row;
            LocationAttributeFour= number;
        }

        public int Id { get; set; }

        public int? FkGrave { get; set; }

        [Display(Name = "Atrybut lokalizacji")]
        public string LocationAttributeOne { get; set; }

        [Display(Name = "Sektor")]
        public string LocationAttributeTwo { get; set; }

        [Display(Name = "Rząd")]
        public string LocationAttributeThree { get; set; }

        [Display(Name = "Numer")]
        public string LocationAttributeFour { get; set; }

        public string Cords { get; set; }

        public bool GraveExists { get; set; }
    }
}