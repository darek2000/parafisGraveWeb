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
        private CmentarioDBEntities _dbContext;

        public GraveController()
        {
            _dbContext = new CmentarioDBEntities();
        }

        public ActionResult Index()
        {
            return View(new SearchModel());
        }

        public ActionResult Add()
        {
            ViewData["MapAreaName"] = " Cmentarz Sieniawa Żarska";

            return View(new GraveAddModel() { FkCemetery = 1, LocLength = 3, IdGrave = -1 });
        }

        [HttpPost]
        public ActionResult Add(GraveAddModel model)
        {
            ViewData["MapAreaName"] = " Cmentarz Sieniawa Żarska";

            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Proszę uzupełnić/poprawić dane";

                return View(model);
            }

            var result = Helper.AddGrave(_dbContext, model);

            if (result != string.Empty)
            {
                ViewData["ErrorMessage"] = $"Grób nie został dodany. Błąd: {result}";

                model.PhotoList = Helper.GetGravePhotos(_dbContext, (int)model.IdGrave);

                return View(model);
            }

            ViewData["InfoMessage"] = $"Grób dodany";

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Helper.GetGraveDetails(_dbContext, id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GraveEditModel model)
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

        [HttpPost]
        public ActionResult FilterGrave(string nameFilter)
        {
            var model = string.Empty;

            if (nameFilter == "unassignedcords")
            {
                model = Helper.GetUnassignedCordsList(_dbContext);
                return PartialView("_UnassignedCordsListPartial", model);
            }
            else if (nameFilter == "nocords")
            {
                return PartialView("_NoCordsGraveListPartial", Helper.GetNoCordsGraveList(_dbContext));
            }
            else if (nameFilter == "lackinglocparam")
            {
                return PartialView("_LackingParamsGraveListPartial", Helper.GetLackingParamsGraveList(_dbContext));
            }
            else
                return PartialView("_EmptyListPartial", model);
        }

        [HttpPost]
        public ActionResult SearchGrave(string namePerson, string surnamePerson, string sector, string row, string number)
        {
            var model = new List<GravePersonDetailModel>();

            if (namePerson != string.Empty || surnamePerson != string.Empty || sector != string.Empty || row != string.Empty || number != string.Empty)
                model = Helper.GetGraveDetailsList(_dbContext, namePerson, surnamePerson, sector, row, number, null, null);

            return PartialView("_EditGraveListPartial", model);
        }

        [HttpPost]
        public ActionResult EditSearchGrave(string namePerson, string surnamePerson, string sector, string row, string number, bool verification, bool reserved)
        {
            var model = new List<GravePersonDetailModel>();

            if (namePerson != string.Empty || surnamePerson != string.Empty || sector != string.Empty || row != string.Empty || number != string.Empty)
                model = Helper.GetGraveDetailsList(_dbContext, namePerson, surnamePerson, sector, row, number, null, null);

            return PartialView("_EditGraveListPartial", model);
        }
    }
}