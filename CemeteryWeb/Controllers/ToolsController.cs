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
        private CmentarioDBEntities _dbContext;

        public ToolsController()
        {
            _dbContext = new CmentarioDBEntities();
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