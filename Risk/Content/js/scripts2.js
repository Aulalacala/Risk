/* ************************************************************************************************************** */
// Assign/Risks

//Clasificaciones Buscador

$("select[ref=comboDinamico]").change(function () {
    var opcionSelect = $(this).val();
    var comboAPintar = $(this).attr("sig");
   
    $("select[id=" + comboAPintar + "]").empty();

    $.getJSON("/Assign/recuperaListClasif?idEstructura=" + opcionSelect, function (data) {

        $.each(data, function (Key, Value) {
            $("select[id=" + comboAPintar + "]").append("<option value=\"" + Key + "\">" + Value + "</option>");
        });

    });
})

//Carga Tabla de riesgos según parámetros de búsqueda

$("select").change(function () {
    var selects = [];


    $("select").each(function () {
        selects.push($(this).val());
    })


    $.post("/Assign/BusquedaRiks", { 'seleccionados[]': selects }, function (data) {


    },"json");


    //$.getJSON("/Assign/BusquedaRiks?seleccionados=" + selects, function (data) {

    //})


})