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

$('#BtnCloseModal').click(function () {
    $('#modalBorrado').modal('hide');
    window.location.href = urlRedireccion;
});

$('#BtnCloseModalNuevo').click(function () {
    $('#modalNuevo').modal('hide');
    window.location.href = urlRedireccion;
});

$('#BtnDiscard').click(function () {
    var handler = $('#BtnDiscard').attr('handler').split('_')[1];
    alert(handler);
    $('#modalAviso').modal('hide');

    var ruta = "http://localhost:1525/Risk/" + handler;
    $('#contenidoDinamico').load(ruta);
});

$('#BtnChangeHistorical').click(function () {
    $('#financialHistoricalDiv').show();
    $('#BtnSaveHistorical').removeAttr('disabled');

    var idEvaluacion = $('#financialImpact').attr('idEvaluacion');
    var idRiesgo = $('#idRiesgo').val();

    $('#financialImpactDiv').empty();
    var ruta = "http://localhost:1525/Risk/FinancialImpactCombosHelpers";
    $('#financialImpactDiv').load(ruta, { "id": idRiesgo, "idEvaluacion": idEvaluacion })
});

$('#BtnSaveHistorical').click(function () {
    //alert('voy a salvar esta evaluacion');

    var idEvaluacion = $('#financialImpact').attr('idEvaluacion');
    var idRiesgo = $('#idRiesgo').val();

    var datosEvaluacion = {
        IdRiesgo: $('#idRiesgo').val(),
        IdEvaluacion: $('#financialImpact').attr('idEvaluacion'),
        idEfectividad: $('#efectividad').val(),
        Fecha: $('#fecha').val()
    }

    $('#financialImpact select').each(function (pos, el) {
        var propiedad = $(this).attr('id').replace('Nombre', 'Id');
        var valor = $(this).val();
        datosEvaluacion[propiedad] = valor;
    })

    alert('idEvaluacion=> ' + idEvaluacion + 'idRiesgo => ' + idRiesgo);
    $.ajax({
        url: "/Risk/guardaEvaluacion",
        type: 'post',
        data: { idRiesgo: idRiesgo, idEvaluacion:idEvaluacion, evaluacion: datosEvaluacion },
        success: function (data) {
            alert('ok')

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    })
})


