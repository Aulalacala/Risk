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

    if ($(this).parent().hasClass('disabled')) {
        //alert('Hay cambios por guardar.');
        $('#modalAviso').modal('show');
        return false;
    }
    else {

        alert('pasando por else');
        $('#mnu li').removeClass('active');
        $(this).parent().addClass('active');

        $('#mnu li a').attr('aria-expanded', 'false');
        $(this).attr('aria-expanded', 'true');

        var pagina = $(this).attr("id").replace('mnu_', '');
        var id = $('#IdRiesgo').val();
        var ruta = "http://localhost:1525/Risk/" + pagina;

        $('#contenidoDinamico').load(ruta, { "id": id });
    }
});


// PartialView GENERAL carga del dropdown dinamico Particle Code al crear nuevo Riesgo

$('#StructureCode').change(function () {
    $.get("/Risk/dameUltimoRiesgoDisponible", { id : $(this).val() }, function (data) {
        $('#IdRiesgo').val() = data;
    })
})

$('select[combo=true]').change(function () {
   var idSeleccionado = $(this).val();
   var color;
   var idCombo = $(this).attr('id');

   $('select[id=' + idCombo + ']').children().each(function (pos, el) {
       if ($(this).attr('selected') == 'selected') {
           $(this).removeAttr('selected');
       }

       if ($(this).val() == idSeleccionado) {
           color = $(this).attr('style').split(':')[2];
           $(this).attr('selected', 'selected');
       }    
   })

    $(this).css('background-color', '');
    $(this).css('background-color', color);
})


    /* ************************************************************************************************************** */
    // Assign/Structure

    // Menu Partial Description. Carga dinámica de partialsViews cuando click en Menu

    //var codigo;

$('a[name="partalView"]').click(function () {
    $('#assignedRisks').show();

    $('a[name="partalView"]').css('color', '#8098a7');
    $(this).css('color', '#64c5da');

    var idEstructura = $(this).attr('id');

    $('#itemSelec').remove();
    $('body').append('<input id="itemSelec" type="hidden" value=' + idEstructura + '>');

    var ruta = "http://localhost:1525/Assign/Description";
    $('#contenidoDinamico').load(ruta, { "id": idEstructura });
    var ruta2 = "http://localhost:1525/Assign/TablaDatos";
    $('#contenidoTabla').load(ruta2, { "id": idEstructura });

})

    $('#buttonMas').click(function () {
        var id = $('#itemSelec').val();
        $('a[id=' + id + ']').siblings().last().find('li').css('display', 'list-item')
    })

    $('#buttonMenos').click(function () {
        var id = $('#itemSelec').val();
        $('a[id=' + id + ']').siblings().last().find('li').css('display', 'none')
    })


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

