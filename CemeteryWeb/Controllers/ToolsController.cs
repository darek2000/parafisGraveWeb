using CemeteryWeb.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemeteryWeb.Controllers
{
    public class ToolsController : Controller
    {
        private ParafisDBTestoweEntities _dbContext;

        public ToolsController()
        {
            _dbContext = new ParafisDBTestoweEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CleanPhotoPath()
        {
            TempData["InfoMessage"] = Helper.CleanPhotoPaths(_dbContext);

            return View();
        }
    }
}