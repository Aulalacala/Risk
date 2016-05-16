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
        success: function (data, status) {
            alert('Data  ' + data + ' Status ' + status)
            $('#IdRiesgo').val(data);



                var ruta1 = "http://localhost:1525/Risk/RiskFichaPartialCabecera/";
                $('#partialCabecera').load(ruta1, { "id": $('#IdRiesgo').val() });
                
                var ruta = "http://localhost:1525/Risk/General/";
                $('#contenidoDinamico').load(ruta, { "id": $('#IdRiesgo').val() });
           

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert('ERORRRRRRRRRRRRRRRRR ' + jqXHR + ' ' + textStatus + ' ' + errorThrown)
        }
    })   
})

var urlRedireccion;
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

$('#BtnDiscard').click(function () {   
    var handler = $('#BtnDiscard').attr('handler').split('_')[1];
    alert(handler);
    $('#modalAviso').modal('hide');

    var ruta = "http://localhost:1525/Risk/" + handler;
    $('#contenidoDinamico').load(ruta);
});


