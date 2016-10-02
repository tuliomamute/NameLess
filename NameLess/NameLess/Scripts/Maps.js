function CarregarPontos(url) {
    var DataInicial = $("#DataInicialPontos").val();
    var DataFinal = $("#DataFinalPontos").val();

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json;",
        data: { 'DataInicial': DataInicial, 'DataFinal': DataFinal },
        dataType: "json",
        success: function (data) {

            var cities = new L.LayerGroup();

            for (var contador = 0; contador < data.Result.length; contador++) {

                L.marker([data.Result[contador].Longitude, data.Result[contador].Latitude])
                        .bindPopup(data.Result[contador].TermoPesquisado).addTo(cities);
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

            var map = L.map('mapa1', {
                center: [-19.955674, -44.199455],
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


}

