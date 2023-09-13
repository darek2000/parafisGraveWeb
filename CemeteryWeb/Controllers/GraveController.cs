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
                model = Helper.GetGraveDetailsList(_dbContext, namePerson, surnamePerson, sector, row, number, null, null);

            return PartialView("_EditGraveListPartial", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Helper.GetGraveDetails(_dbContext, id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditGrave(GraveEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Proszę uzupełnić/poprawić dane";

                model.PhotoList = Helper.GetGravePhotos(_dbContext, model.IdGrave);

                return View(model);
            }

            var result = Helper.EditGrave(_dbContext, model);

            if (result != string.Empty)
            {
                ViewData["ErrorMessage"] = $"Grób nie został zaktualizowany";

                model.PhotoList = Helper.GetGravePhotos(_dbContext, model.IdGrave);

                return View(model);
            }

            ViewData["InfoMessage"] = $"Grób zaktualizowany";

            return View(model);
        }

        [HttpPost]
        public ActionResult DeletePhoto(int photoId)
        {
            var result = Helper.DeletePhoto(_dbContext, photoId);

            return Json(new { status = true, textStatus = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowGravePhotos(int graveId)
        {
            var model = Helper.GetGravePhotos(_dbContext, graveId);

            return PartialView("_GravePhotosPartial", model);
        }

        [HttpPost]
        public ActionResult SaveGraveShape(int graveId, string[][] points, byte locLength)
        {
            var result = Helper.UpdateGraveCoordinates(_dbContext, graveId, points, locLength);

            return Json(new { status = true, textStatus = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            //Helper.GetGraveDetails

            var result = Helper.DeleteGrave(_dbContext, id);

            if (result == string.Empty)
                TempData["InfoMessage"] = $"Grób o id: {id} został skasowany";
            else
                TempData["ErrorMessage"] = result;

            return RedirectToAction("Index", "Grave");
        }
    }
}