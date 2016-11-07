var map;

function CarregarPontos(url) {
    var DataInicial = $("#DataInicialPontos").val();
    var DataFinal = $("#DataFinalPontos").val();

    if (DataInicial == "") {
        alert("A Data Inicial, para o 'Mapa de Pontos' deve ser informada!  ");
        return;
    }

    if (DataFinal == "") {
        alert("A Data Final, para o 'Mapa de Pontos' deve ser informada!");
        return;
    }

    document.getElementById('atualizarPontos').style.display = 'inline-block';

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
                    document.getElementById('atualizarPontos').style.display = 'none';

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                    document.getElementById('atualizarPontos').style.display = 'none';

                }
            });

        }, function (error) {
            document.getElementById('atualizarPontos').style.display = 'none';
        });
    }


}

function MapaCalor(url) {
    var DataInicial = $("#DataInicialCalor").val();
    var DataFinal = $("#DataFinalCalor").val();

    if (DataInicial == "") {
        alert("A Data Inicial, para o 'Mapa de Calor' deve ser informada!");
        return;
    }

    if (DataFinal == "") {
        alert("A Data Final, para o 'Mapa de Calor' deve ser informada!");
        return;
    }

    document.getElementById('atualizarMapaCalor').style.display = 'inline-block';

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json;",
        data: { 'DataInicial': DataInicial, 'DataFinal': DataFinal },
        dataType: "json",
        success: function (data) {

            var data2 = [];

            for (var contador = 0; contador < data.Result.length; contador++) {
                data2.push({ "hc-key": data.Result[contador].SiglaEstado, "value": data.Result[contador].Count })
            }
            document.getElementById('mapa2').style.display = 'block';

            // Initiate the chart
            $('#mapa2').highcharts('Map', {

                title: {
                    text: 'Pesquisas por estado'
                },

                legend: {
                    layout: 'vertical',
                    align: 'left',
                    verticalAlign: 'bottom'
                },

                mapNavigation: {
                    enabled: true,
                    buttonOptions: {
                        verticalAlign: 'top'
                    }
                },

                colorAxis: {
                    min: 0
                },

                series: [{
                    data: data2,
                    mapData: Highcharts.maps['countries/br/br-all'],
                    joinBy: 'hc-key',
                    name: 'Pesquisas Efetuadas',
                    states: {
                        hover: {
                            color: '#BADA55'
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    }
                }]
            });
            document.getElementById('atualizarMapaCalor').style.display = 'none';
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
            document.getElementById('atualizarPontos').style.display = 'none';

        }
    })
}