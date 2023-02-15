using CemeteryWeb.DBContext;
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

		//[HttpPost]
		//public ActionResult SearchGrave(string namePerson, string surnamePerson)
		//{
  //          var result = Helper.GetGraveDetails(_dbContext, namePerson, surnamePerson);

  //          return Json(new { status = true, dataGrave = result }, JsonRequestBehavior.AllowGet);
		//}

        [HttpPost]
        public ActionResult SearchGrave(string namePerson, string surnamePerson)
        {
            //var model = Helper.GetGraveDetails(_dbContext, namePerson, surnamePerson);
            var model = Helper.GetGraveDetailsList(_dbContext, namePerson, surnamePerson);

            return PartialView("_GraveDetailsListPartial", model);
        }
    }
}