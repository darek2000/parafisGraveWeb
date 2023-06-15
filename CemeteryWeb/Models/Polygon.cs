using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb.Models
{
    public class Polygon
    {
        public Polygon()
        {
            Name = string.Empty;
            Points = new List<Point>();
        }

        public string Name { get; set; }
        public List<Point> Points { get; set; }

    }
}