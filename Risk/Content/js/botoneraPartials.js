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
    
    alert(datosFormulario['Ejemplo'])
    

    $.ajax({
        url: '/Risk/formGeneral',
        type: 'post',
        data: datosFormulario,
        //data: { 'p': JSON.stringify(json) },
        success: function (data, status) {
            alert('Data  ' + data + ' Status ' + status)},
        error: function (jqXHR, textStatus, errorThrown) {
            alert('ERORRRRRRRRRRRRRRRRR ' + jqXHR + ' ' + textStatus + ' ' + errorThrown)
        }
    })
    

    //$('miForm').submit();
})