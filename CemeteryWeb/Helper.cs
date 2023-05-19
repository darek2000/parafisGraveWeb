using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CemeteryWeb
{
	public static class Helper
	{
        private static readonly string _salt = "ProgramParafia";

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

        public static List<GravePersonDetailModel> GetGraveDetailsList(ParafisDBTestoweEntities db, SearchModel search)
        {
            var result = new List<GravePersonDetailModel>();

            try
            {
                var a = db.VGravePersonDetail.Where(x => (search.Name != string.Empty) ? x.Name == search.Name : true
                                && (search.Surname != string.Empty) ? x.Surname == search.Surname : true
                                && (search.Sector != string.Empty) ? x.LocationAttributeTwo == search.Sector : true
                                && (search.Row != string.Empty) ? x.LocationAttributeThree == search.Row : true
                                && (search.Number != string.Empty) ? x.LocationAttributeFour == search.Number : true
                                ).ToList();

                foreach (var item in a)
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

        public static List<GravePersonDetailModel> GetGraveDetailsList(ParafisDBTestoweEntities db, string graveDetails)
		{
			var result = new List<GravePersonDetailModel>();

			var locParams = graveDetails.Split(' ');
			var sector = locParams[0];
			var row = locParams[1];
			var number = locParams[3];

			try
			{
				var a = db.VGravePersonDetail.Where(x => 
									((sector != string.Empty) ? x.LocationAttributeTwo == sector : true)
								&& ((row != string.Empty) ? x.LocationAttributeThree == row : true)
								&& ((number != string.Empty) ? x.LocationAttributeFour == number : true)
								).ToList();

				foreach (var item in a)
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
        private static List<Polygon> GetPolygons(string[] data, string pathError)
        {
            var result = new List<Polygon>();

            foreach (var line in data)
            {
                var col = line.Split(';');

				//result.Add(new Polygon()
				//{
				//    Name = $"{col[0]} {col[1]} {col[2]}",
				//    Points = GetPointList(line, pathError)
				//});

				result.Add(GetSinglePolygon(line, pathError));
            }

            return result;
        }

		private static Polygon GetSinglePolygon(string line, string pathError)
		{
            var col = line.Split(';');

			return new Polygon()
			{
				Name = $"{col[0]} {col[1]} {col[2]}",
				Points = GetPointList(line, pathError)
			};
        }

        public static List<Polygon> ReadPolygonFromFile()
		{
			var result = new List<Polygon>();
			
			var path = AppDomain.CurrentDomain.BaseDirectory + @"\Data\" + "graves.cords.txt";
			var data = System.IO.File.ReadAllLines(path);

			var pathError = AppDomain.CurrentDomain.BaseDirectory + @"\Data\" + "graves.cords.error.txt";

			//if (System.IO.File.Exists(pathError))
			//	System.IO.File.Delete(pathError);

			//TODO moved to the GetPolygons method
			//foreach (var line in data)
			//{
			//	var col = line.Split(';');

			//	result.Add(new Polygon()
			//	{
			//		Name = $"{col[0]} {col[1]} {col[2]}",
			//		Points = GetPointList(line, pathError)
			//	});

			//}

			//return result;

			return GetPolygons(data, pathError);
		}

        public static List<Polygon> ReadPolygonFromDatabase(ParafisDBTestoweEntities db, string pathError)
        {
            var result = new List<Polygon>();

			var list = db.GraveCoordinate.ToList();

			foreach(var g in list)
			{
				result.Add(GetSinglePolygon(g.Coordinate, pathError));
			}

			return result;
        }

        //TODO add filename as a call parameter
        public static string ReadGraveCoordsFromFileSaveDb(ParafisDBTestoweEntities db)
		{
			var result = string.Empty;
			var path = AppDomain.CurrentDomain.BaseDirectory + @"\Data\" + "graves.cords.txt";
			var data = System.IO.File.ReadAllLines(path);

			//var pathError = AppDomain.CurrentDomain.BaseDirectory + @"\Data\" + "graves.cords.error.txt";
			//if (System.IO.File.Exists(pathError))
			//	System.IO.File.Delete(pathError);

			foreach (var line in data)
			{
				try
				{
					var c = db.GraveCoordinate.Add(new GraveCoordinate());

					c.TimeStamp = DateTime.Now;
					c.Coordinate = line;

					db.SaveChanges();

					FindGraveAssignCoordinate(db, line, c.Id);
                }
				catch (Exception e)
				{
					return $"Wystąpił problem podczas importu koordynaty: {line}, Message: {e.Message}, InnerException: {e.InnerException}";
				}
			}

			return "Zaimportowano koordynaty grobów";
		}

		private static void FindGraveAssignCoordinate(ParafisDBTestoweEntities db, string line, int coordsId)
		{
			var locParams = line.Split(';');
			var sector = locParams[0];
			var row = locParams[1];
			var partsNumber = locParams[2].Split(' ');

			var number = partsNumber.Last();

			var graveList = db.CemeteryGrave.Where(x =>
									((sector != string.Empty) ? x.LocationAttributeTwo == sector : true)
								&& ((row != string.Empty) ? x.LocationAttributeThree == row : true)
								&& ((number != string.Empty) ? x.LocationAttributeFour == number : true)
								).ToList();

			foreach (var g in graveList)
			{
				g.FkGraveCoordinate = coordsId;

				db.SaveChanges();
			}
		}

        public static User GetLoggedUser(ParafisDBTestoweEntities db, LoginModel model)
        {
            if (model == null)
                return null;

            return GetUserData(db, model.Username, GetHashedPass(model.Password));
        }

        private static User GetUserData(ParafisDBTestoweEntities db, string login, string password)
        {
            if ((login == null) || (password == null))
                return null;

            var result = db.User.Where(x => x.Login == login && x.Pass == password).FirstOrDefault();

            return result;
        }

        public static string GetHashedPass(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            using (var sha = new SHA256Managed())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(text + _salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }

        public static GraveEditModel GetGraveDetails(ParafisDBTestoweEntities db, int id)
		{
			var info = db.VGravePersonDetail.Find(id);

			var grave = db.CemeteryGrave.Find(info.FkGrave);

			var person = db.Person.Find(info.FkPerson);

			var photos = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == info.FkGrave).Select(z => z.PhotoFile).ToArray();

			return new GraveEditModel(info.Id, grave, person, photos);
		}
    }
}