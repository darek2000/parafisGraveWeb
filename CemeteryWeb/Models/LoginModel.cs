using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CemeteryWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login nie może być pusty")]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło nie może być puste")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}