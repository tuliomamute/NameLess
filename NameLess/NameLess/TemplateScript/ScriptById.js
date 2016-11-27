$('#{IdCampoBotao}').click(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $.ajax({
                url: "{UrlApi}",
                data: {
                    "TermoPesquisado": $('#{IdCampoTexto}').val(),
                    "DataPesquisa": new Date(),
                    "ClienteId": { ClienteId },
                    "TagId": { TagId },
                    "Latitude": position.coords.latitude,
                    "Longitude": position.coords.longitude
                },
                type: "post",
                dataType: "json",
                success: function (data) {

                }
            });

        });
    }
})
