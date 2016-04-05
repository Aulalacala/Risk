/* ************************************************************************************************************** */
// Assign/Risks

//Clasificaciones Buscador

var arrayClasif;
//$.ajaxSetup({ 'async': false });

$("#selectClasif1").change(function () {
    var selectClasif1 = $("#selectClasif1").val();

    $.getJSON("/Assign/recuperaListClasif?idEstructura=" + selectClasif1, function (data) {
        arrayClasif = data;

        $.each(data, function(){
            $("#selectClasif2").append("<option value=\""+ data.IdEstructura +"\">"+ data.Nombre +"</option>");
        });

    });
})