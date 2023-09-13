//read shape for Edit (init)
//additional parameter
function readPolygon() {

	$.ajax({
		type: "POST",
		url: '/Map/PolygonSingle_Read',
		dataType: 'json',
		data: { locLength: configLocParamsLength, sector: configGraveSector, row: configGraveRow, number: configGraveNumber, cemeteryId: configIdCemetery },
		success: function (data, textStatus, jqXHR) {

			if (data == null) {
				return
			}

			if (data.length == 0) {
				console.log("readPolygon: PolygonSingle_Read zwrocil pusty obrys");
				return;
			}

			if (polygons.length != 0) {
				polygons.forEach((p) => { p.remove(map) });
			}

			polygons = [];

			data.forEach(addPolygon);

			map.setView([data[0].Points[0].X, data[0].Points[0].Y], configZoomLevel + 1);
		},
		error: function (a, b, c) {
			console.log("readPolygon: Wystąpił bład podczas zaczytywania pojedynczego kształtu. Proszę skontaktować się ze wsparciem aplikacji")
		}
	});
}

//part of initial shapes read (init)
function addPolygon(item) {

	if (item.Points.length == 2) {

		polygons.push(new L.rectangle([[item.Points[0].X, item.Points[0].Y], [item.Points[1].X, item.Points[1].Y]],
			{
				className: item.Name,
				color: colorGraveOutline,
				weight: 2,
				fillColor: colorGraveFill,
				fillOpacity: 0.7
			}).addTo(map));
	}

	else if (item.Points.length > 2) {

		var polyShape = [];

		item.Points.forEach((p) => { polyShape.push([p.X, p.Y]) });
		polyShape.push([item.Points[0].X, item.Points[0].Y]);	//closing shape

		polygons.push(new L.polyline(polyShape,
			{
				className: item.Name,
				color: colorGravePolyOutline,
				weight: 2,
				fillColor: colorGravePolyFill
				//fillOpacity: 0.7
			}).addTo(map));
	}

	if ($("#mapGraveShapeData").length) {

		$("#mapGraveShapeData").html("");

		item.Points.forEach((p) => {
			$("#mapGraveShapeData").append(p.X + "," + p.Y + "; ")
		});
	}
}

var map = L.map('map').setView(configSetViewCords, configZoomLevel);

var prevZoom = map.getZoom();

var latLngBounds = L.latLngBounds(configLatLngBounds);

var polygons = [];

//var lastClicked = null;
//var lastSingleSelected = null;

var colorGraveShape = '#888';
var colorGraveSelected = 'green';
var colorGraveAssigned = 'red';
var colorGraveGray = 'yellow';
var colorGraveOutline = 'yellow';
var colorGraveFill = 'silver';	//'#888'
var colorGravePolyOutline = 'yellow'; // '#00f';
var colorGravePolyFill = 'yellow';


var imageOverlay = L.imageOverlay('/Images/' + configLayerPic, latLngBounds, {
	opacity: 0.8,
	interactive: true
});

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
	maxZoom: configMaxZoom,
	attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

imageOverlay.addTo(map);