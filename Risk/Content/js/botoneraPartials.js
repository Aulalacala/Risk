$('#miForm [campos="campos"]').on('change', function () {
    var nose = $(this).val()
    //alert($(this).attr("id") + "====>>>> " + $(this).val())

        $(this).addClass("dirty");

    $('#BtnSave').removeClass('btn-primary').addClass('btn-danger');
    $('#Menu li:not([class*="active"])').addClass('disabled');
});

$('#BtnSave').click(function () {
    var json = "{";

    $('#miForm .dirty').each(function (pos, el) {

        json += "'" + el.id + "' : '" + el.value + "',"

    });

    json = json.substring(0, json.length - 1);
    json += "}";

    alert(json);

    $.ajax({
        url: '/Risk/formGeneral',
        type: 'post',
        data: { 'p': json },
        success : function (data, status) {
            alert('OKKKKKKKKKKKKKKKKKKKK' + status)},
        error: function (jqXHR, textStatus, errorThrown) {
            alert('ERORRRRRRRRRRRRRRRRR ' + jqXHR + ' ' + textStatus + ' ' + errorThrown)
        }
    })
    

    //$('miForm').submit();
})