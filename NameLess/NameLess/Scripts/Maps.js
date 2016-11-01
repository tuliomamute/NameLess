var map;
var map2;

function CarregarPontos(url) {
    var DataInicial = $("#DataInicialPontos").val();
    var DataFinal = $("#DataFinalPontos").val();

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json;",
                data: { 'DataInicial': DataInicial, 'DataFinal': DataFinal, 'Latitude': position.coords.latitude, 'Longitude': position.coords.longitude },
                dataType: "json",
                success: function (data) {

                    var cities = new L.LayerGroup();

                    for (var contador = 0; contador < data.Result.length; contador++) {

                        L.marker([data.Result[contador].Longitude, data.Result[contador].Latitude])
                                .bindPopup("Termo: " + data.Result[contador].TermoPesquisado + "<br/>Data: " + new Date(parseInt(data.Result[contador].DataPesquisa.substr(6))).toUTCString()).addTo(cities);
                    }

                    var mbAttr = 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, ' +
                            '<a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
                            'Imagery © <a href="http://mapbox.com">Mapbox</a>',
                        mbUrl = 'https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw';

                    var grayscale = L.tileLayer(mbUrl, { id: 'mapbox.light', attribution: mbAttr }),
                        streets = L.tileLayer(mbUrl, { id: 'mapbox.streets', attribution: mbAttr });

                    var baseLayers = {
                        "Grayscale": grayscale,
                        "Streets": streets
                    };

                    var overlays = {
                        "Cities": cities
                    };

                    if (map != null) {
                        map.remove();
                    }

                    document.getElementById('mapa1').style.display = 'block';

                    map = L.map('mapa1', {
                        center: [position.coords.latitude, position.coords.longitude],
                        zoom: 10,
                        layers: [grayscale, cities]
                    });

                    L.control.layers(baseLayers, overlays).addTo(map);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                    alert(thrownError);
                }
            });

        }, function (error) {

        });
    }
}

function MapaCalor(url) {
   map2  = L.map('map2').setView([37.8, -96], 4);

    L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw', {
        maxZoom: 18,
        attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors,' +
			'<a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
			'Imagery © <a href="http://mapbox.com">Mapbox</a>',
        id: 'mapbox.light'
    }).addTo(map2);
}