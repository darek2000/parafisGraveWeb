using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemeteryWeb.Controllers
{
    public class GraveController : Controller
    {
        private ParafisDBTestoweEntities _dbContext;

        public GraveController()
        {
            _dbContext = new ParafisDBTestoweEntities();
        }

        public ActionResult Index()
        {
            return View(new SearchModel());
        }

        [HttpPost]
        public ActionResult SearchGrave(string namePerson, string surnamePerson, string sector, string row, string number)
        {
            var model = new List<GravePersonDetailModel>();

            if (namePerson != string.Empty || surnamePerson != string.Empty || sector != string.Empty || row != string.Empty || number != string.Empty)
                model = Helper.GetGraveDetailsList(_dbContext, namePerson, surnamePerson, sector, row, number);

            return PartialView("_EditGraveListPartial", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Helper.GetGraveDetails(_dbContext, id);

            return View(model);
        }
    }
}