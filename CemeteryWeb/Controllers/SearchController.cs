using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace CemeteryWeb.Controllers
{
	public class SearchController : Controller
	{
		private ParafisDBTestoweEntities _dbContext;

		public SearchController()
		{
			_dbContext = new ParafisDBTestoweEntities();
		}

		public ActionResult Index()
		{
			return View(new CemeteryWeb.Models.SearchModel());
		}

        public ActionResult IndexMap()
        {
            return View(new CemeteryWeb.Models.SearchModel());
        }

        //[HttpPost]
        //public ActionResult SearchGrave(string namePerson, string surnamePerson)
        //{
        //          var result = Helper.GetGraveDetails(_dbContext, namePerson, surnamePerson);

        //          return Json(new { status = true, dataGrave = result }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult SearchGrave(string namePerson, string surnamePerson, string sector, string row, string number)
        {
			var model = new List<GravePersonDetailModel>();

			if (namePerson != string.Empty || surnamePerson != string.Empty || sector != string.Empty || row != string.Empty || number != string.Empty)
                model = Helper.GetGraveDetailsList(_dbContext, namePerson, surnamePerson, sector, row, number);

            return PartialView("_GraveDetailsListPartial", model);
        }
    }
}