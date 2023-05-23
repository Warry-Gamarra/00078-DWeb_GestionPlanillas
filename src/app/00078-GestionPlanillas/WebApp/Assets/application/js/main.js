$(function () {
    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
    });

    $('body').on('click', '.modal-link-lg', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container-lg');
        $(this).attr('data-toggle', 'modal');
    });

    $('body').on('click', '.modal-link-xl', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container-xl');
        $(this).attr('data-toggle', 'modal');
    });

    $('#modal-container').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var url = button.attr("href");
        var modal = $(this);
        modal.find('.modal-content').load(url);
    });

    $('#modal-container-lg').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var url = button.attr("href");
        var modal = $(this);
        modal.find('.modal-content').load(url);
    });

    $('#modal-container-xl').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var url = button.attr("href");
        var modal = $(this);
        modal.find('.modal-content').load(url);
    });

    $('#modal-container').on('hide.bs.modal', function () {
        $(this).removeData('bs.modal');
        $('#modal-container .modal-content').html('<p style="margin:50px;" class="text-center text-muted"><i class="fa fa-spin fa-3x fa-cog"></i></p>');
    });

    $('#modal-container-lg').on('hide.bs.modal', function () {
        $(this).removeData('bs.modal');
        $('#modal-container-lg .modal-content').html('<p style="margin:50px;" class="text-center text-muted"><i class="fa fa-spin fa-3x fa-cog"></i></p>');
    });

    $('#modal-container-xl').on('hide.bs.modal', function () {
        $(this).removeData('bs.modal');
        $('#modal-container-xl .modal-content').html('<p style="margin:50px;" class="text-center text-muted"><i class="fa fa-spin fa-3x fa-cog"></i></p>');
    });

    $('[data-toggle="tooltip"]').tooltip();
});

//$(document).on('ready', function () {
//    $('#btnsubmit').removeClass('disabled');
//    $('#btnCancel').removeClass('disabled');
//});

function Load() {
    $('#loading').show();
    $('#btnsubmit').addClass('disabled');
    $('#btnCancel').addClass('disabled');
    
}

function Stop() {
    $('#loading').hide();
    $('#btnsubmit').removeClass('disabled');
    $('#btnCancel').removeClass('disabled');
}

//function Submited() {
//    var submited = parseInt($("#submited").val());
//    $("#submited").val(submited + 1);
//}

//function Begin() {
//    $('#loading').show();
//    $('#div-resultado').html('');
//    $('#div-resultado').hide();
//}

//function onComplete() {
//    $('#loading').hide();
//    $('#div-resultado').show();
//}

function FormatearNumero(number, decimals = 2, round = true) {
    if (isNaN(number) || number.lenght == 0) number = 0;

    if (round) {
        return parseFloat(number).toFixed(decimals);
    }
    else {
        let arrStr = number.split('.');
        if (decimals > 0 && arrStr.lenght > 1) {
            number = arrStr[0] + '.' + arrStr[1].substring(0, decimals);
            return parseFloat(number);
        }
        else {
            return parseFloat(arrStr[0]);
        }
    }
}

function htmlDecode(input) {
    var element = document.createElement('textarea');
    element.innerHTML = input;
    return element.value;
}

function ChangeStateReloadPage(rowID, estaHabilitado, actionName, token) {
    var parametros = {
        rowID: rowID,
        estaHabilitado: estaHabilitado,
        __RequestVerificationToken: token
    };
    $.ajax({
        cache: false,
        url: actionName,
        type: "POST",
        data: parametros,
        dataType: "json",
        beforeSend: function () {
            $('#loader' + rowID).css("display", "inline");
        },
        success: function (data) {
            $('#loader' + rowID).css("display", "none");
            if (data['Success']) {
                $('#td' + rowID).html(data['Message']);

                location.reload();
            }
            else {
                toastr.warning(data['Message']);
            }
        },
        error: function () {
            $('#loader' + rowID).css("display", "none");

            toastr.error("No se pudo actualizar el estado. Intente nuevamente en unos segundos.<br /> Si el problema persiste comuníquese con el área de soporte de la aplicación.");
        }
    });
}

function ChangeState(rowID, estaHabilitado, actionName, token) {
    var parametros = {
        rowID: rowID,
        estaHabilitado: estaHabilitado,
        __RequestVerificationToken: token
    };
    $.ajax({
        cache: false,
        url: actionName,
        type: "POST",
        data: parametros,
        dataType: "json",
        beforeSend: function () {
            $('#loader' + rowID).css("display", "inline");
        },
        success: function (data) {
            $('#loader' + rowID).css("display", "none");
            if (data['Success']) {
                if (estaHabilitado) {
                    $('#td' + rowID).html(`<button type="submit" class="btn btn-xs btn-secondary" onclick="ChangeState(${rowID}, false, '${actionName }');"><i class="fa fa-minus-circle">&nbsp;</i><span class="d-none d-md-inline-block">Deshabilitado</span></button>`);
                }
                else {
                    $('#td' + rowID).html(`<button type="submit" class="btn btn-xs btn-success" onclick="ChangeState(${rowID}, true, '${actionName }');"><i class="fa fa-check-circle">&nbsp;</i><span class="d-none d-md-inline-block">Habilitado</span></button>`);
                }

                toastr.success(data['Message']);
            }
            else {
                toastr.warning(data['Message']);
            }
        },
        error: function () {
            $('#loader' + RowID).css("display", "none");

            toastr.error("No se pudo actualizar el estado. Intente nuevamente en unos segundos.<br /> Si el problema persiste comuníquese con el área de soporte de la aplicación.");
        }
    });
}

function mostrarMensajeSistema(mensaje, icon, recargarPagina = null) {
    Swal.fire({
        title: '',
        text: mensaje,
        icon: icon,
        timer: 6000,
        confirmButtonColor: '#0069d9',

    }).then((result) => {
        if (result.isConfirmed && recargarPagina !== undefined && recargarPagina !== null) {
            recargarPagina();
        }
    });
}

function recargarPagina() {
    location.reload();
}

function CargarMeses(cmbAnio, cmbMeses, url) {
    let anio = cmbAnio.value;

    let parametros = {
        I_Anio: anio
    };

    $.ajax({
        type: 'GET',
        url: url,
        data: parametros,
        dataType: 'json',
        beforeSend: function () {
        },
        success: function (response) {
            if (response.Success) {
                cmbMeses.html("");

                let html = "";

                $.each(response.Result, function (i, item) {
                    html += '<option value="' + item.Value + '">' + item.Text + '</option>'
                });

                cmbMeses.html(html);
            } else {
                mostrarMensajeSistema(response.Message, MENSAJE.ERROR);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mostrarMensajeSistema(thrownError, MENSAJE.ERROR);
        }
    });
}
