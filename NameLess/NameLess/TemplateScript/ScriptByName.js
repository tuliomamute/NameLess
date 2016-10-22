$('input[name="{IdCampoBotao}"]').click(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $.ajax({
                url: "{UrlApi}",
                data: {
                    "TermoPesquisado": $('#{IdCampoTexto}').val(),
                    "DataPesquisa": new Date(),
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
