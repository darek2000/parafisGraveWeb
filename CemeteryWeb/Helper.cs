using CemeteryWeb.DBContext;
using CemeteryWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CemeteryWeb
{
	public static class Helper
	{
        private static readonly string _salt = "ProgramParafia";

        private static readonly string _gravePhotosDir = AppDomain.CurrentDomain.BaseDirectory + "GravePhotos";

        private static int _loggedUser = -1;

        public static void SetLoggedUser(int id)
        {
            _loggedUser = id;
        }

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

		public static List<GravePersonDetailModel> GetGraveDetailsList(ParafisDBTestoweEntities db, string personName, string personSurname, string sector, string row, string number, int? yearBirth, int? yearDeath)
		{
			var result = new List<GravePersonDetailModel>();

			try
			{
				var a = db.VGravePersonDetail.Where(x => ((personName != string.Empty) ? x.Name == personName : true)
								&& ((personSurname != string.Empty) ? x.Surname == personSurname : true)
								&& ((sector != string.Empty) ? x.LocationAttributeTwo == sector : true)
								&& ((row != string.Empty) ? x.LocationAttributeThree == row : true)
								&& ((number != string.Empty) ? x.LocationAttributeFour == number : true)
								&& ((yearBirth != null) ? (x.YearBirth == yearBirth || x.DateBirth.Value.Year == yearBirth) : true)
                                && ((yearDeath != null) ? (x.YearDeath == yearDeath || x.DateDeath.Value.Year == yearDeath) : true)
                                ).ToList();

				foreach(var item in a)
				{
					var graveid = item.FkGrave;

                    //var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).ToList().FirstOrDefault()?.PhotoFile;
                    var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).Select(z => z.PhotoFile).ToList();

                    //result.Add(new GravePersonDetailModel(item, f == null ? "nopicture.png" : f));
                    result.Add(new GravePersonDetailModel(item, GetPhotoList(f)));
                }

                return result;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

        private static List<string> GetPhotoList(List<string> list)
        {
            if (list.Count == 0)
                return new List<string>() { "nopicture.png" };
            else
                return list;
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

                    //var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).ToList().FirstOrDefault()?.PhotoFile;
                    var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).Select(z => z.PhotoFile).ToList();

                    //result.Add(new GravePersonDetailModel(item, f == null ? "nopicture.png" : f));
                    result.Add(new GravePersonDetailModel(item, GetPhotoList(f)));
                }

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static List<GravePersonDetailModel> GetGraveDetailsList(ParafisDBTestoweEntities db, string graveDetails, byte locLength, int cemeteryId = -1)
        {
			var result = new List<GravePersonDetailModel>();

            var sector = string.Empty;
            var row = string.Empty;
            var number = string.Empty;

            var locParams = graveDetails.Replace("Grób ", "").Split(' ');
            //var sector = locParams[0];
            //var row = locParams[1];
            //var number = locParams[3];

            if (locLength == 3)
            {
                sector = locParams[0];
                row = locParams[1];
                number = locParams[2];
            }
            else
            {
                row = locParams[0];
                number = locParams[1];
            }

            try
			{
				var a = db.VGravePersonDetail.Where(x => 
									((sector != string.Empty) ? x.LocationAttributeTwo == sector : true)
								&& ((row != string.Empty) ? x.LocationAttributeThree == row : true)
								&& ((number != string.Empty) ? x.LocationAttributeFour == number : true)
                                && (x.FkCemetery == cemeteryId)).ToList();

                foreach (var item in a)
				{
					var graveid = item.FkGrave;

                    //var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).ToList().FirstOrDefault()?.PhotoFile;

                    //result.Add(new GravePersonDetailModel(item, f == null ? "nopicture.png" : f));

                    var f = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveid).Select(z => z.PhotoFile).ToList();

                    result.Add(new GravePersonDetailModel(item, GetPhotoList(f)));
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

        public static List<Polygon> ReadPolygonSingleFromDatabase(ParafisDBTestoweEntities db, string pathError, byte locLength, string sector, string row, string number, int cemeteryId)
        {
            var result = new List<Polygon>();

            var coordsId = db.CemeteryGrave.Where(x => x.FkCemetery == cemeteryId && x.FkGraveCoordinate != null
                                                && ((sector != string.Empty) ? x.LocationAttributeTwo == sector : true)
                                                && ((row != string.Empty) ? x.LocationAttributeThree == row : true)
                                                && ((number != string.Empty) ? x.LocationAttributeFour == number : true)
                                                ).Select(z => z.FkGraveCoordinate).ToList();

            var list = db.GraveCoordinate.Where(x => coordsId.Contains(x.Id) == true).ToList();

            foreach (var g in list)
            {
                result.Add(GetSinglePolygon(g.Coordinate, pathError));  //, locLength));
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

            //var photos = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == info.FkGrave).Select(z => z.PhotoFile).ToArray();
            var photos = GetGravePhotos(db, (int)info.FkGrave);

            //return new GraveEditModel(info.Id, grave, person, photos);
            return new GraveEditModel(info, photos);
        }

        public static Dictionary<int, string> GetGravePhotos(ParafisDBTestoweEntities db, int id)
        {
            return db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == id).ToDictionary(z => z.Id, z => z.PhotoFile);
        }

        public static string UpdateGraveCoordinates(ParafisDBTestoweEntities db, int graveId, string[][] points, byte locLength)
        {
            var result = string.Empty;

            var grave = db.CemeteryGrave.Find(graveId);

            if (grave == null)
                return $"Brak grobu o identyfikatorze id: {graveId}";

            var coords = db.GraveCoordinate.Find(grave.FkGraveCoordinate);

            if (coords == null)
            {
                coords = db.GraveCoordinate.Add(new GraveCoordinate());

                coords.TimeStamp = DateTime.Now;

                var locAttrib = (locLength == 3) ? $"{grave.LocationAttributeTwo};{grave.LocationAttributeThree};{grave.LocationAttributeFour}" : $"{grave.LocationAttributeThree};{grave.LocationAttributeFour}";

                coords.Coordinate = locAttrib;

                db.SaveChanges();

                grave.FkGraveCoordinate = coords.Id;

                db.SaveChanges();

                result = $"Brak koordynat o identyfikatorze id: {grave.FkGraveCoordinate}. Utworozno obiekt z koordynatami";
            }

            try
            {
                var col = coords.Coordinate.Split(';');

                var locAttrib = (locLength == 3) ? $"{col[0]};{col[1]};{col[2]}" : $"{col[0]};{col[1]}";

                var locPoints = GetCoordinateLine(points).TrimEnd(';');

                coords.Coordinate = $"{locAttrib};{locPoints}";

                db.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {

                return $"Wystąpił wyjątek podczas zapisywania koordynat dla grobu id: {graveId}, Msg:{ex.Message}, IntMsg: {ex.InnerException.Message}";
            }
        }

        private static string GetCoordinateLine(string[][] points)
        {
            var result = string.Empty;

            foreach (var p in points)
                result += $"{p[0]},{p[1]};";

            return result;
        }

        private static int? CheckIfGraveExists(ParafisDBTestoweEntities db, GraveAddModel model)
        {
            var g = db.CemeteryGrave.Where(x => x.FkCemetery == model.FkCemetery
                                    && ((model.LocLength >= 3 && model.AttributeTwo != string.Empty) ? x.LocationAttributeTwo == model.AttributeTwo : true)
                                    && ((model.LocLength >= 2 && model.AttributeThree != string.Empty) ? x.LocationAttributeThree == model.AttributeThree : true)
                                    && ((model.LocLength >= 1 && model.AttributeFour != string.Empty) ? x.LocationAttributeFour == model.AttributeFour : true)
                                    ).FirstOrDefault();

            if (g != null)
                model.IdGrave = g.Id;

            return (g != null) ? g.Id : -1;
        }

        private static string AddGraveDB(ParafisDBTestoweEntities db, GraveAddModel model)
        {
            string result = string.Empty;

            try
            {
                var g = db.CemeteryGrave.Add(new CemeteryGrave());

                g.TimeStamp = DateTime.Now;
                g.FkCemetery = model.FkCemetery;
                g.LocationAttributeOne = model.AttributeOne;
                g.LocationAttributeTwo = model.AttributeTwo;
                g.LocationAttributeThree = model.AttributeThree;
                g.LocationAttributeFour = model.AttributeFour;
                g.FkUser = _loggedUser;
                g.IsForVerification = model.IsForVerification;
                g.IsReserved = model.IsReserved;

                db.SaveChanges();

                model.IdGrave = g.Id;

                return result;
            }
            catch (Exception ex)
            {

                return $"Wyjątek podczas dodawania grobu. Message: {ex.Message}, Internal.Message: {ex.InnerException.Message}";
            }
        }

        private static string CheckIfPersonExists(ParafisDBTestoweEntities db, GraveAddModel model)
        {
            DateTime dtBirth = DateTime.Now;
            DateTime dtDeath = DateTime.Now;
            string result = string.Empty;

            if (model.DateBirth != null)
            {
                if (DateTime.TryParse(model.DateBirth, out dtBirth) != true)
                {
                    result += "Nie można rozpoznać daty urodzenia\n";
                }
            }

            if (model.DateDeath != null)
            {
                if (DateTime.TryParse(model.DateDeath, out dtDeath) != true)
                {
                    result += "Nie można rozpoznać daty zgonu\n";
                }
            }

            if (result != string.Empty)
                return result;

            var g = db.Person.Where(x => x.Name == model.Name && x.Surname == model.Surname
                                    && ((model.BirthYear != null) ? x.YearBirth == model.BirthYear : true)
                                    && ((model.DeathYear != null) ? x.YearDeath == model.DeathYear : true)
                                    && ((model.DateBirth != string.Empty) ? (x.DateBirth.Value.Year == dtBirth.Year && x.DateBirth.Value.Month == dtBirth.Month && x.DateBirth.Value.Day == dtBirth.Day) : true)
                                    && ((model.DateDeath != string.Empty) ? (x.DateDeath.Value.Year == dtDeath.Year && x.DateDeath.Value.Month == dtDeath.Month && x.DateDeath.Value.Day == dtDeath.Day) : true)
                                    ).FirstOrDefault();

            return (g != null) ? $"Osoba już istnieje. Nie można dodać osoby z tymi samymi danymi" : string.Empty;
        }

        private static string AddPersonDB(ParafisDBTestoweEntities db, GraveAddModel model)
        {
            string result = string.Empty;
            DateTime dt;

            try
            {
                var r = db.Person.Add(new Person());

                r.TimeStamp = DateTime.Now;
                r.Name = model.Name;
                r.Surname = model.Surname;

                if (model.DateBirth != null)
                {
                    if (DateTime.TryParse(model.DateBirth, out dt) == true)
                    {
                        r.DateBirth = dt;
                    }
                    else
                        result += "Nie można zapisać daty urodzenia\n";
                }
                else
                {
                }

                if (model.BirthYear != null)
                    r.YearBirth = model.BirthYear;

                if (model.DateDeath != null)
                {
                    if (DateTime.TryParse(model.DateDeath, out dt) == true)
                    {
                        r.DateDeath = dt;
                    }
                    else
                        result += "Nie można zapisać daty zgonu\n";
                }
                else
                {
                }

                if (model.DeathYear != null)
                    r.YearDeath = model.DeathYear;

                r.FKUser = _loggedUser;

                db.SaveChanges();

                model.FkPerson = r.Id;

                return result;
            }
            catch (Exception ex)
            {

                return $"Wyjątek podczas dodawania grobu. Message: {ex.Message}, Internal.Message: {ex.InnerException.Message}";
            }
        }

        private static string AddGravePersonDB(ParafisDBTestoweEntities db, GraveAddModel model)
        {
            string result = string.Empty;
            DateTime dt;

            try
            {
                var g = db.CemeteryGravePerson.Add(new CemeteryGravePerson());

                g.TimeStamp = DateTime.Now;
                g.Surname = model.Surname;
                g.Name = model.Name;

                if (model.DateBirth != null)
                {
                    if (DateTime.TryParse(model.DateBirth, out dt) == true)
                    {
                        g.DateBirth = dt;
                    }
                    else
                        result += "Nie można zapisać daty urodzenia\n";
                }
                else
                {
                }

                //if (model.BirthYear != null)
                //    g.YearBirth = model.BirthYear;

                if (model.DateDeath != null)
                {
                    if (DateTime.TryParse(model.DateDeath, out dt) == true)
                    {
                        g.DateDeath = dt;
                    }
                    else
                        result += "Nie można zapisać daty zgonu\n";
                }
                else
                {
                }

                //if (model.DeathYear != null)
                //    g.YearDeath = model.DeathYear;

                g.FkGrave = model.IdGrave;
                g.FkPerson = model.FkPerson;
                g.FkUser = _loggedUser;

                db.SaveChanges();

                return string.Empty;
            }
            catch (Exception ex)
            {

                return $"Wystąpił błąd podczas dodawania grobu z osobą. Błąd Message: {ex.Message}, InternalMessage: {ex.InnerException.Message}";
            }
        }

        public static string AddGrave(ParafisDBTestoweEntities db, GraveAddModel model)
        {
            string result = string.Empty;

            var graveId = CheckIfGraveExists(db, model);

            if (graveId == -1)
                result = AddGraveDB(db, model);

            if (result != string.Empty)
                return result;

            result = CheckIfPersonExists(db, model);

            if (result != string.Empty)
                return result;

            result = AddPersonDB(db, model);

            if (result != string.Empty)
                return result;

            result = AddGravePersonDB(db, model);

            if (result != string.Empty)
                return result;

            foreach (var file in model.Photos)
            {
                if (file == null)
                    continue;

                if (CheckIfPhotoExist(db, (int)graveId, file.FileName) != 0)
                    continue;

                SaveImageToFolder(_gravePhotosDir, file);

                AddPhotoToGrave(db, (int)graveId, file.FileName);
            }

            model.PhotoList = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveId).ToDictionary(c => c.Id, c => c.PhotoFile);

            return result;
        }

        public static string EditGrave(ParafisDBTestoweEntities db, GraveEditModel model)
        {
            string result = string.Empty;

            if (model.IdGrave == -1)
                return result;

            var grave = db.CemeteryGrave.Find(model.IdGrave);

            //TODO 
            //save Grave changes, person, photos
            UpdateCemeteryGrave(db, model);

            result = UpdatePerson(db, model);

            //if result not string.empty then doent SaveChange
            if (result != string.Empty)
                return result;

            db.SaveChanges();

            foreach (var file in model.Photos)
            {
                if (file == null)
                    continue;

                if (CheckIfPhotoExist(db, grave.Id, file.FileName) != 0)
                    continue;

                SaveImageToFolder(_gravePhotosDir, file);

                AddPhotoToGrave(db, model.IdGrave, file.FileName);
            }

            model.PhotoList = db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == grave.Id).ToDictionary(c => c.Id, c => c.PhotoFile);

            return result;
        }

        public static string DeleteGrave(ParafisDBTestoweEntities db, int idGrave)
        {
            try
            {
                var gp = db.CemeteryGravePerson.Find(idGrave);

                if (gp == null)
                    return $"Grób o id:{idGrave} nie istnieje";

                var p = db.Person.Find(gp.FkPerson);

                if (p != null)
                    db.Person.Remove(p);

                db.CemeteryGravePerson.Remove(gp);

                db.SaveChanges();

                return string.Empty;
            }
            catch (Exception ex)
            {

                return $"Wystąpił błąd podczas kasowania grobu o id:{idGrave}. Message: {ex.Message}, InternalException: {ex.InnerException.Message}";
            }
        }

        public static string UpdatePerson(ParafisDBTestoweEntities db, GraveEditModel model)
        {
            string result = string.Empty;

            DateTime dt;
            int year = -1;

            try
            {
                var p = db.Person.Find(model.PersonGrave.IdPerson);

                p.Name = model.PersonGrave.Name;
                p.Surname = model.PersonGrave.Surname;
                if (model.PersonGrave.DateBirth != null)
                {
                    if (DateTime.TryParse(model.PersonGrave.DateBirth, out dt) == true)
                    {
                        if (p.DateBirth != dt)
                        {
                            p.DateBirth = dt;
                        }
                    }
                    else
                        result += "Nie można zapisać daty urodzenia\n";
                }
                else
                {
                    //clear DateBirth
                    //if (p.DateBirth != null)
                    //	p.DateBirth = null;
                }

                if (p.YearBirth != model.PersonGrave.YearBirth)
                    p.YearBirth = model.PersonGrave.YearBirth;

                if (model.PersonGrave.DateDeath != null)
                {
                    if (DateTime.TryParse(model.PersonGrave.DateDeath, out dt) == true)
                    {
                        if (p.DateDeath != dt)
                            p.DateDeath = dt;
                    }
                    else
                        result += "Nie można zapisać daty zgonu\n";
                }
                else
                {
                    //clear DateDeath
                }

                if (p.YearDeath != model.PersonGrave.YearDeath)
                    p.YearDeath = model.PersonGrave.YearDeath;
            }
            catch (Exception ex)
            {

                result += $"Wyjątek: {ex.Message}, {ex.InnerException.Message}";
            }

            return result;
        }

        private static string AddPhotoToGrave(ParafisDBTestoweEntities db, int graveId, string fileName)
        {
            string result = string.Empty;

            try
            {
                var p = db.CemeteryGravePhoto.Add(new CemeteryGravePhoto());

                p.TimeStamp = DateTime.Now;
                p.FkCemeteryGrave = graveId;
                p.FkUser = _loggedUser;
                p.PhotoFile = fileName;

                db.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                return $"Wystąpił wyjątek podczas dodawania zdjęcia {fileName} dla grobu {graveId}";
            }
        }

        private static int CheckIfPhotoExist(ParafisDBTestoweEntities db, int graveId, string fileName)
        {
            return db.CemeteryGravePhoto.Where(x => x.FkCemeteryGrave == graveId && x.PhotoFile == fileName).Count();
        }

        public static string SaveImageToFolder(string pathName, HttpPostedFileBase httpFile)
        {
            var result = string.Empty;

            MemoryStream target = new MemoryStream();

            httpFile.InputStream.CopyTo(target);

            byte[] img = target.ToArray();

            var pathFull = string.Format(@"{0}\{1}", pathName, httpFile.FileName);

            using (FileStream file = new FileStream(pathFull, FileMode.Create, System.IO.FileAccess.Write))
            {
                try
                {
                    file.Write(img, 0, img.Length);

                    return result;

                }
                catch (Exception e)
                {
                    return $"Błąd podczas zapisania tymczasowo zdjęcia {httpFile.FileName} w folderze {pathName}. Message: {e.Message}, InnerException: {e.InnerException?.Message}\n";
                }
            }
        }

        //TODO result as string and try-catch
        public static void UpdateCemeteryGrave(ParafisDBTestoweEntities db, GraveEditModel model)
        {
            CemeteryGrave grave;
            CemeteryGravePerson gp;
            bool flagSave = false;

            grave = db.CemeteryGrave.Find(model.IdGrave);
            gp = db.CemeteryGravePerson.Find(model.FkPersonGrave);

            if (grave.LocationAttributeOne != model.AttributeOne || grave.LocationAttributeTwo != model.AttributeTwo
                || grave.LocationAttributeThree != model.AttributeThree || grave.LocationAttributeFour != model.AttributeFour || grave.FkCemetery != model.FkCemeteryUser)
            {
                grave = db.CemeteryGrave.Where(x =>
                                            ((model.AttributeOne != null) ? x.LocationAttributeOne == model.AttributeOne : true)
                                            && ((model.AttributeTwo != null) ? x.LocationAttributeTwo == model.AttributeTwo : true)
                                            && ((model.AttributeThree != null) ? x.LocationAttributeThree == model.AttributeThree : true)
                                            && ((model.AttributeFour != null) ? x.LocationAttributeFour == model.AttributeFour : true)
                                            && ((grave.FkCemetery != model.FkCemeteryUser) ? x.FkCemetery == model.FkCemeteryUser : x.FkCemetery == model.FkCemetery)).FirstOrDefault();

                if (grave == null)
                {
                    grave = db.CemeteryGrave.Add(new CemeteryGrave());

                    grave.TimeStamp = DateTime.Now;
                    grave.LocationAttributeOne = model.AttributeOne;
                    grave.LocationAttributeTwo = model.AttributeTwo;
                    grave.LocationAttributeThree = model.AttributeThree;
                    grave.LocationAttributeFour = model.AttributeFour;
                    grave.FkCemetery = (grave.FkCemetery != model.FkCemeteryUser) ? model.FkCemeteryUser : model.FkCemetery;
                    grave.FkUser = _loggedUser;

                    db.SaveChanges();
                }

                gp.FkGrave = grave.Id;
                model.IdGrave = grave.Id;
                db.SaveChanges();
            }

            if (grave.FkCemetery != model.FkCemeteryUser)
            {
                grave.FkCemetery = model.FkCemeteryUser;
                flagSave = true;
            }

            if (grave.IsReserved != model.IsReserved)
            {
                grave.IsReserved = model.IsReserved;
                flagSave = true;
            }

            if (grave.IsForVerification != model.IsForVerification)
            {
                grave.IsForVerification = model.IsForVerification;
                flagSave = true;
            }

            if (gp.Description != model.Description)
            {
                gp.Description = model.Description;
                flagSave = true;
            }

            if (flagSave)
                db.SaveChanges();
        }

        public static string DeletePhoto(ParafisDBTestoweEntities db, int photoId)
        {
            string result = string.Empty;
            string fileName = string.Empty;

            try
            {
                var p = db.CemeteryGravePhoto.Find(photoId);

                if (p == null)
                    return $"Brak zdjęcia o ID: {photoId}";

                fileName = p.PhotoFile;

                db.CemeteryGravePhoto.Remove(p);

                db.SaveChanges();

                File.Delete(string.Format(@"{0}\{1}", _gravePhotosDir, fileName));

                return result;
            }
            catch (Exception ex)
            {
                return $"Wystąpił wyjątek podczas kasowania zdjęcia: {photoId}. {ex.Message}, {ex.InnerException.Message}";
            }
        }
    }
}