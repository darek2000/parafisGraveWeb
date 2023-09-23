using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemeteryWeb.Controllers
{
    public class LoginController : Controller
    {
        private CmentarioDBEntities _dbContext;

        public LoginController()
        {
            _dbContext = new CmentarioDBEntities();
        }

        public ActionResult Index()
        {
            Session["UserId"] = null;
            Session["UserNameSurname"] = null;

            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Proszę uzupełnić dane";

                return View(model);
            }

            var user = Helper.GetLoggedUser(_dbContext, model);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Nie ma użytkownika z podanymi danymi";

                return View(model);
            }

            Session["UserId"] = user.IDUser;
            Session["UserNameSurname"] = user.NameSurname;
            Helper.SetLoggedUser(user.IDUser);

            return RedirectToAction("Index", "Grave");
        }
    }
}