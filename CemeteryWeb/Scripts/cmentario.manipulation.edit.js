//content should be moved to mapEdit

var shape = [];
var shapeCopy = [];
var singleShape = null;

function drawCustomShape() {

	if (shapeCopy.length == 2) {

		singleShape = L.rectangle(shapeCopy,
				{
					fillColor: colorGraveFill,

				}).addTo(map);
	}

	if (shapeCopy.length > 2) {

		shapeCopy.push(shapeCopy[0]);

		singleShape = L.polyline(shapeCopy,
			{
				fillColor: colorGraveFill,

			}).addTo(map);
	}
}

//function drawCustomPolyline() {

//	shapeCopy.push(shapeCopy[0]);

//	singleShape = L.polyline(shapeCopy,
//		{
//			fillColor: colorGraveFill,

//		}).addTo(map);
//}

function removeCustomShape() {

	if (singleShape != null) {
		singleShape.remove(map);
		singleShape = null;
	}

	$("#mapShapeData").html("");
	shape = [];
	shapeCopy = [];
}

function removeShapeData() {

	$("#mapShapeData").html("");
	shape = [];
	shapeCopy = [];
}

function copyToClipboard() {

	//var copyText = $("#mapShapeData").html();
	//copyText.select();
	//copyText.setSelectionRange(0, 99999);
	//navigator.clipboard
	//	.writeText(copyText.value)
	//	.then(() => {
	//		//alert("successfully copied");
	//	})
	//	.catch(() => {
	//		alert("something went wrong");
	//	});
}

function deleteGrave(idGrave) {

	if (!confirm('Czy na pewno skasować grób?'))
		return

	var link = '/Grave/Delete/' + idGrave;

	window.location.href = link;
}

