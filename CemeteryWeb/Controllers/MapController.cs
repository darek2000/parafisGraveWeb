using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CemeteryWeb.Controllers
{
    public class MapController : Controller
    {
        private ParafisDBTestoweEntities _dbContext;

        public MapController()
        {
            _dbContext = new ParafisDBTestoweEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PolygonFromFile_Read()
        {
            var result = Helper.ReadPolygonFromFile();

            return Json(result);
        }

        public ActionResult Polygon_Read()
        {
            var result = Helper.ReadPolygonFromDatabase(_dbContext, string.Empty);

            return Json(result);
        }

        public ActionResult ImportGraveDetails()
        {
            var result = Helper.ReadGraveCoordsFromFileSaveDb(_dbContext);

            //return Json(result);
            return Json(new { status = true, message = result }, JsonRequestBehavior.AllowGet);

            //return Json(new { status = true, textStatus = result }, JsonRequestBehavior.AllowGet);
        }
    }
}