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

//Carga Tabla de riesgos según parámetros de búsqueda

//$("select").change(function () {
//    var selects = [];
//    var query = "riesgosBD.qRiesgosNombres.Where(r=>";

//    $("select").each(function () {
//        var valorParam = $(this).val();
//        var columTablaParam = $(this).attr("filtro");

//        valorParam != 0 ? query += "r."+ columTablaParam +"="+valorParam: query = query;
//        selects.push($(this).val());
//    })


//    $.post("/Assign/BusquedaRiks", { 'query': query }, function (data) {
//    },"json");
//})
