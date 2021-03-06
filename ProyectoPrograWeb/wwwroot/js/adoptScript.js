﻿$(document).ready(function () {
    $('form').on('submit', function (e) {
        e.preventDefault();
    });

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            let min = parseInt($('#minAge').val(), 10);
            let max = parseInt($('#maxAge').val(), 10);
            let typeAge = $('#typeAge').val();

            let ageRow = (data[5] || 0).split(" ");
            let typeAgeRow = ageRow[3];
            let age = parseFloat(ageRow[1]); // use data for the age column
            let kk = (typeAgeRow.includes(typeAge) || !typeAge);

            if (
                (isNaN(min) && isNaN(max)) ||
                (isNaN(min) && age <= max) ||
                (min <= age && isNaN(max) && (typeAgeRow.includes(typeAge) || !typeAge)) ||
                (min <= age && age <= max) && (typeAgeRow.includes(typeAge) || !typeAge)
            ) {
                return true;
            }

            return false;
        }
    );

    let table = $('#pets').DataTable({
        "sDom": "ltipr"
    });

    $('#minAge, #maxAge').change(function () {
        table.draw();
    });

    $('#typeAge').change(function () {
        table.column(5).search($(this).val()).draw();
    });

    $('#breed').change(function () {
        table.column(3).search($(this).val()).draw();
    });

    $('#namePet').change(function () {
        table.column(2).search($(this).val()).draw();
    });

    $('#statusPet').change(function () {
        table.column(7).search($(this).val()).draw();
    });

    $('#sex').change(function () {
        if ($(this).val() != '')
            table.column(4).search("(^" + $(this).val() + "$)", true, false).draw();
        else
            table.column(4).search("", true, false).draw();
    });


    $('.btnImageAdopt').on('click', function () {
        let idPet = $(this).attr('idPet');

        let htmlSwal = `
    <div class="row mt-2">
        <div class="col-8 mx-auto">
            <ul class="text-justify">
                <li class="mt-1">Your name and email will be taken from your account.</li>
                <li class="mt-1">We are going to receive your message and you'll be notified via email or phone number if accepted or rejected.</li>
                <li class="mt-1">If accepted, we'll try to fix an appointment to meet and interview with you.</li>
                <li class="mt-1">Please answer the following form.</li>
            </ul>
        </div>
    </div>
    <div class="row align-content-center">
        <div class="col-10 offset-1">
            <form id="formAdopt">
                <input id="idPet" type="hidden" value="${ idPet }" />
                <div class="form-group">
                    <input id="inputCellphone" name="cellphone" type="text"
                           class="form-control" onkeypress="return onlyNumberKey(event)" 
                           placeholder="Cellphone (optional)" />
                </div>
                <div class="form-group">
                    <textarea id="inputReason" name="reason" rows="4" class="form-control"
                              placeholder="Why do you wanna adopt?" required></textarea>
                </div>
                <div class="form-group">
                    <textarea id="inputWhereWho" name="whereWho" rows="4" class="form-control"
                              placeholder="Where and with whom it'll live?" required></textarea>
                </div>
                <div class="form-group">
                    <button type="submit" class="btn btn-secondary">Confirm</button>
                </div>
            </form>
        </div>
    </div>
    `;
        Swal.fire({
            title: 'Adopt',
            html: htmlSwal,
            width: '800px',
            showConfirmButton: false,
            showCloseButton: true
        });

        $('#formAdopt').on('submit', function (e) {
            e.preventDefault();

            let inputCellphone = $('#inputCellphone').val();
            let inputWhereWho = $('#inputWhereWho').val();
            let inputReason = $('#inputReason').val();
            let idPet = $('#idPet').val();

            $.ajax({
                url: adoptRequestURL,
                data: {
                    idPet: idPet,
                    cellphone: inputCellphone,
                    whereWho: inputWhereWho,
                    reason: inputReason
                },
                type: 'post',
                datatype: 'json',
                async: false,
                success: function (data) {
                    swalOSuccess(data);

                    $('.pet-' + idPet).remove();
                },
                error: function (data, textStatus, errorThrown) {
                    //VENTANA ERROR
                    swalOError(data.responseJSON);
                }
            });
        });
    });
});

function swalOError(text) {
    Swal.fire({
        icon: 'error',
        title: text,
        showConfirmButton: true,
        showCancelButton: false,
        showCloseButton: true,
    });
}

function swalOSuccess(text) {
    Swal.fire({
        icon: 'success',
        title: text,
        showConfirmButton: true,
        showCancelButton: false,
        showCloseButton: true,
    });
}

function onlyNumberKey(evt) { //FUNCIÓN PARA PONER UNICAMENTE NUMEROS EN EL INPUT

    // Only ASCII charactar in that range allowed 
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}