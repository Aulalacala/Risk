/* ************************************************************************************************************** */
// Assign/Risks

//Clasificaciones Buscador

$("select[ref=comboDinamico]").change(function () {
    var opcionSelect = $(this).val();
    var comboAPintar = $(this).attr("sig");
   
    $("select[id=" + comboAPintar + "]").empty();

    $.getJSON("/Assign/recuperaListClasif?idEstructura=" + opcionSelect, function (data) {

        $("select[id=" + comboAPintar + "]").append("<option selected='selected' value='0'></option>");

        $.each(data, function (Key, Value) {
            $("select[id=" + comboAPintar + "]").append("<option value=\"" + Key + "\">" + Value + "</option>");
        });

    });
})



/* ************************************************************************************************************** */
// Risk/RiskFicha

// Menu RiskFicha. Carga dinámica de partialsViews cuando click en Menu



$('a[id^="mnu_"]').click(function () {

    $('#mnu li').removeClass('active');
    $(this).parent().addClass('active');

    $('#mnu li a').attr('aria-expanded', 'false');
    $(this).attr('aria-expanded', 'true');

    var id = $(this).attr("id").replace('mnu_', '');
    var ruta = "http://localhost:1525/Risk/" + id;

    $('#contenidoDinamico').load(ruta, { 'id': $('#IdRiesgo').val() });
})

