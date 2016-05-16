$('#miForm [campos="campos"]').on('change', function () {
    var nose = $(this).val()
    //alert($(this).attr("id") + "====>>>> " + $(this).val())

        $(this).addClass("dirty");

    $('#BtnSave').removeClass('btn-primary').addClass('btn-danger');
    $('#Menu li:not([class*="active"])').addClass('disabled');
});

$('#BtnSave').click(function () {

    var datosFormulario = {}

    datosFormulario['IdRiesgo'] = $('#IdRiesgo').val();


    $('#miForm .dirty').each(function (pos, el) {

        var propiedad = $(this).attr('id');
        var valor = $(this).val() + ":" + $(this).attr('tabla');

        datosFormulario[propiedad] = valor;
      
    })    
    //alert(datosFormulario['Ejemplo'])
    
    $.ajax({
        url: '/Risk/formGeneral',
        type: 'post',
        data: datosFormulario,
        success: function (data) {
            // Redirect a RiskFicha con el idRiesgo nuevo
            window.location.href = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert('ERORRRRRRRRRRRRRRRRR ' + jqXHR + ' ' + textStatus + ' ' + errorThrown)
        }
    })
    

    //$('miForm').submit();
})