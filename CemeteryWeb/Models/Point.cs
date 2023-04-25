using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb.Models
{
    public class Point
    {
        public Point()
        {
            X = 0;
            Y = 0;
        }

        public decimal X { get; set; }
        public decimal Y { get; set; }

    }
}