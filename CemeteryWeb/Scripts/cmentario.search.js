
var lastClicked = null;
var lastSingleSelected = null;

//action for Search button (search)
function searchGrave(e) {

	var valName = $("#Name").val();
	var valSurname = $("#Surname").val();
	var valSector = $("#Sector").val();
	var valRow = $("#Row").val();
	var valNumber = $("#Number").val();
	var valYearBirth = $("#YearBirth").val();
	var valYearDeath = $("#YearDeath").val();

	console.log("searchGrave name: " + valName + ", surname: " + valSurname + ", on cemetery: " + configIdCemetery);

	if (valName != "" || valSurname != "" || valSector != "" || valRow != "" || valNumber != "" || valYearBirth != "" || valYearDeath != "")
		$.ajax({
			type: "POST",
			url: '/Search/SearchGrave',
			data: { namePerson: valName, surnamePerson: valSurname, sector: valSector, row: valRow, number: valNumber, yearBirth: valYearBirth, yearDeath: valYearDeath, locLength: configLocParamsLength, cemeteryId: configIdCemetery },
			success: function (data, textStatus, jqXHR) {
				$('#graveInfo').html(data);
			},
			error: function (a, b, c) {
				alert("Wystąpił bład podczas pobierania danych. Proszę skonatkować się ze wsparciem aplikacji")
			}
		});
}

//show individual grave data (search)
function showGraveData(e) {

	console.log("showGraveData: " + e);

	$.ajax({
		type: "POST",
		url: '/Search/ShowGraveData',
		data: { graveDetails: e, locLength: configLocParamsLength, cemeteryId: configIdCemetery },
		success: function (data, textStatus, jqXHR) {
			$('#graveInfo').html(data);
		},
		error: function (a, b, c) {
			alert("Wystąpił bład podczas wyszukiwania danych. Proszę skonatkować się ze wsparciem aplikacji")
		}
	});
}

//unselect after search or show details (search)
function unselectGraves() {

	polygons.every(p => {

		p.setStyle({
			color: colorGraveOutline,
			weight: 1,
			fillColor: colorGraveFill
		});

		return true;
	});

	lastSingleSelected = null;
	lastClicked = null;
}

//show graves after search result (search)
function showGravesOnTheMap(item) {

	console.log("showGravesOnTheMap: " + item.Location);

	polygons.every(p => {

		if (p.options.className == item.Location) {

			p.setStyle({
				color: colorGraveAssigned,
				weight: 2,
				fillColor: colorGraveAssigned
			});

			return false;
		}
		else {

			return true;
		}

	});
}

//action after pokaz button (search)
function showSingleGrave(location) {

	console.log("show grave: " + location);

	if (lastSingleSelected != null)
		lastSingleSelected.setStyle({
			color: colorGraveAssigned,
			weight: 1,
			fillColor: colorGraveAssigned
		});

	var grave = polygons.find(p => p.options.className == location)

	if (grave == null)
		return;

	grave.setStyle({
		color: colorGraveSelected,
		weight: 2,
		fillColor: colorGraveSelected
	});

	lastSingleSelected = grave;
}
