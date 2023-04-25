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

		public static List<GravePersonDetailModel> GetGraveDetailsList(ParafisDBTestoweEntities db, string personName, string personSurname, string sector, string row, string number)
		{
			var result = new List<GravePersonDetailModel>();

			try
			{
				var a = db.VGravePersonDetail.Where(x => (personName != string.Empty) ? x.Name == personName : true 
								&& (personSurname != string.Empty) ? x.Surname == personSurname : true
								&& (sector != string.Empty) ? x.LocationAttributeTwo == sector : true
								&& (row != string.Empty) ? x.LocationAttributeThree == row : true
								&& (number != string.Empty) ? x.LocationAttributeFour == number : true
								).ToList();

				foreach(var item in a)
				{
					var graveid = item.FkGrave;

					var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).ToList().FirstOrDefault()?.PhotoFile;

					result.Add(new GravePersonDetailModel(item, f == null ? "nopicture.png" : f));
				}

				return result;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

        public static string CleanPhotoPaths(ParafisDBTestoweEntities db)
		{
			var photos = db.CemeteryGravePhoto.Where(x => x.PhotoFile != null).ToList();
			bool changeFlag = false;

			try
			{
				foreach (var p in photos)
				{
					if (p.PhotoFile.Contains("\\") == true)
					{
						RemovePathPart(p);
						changeFlag = true;
					}
				}

				if (changeFlag)
					db.SaveChanges();

				return "Nazwy plików zostały wyczyszczone ze scieżek";
            }
			catch (Exception ex)
			{
                return $"Message: {ex.Message}, InnerException: {ex.InnerException}";
            }
        }

		public static void RemovePathPart(CemeteryGravePhoto p)
		{
            p.PhotoFile = System.IO.Path.GetFileName(p.PhotoFile);
		}

        private static List<Point> GetPointList(string line, string pathError)
        {
            var result = new List<Point>();

            var columns = line.Split(';');

            var idx = 0;

            if (line.Count(x => x == ';') > 4)
            {
                idx = 0;
            }

            try
            {
                foreach (var col in columns)
                {
                    if (idx < 3)
                    {
                        idx++;
                        continue;
                    }

                    var cords = col.Split(',');

					decimal val = 0;

					if (!decimal.TryParse(cords[0].Replace('.', ','), out val) || !decimal.TryParse(cords[1].Replace('.', ','), out val))
					{
						//System.IO.File.AppendAllText(pathError, line + "\n");
					}
					else
						result.Add(new Point() { X = decimal.Parse(cords[0].Replace('.', ',')), Y = decimal.Parse(cords[1].Replace('.', ',')) });

                    idx++;
                }

                return result;
            }
            catch (Exception ex)
            {
                var u = 0;

                return result;
            }
        }

        public static List<Polygon> GetPolygonList()
		{
            var result = new List<Polygon>();

            var path = AppDomain.CurrentDomain.BaseDirectory + @"\Data\" + "graves.cords.txt";
            var data = System.IO.File.ReadAllLines(path);

			var pathError = AppDomain.CurrentDomain.BaseDirectory + @"\Data\" + "graves.cords.error.txt";

			//if (System.IO.File.Exists(pathError))
			//	System.IO.File.Delete(pathError);

            foreach (var line in data)
            {
                var col = line.Split(';');

                result.Add(new Polygon()
                {
                    Name = $"{col[0]} {col[1]} {col[2]}",
                    Points = GetPointList(line, pathError)
                });

            }

			return result;
        }
    }
}