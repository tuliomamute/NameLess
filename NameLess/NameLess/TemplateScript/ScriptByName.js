$('input[name="{IdCampoBotao}"]').click(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $.ajax({
                url: "{UrlApi}",
                data: {
                    "TermoPesquisado": $('#{IdCampoTexto}').val(),
                    "DataPesquisa": "2016-08-18T23:38:54.231Z",
                    "ClienteId": { ClienteId },
                    "TagId": { TagId },
                    "Latitude": -19.934028,
                    "Longitude": -44.199325
                },
                type: "post",
                dataType: "json",
                success: function (data) {

                }
            });

        });
    }
})
