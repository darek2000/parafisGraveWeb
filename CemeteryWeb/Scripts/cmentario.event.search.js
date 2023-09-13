map.on('click', function (e) {

	//TODO console.log

	polygons.every(p => {

		if (p.getBounds().contains(e.latlng) == true) {
			if (lastClicked != null) {
				lastClicked.setStyle({ fillColor: colorGraveFill });
			}
			lastClicked = p;
			p.setStyle({ fillColor: colorGraveSelected });
			showGraveData(p.options.className);
			return false;
		}
		else {
			p.setStyle({ fillColor: colorGraveFill });
			return true;
		}
	});
});

//part of event
map.on('zoomend', function (e) {

	if (!$("#cb-map-bright").is(":checked"))
		return;

	var currZoom = map.getZoom();
	var diff = prevZoom - currZoom;
	if (currZoom >= 20) {

		imageOverlay.setStyle({ opacity: 0.4 });

		if (polygons.length == 0) {
			//readPolygon();
		}
	}

	if (currZoom <= 19) {

		imageOverlay.setStyle({ opacity: 0.8 });
	}

	prevZoom = currZoom;
});