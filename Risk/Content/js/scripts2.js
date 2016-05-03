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

    var pagina = $(this).attr("id").replace('mnu_', '');
    var id = $('#IdRiesgo').val();
    var ruta = "http://localhost:1525/Risk/" + pagina;

    $('#contenidoDinamico').load(ruta, {"id" : id});
})


/* ************************************************************************************************************** */
// Assign/Structure

// Menu Partial Description. Carga dinámica de partialsViews cuando click en Menu

//var codigo;

//$('input[name="codigo"]').click(function () {
//    var idEstructura = $(this).attr('id');
//    $('body').append('<input id="IdEstructura" type="hidden" value="' + idEstructura + '" />')
//    var ruta = "http://localhost:1525/Assign/Description";
//    $('#contenidoDinamico').load(ruta, { "id": idEstructura });

//})


$('a[id^="str_"]').click(function () {

    $('#str li').removeClass('active');
    $(this).parent().addClass('active');

    $('#str li a').attr('aria-expanded', 'false');
    $(this).attr('aria-expanded', 'true');

    var pagina = $(this).attr("id").replace('str', '');
    var id = $('#IdEstructura').val();

    var ruta = "http://localhost:1525/Assign/" + pagina;

    $('#contenidoDinamico').load(ruta, { "id": id });
})

