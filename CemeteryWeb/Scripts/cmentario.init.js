//read all shapes (init)
function readPolygon() {

	console.log("readPolygon:");

	$.ajax({
		type: "POST",
		url: '/Map/Polygon_Read',
		dataType: 'json',
		data: { locLength: configLocParamsLength, cemeteryId: configIdCemetery },
		success: function (data, textStatus, jqXHR) {

			if (data == null) {
				return
			}

			console.log("readPolygon: done");

			polygons = [];

			data.forEach(addPolygon);

			//if (document.getElementById('cb-map-showshapes').checked == true) {
			//	polygons.forEach((p) => { p.addTo(map) });
			//}

			polygons.forEach((p) => { p.addTo(map) });
		},
		error: function (a, b, c) {
			//alert("Wystąpił bład podczas zaczytywania kształtów. Proszę skontaktować się ze wsparciem aplikacji")
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
				weight: 1,
				fillColor: colorGraveFill,
				fillOpacity: 0.5
			}));
			//.addTo(map));
	}

	else if (item.Points.length > 2) {

		var polyShape = [];

		item.Points.forEach((p) => { polyShape.push([p.X, p.Y]) });
		polyShape.push([item.Points[0].X, item.Points[0].Y]);	//closing shape

		polygons.push(new L.polyline(polyShape,
			{
				className: item.Name,
				color: colorGravePolyOutline,
				weight: 1,
				fillColor: colorGraveFill,
				fillOpacity: 0.5
			}));
		//.addTo(map));
	}
}

var map = L.map('map').setView(configSetViewCords, configZoomLevel);

var prevZoom = map.getZoom();

//var latLngBounds = L.latLngBounds([[52.814274, 15.658989], [52.813815, 15.660059]]);
var latLngBounds = L.latLngBounds(configLatLngBounds);

var polygons = [];

var colorGraveShape = '#888';
var colorGraveSelected = 'green';
var colorGraveAssigned = 'red';
var colorGraveGray = 'yellow';
var colorGraveOutline = 'yellow';
var colorGravePolyOutline = 'yellow';
var colorGraveFill = 'silver';	//'#888'

var imageOverlay = L.imageOverlay('/Images/' + configLayerPic, latLngBounds, {
	opacity: 0.8,
	interactive: true
});

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
	maxZoom: configMaxZoom,
	attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

imageOverlay.addTo(map);

$("#cb-map-showshapes").click(function (e) {

	console.log("showShapes:" + this.checked);

	if (polygons == null)
		return;

	if (this.checked)
		polygons.forEach((p) => { p.addTo(map) });
	else
		polygons.forEach((p) => { p.remove(map) });
});