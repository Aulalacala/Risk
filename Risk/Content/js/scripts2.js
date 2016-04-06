/* ************************************************************************************************************** */
// Assign/Risks

//Clasificaciones Buscador

$("select[name=comboDinamico]").change(function () {
    var opcionSelect = $(this).val();
    var numeroCombo = parseInt($(this).attr("id").split("_")[1]) + 1;

    $("select[id=selectClasif_" + numeroCombo + "]").empty();

    $.getJSON("/Assign/recuperaListClasif?idEstructura=" + opcionSelect, function (data) {

        $.each(data, function (Key, Value) {
            $("select[id=selectClasif_" +numeroCombo+"]").append("<option value=\"" + Key + "\">" + Value + "</option>");
        });

    });
})