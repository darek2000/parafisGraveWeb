using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryWeb
{
	public static class Helper
	{

		public static GraveModel GetGraveDetails(ParafisDBTestoweEntities db, string personName, string personSurname)
		{
			try
			{
				var a = db.VGravePersonDetail.Where(x => x.Name == personName && x.Surname == personSurname).ToList().FirstOrDefault();

				var g = db.VGravePersonDetail.Where(x =>
					x.LocationAttributeTwo == a.LocationAttributeTwo &&
					x.LocationAttributeThree == a.LocationAttributeThree &&
					x.LocationAttributeFour == a.LocationAttributeFour).ToList();

				var graveid = g.FirstOrDefault().FkGrave;

				var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).ToList().FirstOrDefault();

				var result = new GraveModel(g.FirstOrDefault());

				if (f != null)
				{
					result.Filename = f.PhotoFile;
				}

				foreach (var p in g)
				{
					result.PersonList.Add(new PersonModel(p)
					{
						Name = p.Name,
						Surname = p.Surname,
						DateBirth = ((DateTime)p.DateBirth).ToShortDateString(),
						DateDeath = ((DateTime)p.DateDeath).ToShortDateString()
					});
				}

				return result;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		public static List<GravePersonDetailModel> GetGraveDetailsList(ParafisDBTestoweEntities db, string personName, string personSurname)
		{
			var result = new List<GravePersonDetailModel>();

			try
			{
				var a = db.VGravePersonDetail.Where(x => (personName != string.Empty) ? x.Name == personName : true 
								&& (personSurname != string.Empty) ? x.Surname == personSurname : true).ToList();

				foreach(var item in a)
				{
					var graveid = item.FkGrave;

					var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).ToList().FirstOrDefault()?.PhotoFile;

					result.Add(new GravePersonDetailModel(item, f));
				}

				return result;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}
	}
}