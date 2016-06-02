$('#miForm [campos="campos"]').on('change', function () {
    var nose = $(this).val()
    //alert($(this).attr("id") + "====>>>> " + $(this).val())

    $(this).addClass("dirty");

    $('#BtnSave').removeClass('btn-primary').addClass('btn-danger');
    $('#Menu li:not([class*="active"])').addClass('disabled');
});



var urlRedireccion;

$('#BtnSave').click(function () {

    var datosFormulario = {
        IdRiesgo: $('#IdRiesgo').val(),
        IdEstructura: $('#idEstructura').val(),
    }

    datosFormulario['IdRiesgo'] = $('#IdRiesgo').val();

    $('#miForm .dirty').each(function (pos, el) {

        var propiedad = $(this).attr('id');
        var valor = $(this).val();

        //datosFormulario.datos[propiedad] = valor;
       datosFormulario[propiedad] = valor;

    })
    //alert(datosFormulario['Ejemplo'])

    $.ajax({
        url: '/Risk/formGeneral',
        type: 'post',
        data: { "datosFormulario" : datosFormulario },
        success: function (data) {
            $('#modalNuevo').modal('show');
            // Redirect a RiskFicha con el idRiesgo nuevo
            urlRedireccion = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert('ERORRRRRRRRRRRRRRRRR ' + jqXHR + ' ' + textStatus + ' ' + errorThrown)
        }
    })
})


$('#BtnDelete').click(function () {
    var idRiesgo = $('#IdRiesgo').val();

    $.ajax({
        url: "/Risk/deleteRiesgo",
        type: 'post',
        data: { id: idRiesgo },
        success: function (data) {
            $('#modalBorrado').modal('show');
            urlRedireccion = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    })
});



/****************************************************************************/
/*...........................MODALS........................................*/

$('#BtnCloseModal').click(function () {
    $('#modalBorrado').modal('hide');
    window.location.href = urlRedireccion;
});


$('#BtnCloseModalNuevo').click(function () {
    $('#modalNuevo').modal('hide');
    window.location.href = urlRedireccion;
});

$('#BtnCloseModalNuevoEva').click(function () {
    $('#modalNuevoEva').modal('hide');

    var ruta = "http://localhost:1525/Risk/Historical/";
    $('#contenidoDinamico').load(ruta, { "id": urlRedireccion});
});

$('#BtnCloseModalEva').click(function () {
    $('#modalBorradoEva').modal('hide');

    var ruta = "http://localhost:1525/Risk/Historical/";
    $('#contenidoDinamico').load(ruta, { "id": urlRedireccion });
});

$('#BtnDiscardModal').click(function () {
    var handler = $('#BtnDiscardModal').attr('handler').split('_')[1];
    var idRiesgo = $('#IdRiesgo').val();

    alert(handler + ' ' + idRiesgo)
    $('#modalAviso').modal('hide');

    $('#mnu li').removeClass('active');
    $("#mnu_" + handler).parent().addClass('active');

    $('#mnu li a').attr('aria-expanded', 'false');
    $("#mnu_" + handler).attr('aria-expanded', 'true');

    var ruta = "http://localhost:1525/Risk/" + handler;
    $('#contenidoDinamico').load(ruta, { "id": idRiesgo });


});

$('#BtnContinueModal').click(function () {
    $('#modalAviso').modal('hide');
});


/****************************************************************************/
/*....................HISTORICAL...........................................*/

$('#BtnNewHistorical').click(function () {
    if ($('#financialHistoricalDiv').attr('style').indexOf('display:none') > -1) {
        $('#financialHistoricalDiv').show();
    } 

    $('#BtnSaveHistorical').removeAttr('disabled');
    var idRiesgo = $('#idRiesgo').val();
    var idEvaluacion = $('#financialImpact').attr('idEvaluacion');

    cargaDivs(idRiesgo, 0, "combos");  
});


$('#BtnChangeHistorical').click(function () {
    $('#financialHistoricalDiv').show();
    $('#BtnSaveHistorical').removeAttr('disabled');
    $('#BtnDeleteHistorical').removeAttr('disabled');

    var idEvaluacion = $('#financialImpact').attr('idEvaluacion');
    var idRiesgo = $('#idRiesgo').val();

    $('#financialImpactDiv').empty();
    $('#ActiveEfectivityDateDiv').empty();

    cargaDivs(idRiesgo, idEvaluacion, "combos");
});

$('#BtnSaveHistorical').click(function () {
    var idEvaluacion = $('#financialImpact').attr('idEvaluacion');
    var idRiesgo = $('#idRiesgo').val();
    var activa;

    if ($('#checkActiva').attr('checked') == 'checked') {
        activa = true;
    }

    var datosEvaluacion = {
        IdRiesgo: idRiesgo,
        IdEvaluacion: idEvaluacion,
        idEfectividad: $('#efectividad').val(),
        Fecha: $('#fecha').val(),
        Activa: activa
    }

    $('#financialImpact select').each(function (pos, el) {
        var propiedad = $(this).attr('id').replace('Nombre', 'Id');
        var valor = $(this).val();
        datosEvaluacion[propiedad] = valor;
    })

   // alert('idEvaluacion=> ' + idEvaluacion + 'idRiesgo => ' + idRiesgo + 'idefect' + datosEvaluacion.idEfectividad);
    $.ajax({
        url: "/Risk/guardaEvaluacion",
        type: 'post',
        data: { idRiesgo: idRiesgo, idEvaluacion: idEvaluacion, evaluacion: datosEvaluacion },
        success: function (data) {
            $('#modalNuevoEva').modal('show');
            //alert(urlRedireccion + ' ' + data)
            urlRedireccion = data;

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    })
});

$('#BtnCopyLast').click(function () {
    if ($('#financialHistoricalDiv').attr('style').indexOf('display:none') > -1) {
        $('#financialHistoricalDiv').show();
    }

    var idRiesgo = $('#idRiesgo').val();
    var idUltimaEvaluacion;

    $.ajax({
        url: "/Risk/recuperaIdUltimaEvaluacion",
        type: 'post',
        data: { id: idRiesgo },
        success: function (data) {
            idUltimaEvaluacion = data;
            cargaDivs(idRiesgo, idUltimaEvaluacion, "textBox")
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    })
});


$('#BtnDeleteHistorical').click(function () {
    var idRiesgo = $('#idRiesgo').val();
    var idEvaluacion;

    $('tbody tr').each(function (pos, el) {
        if ($(this).attr('selected') == 'selected') {
            idEvaluacion = $(this).attr('id');
            alert(idEvaluacion)
        }
    })

    $.ajax({
        url: "/Risk/deleteEvaluacion",
        type: 'post',
        data: { idRiesgo: idRiesgo, idEvaluacion: idEvaluacion },
        success: function (data) {
            if (data == "true") {
                $('#modalBorradoEva').modal('show');
                urlRedireccion = idRiesgo
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    })
});

function cargaDivs(idRiesgo, idEvaluacion, tipo) {
    switch (tipo) {
        case "combos":
            var ruta = "http://localhost:1525/Risk/FinancialImpactCombosHelpers";
            $('#financialImpactDiv').load(ruta, { "id": idRiesgo, "idEvaluacion": idEvaluacion })
            break;
        case "textBox":
            var ruta = "http://localhost:1525/Risk/FinancialImpactTextBox";
            $('#financialImpactDiv').load(ruta, { "id": idRiesgo, "idEvaluacion": idEvaluacion })
            break;
    }
    var ruta2 = "http://localhost:1525/Risk/ActiveEfectivityDate";
    $('#ActiveEfectivityDateDiv').load(ruta2, { "id": idRiesgo, "idEvaluacion": idEvaluacion })
}


