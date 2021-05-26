$(document).ready(function () {
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

    $('.btnImageAdopt').on('click', function () {
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

                    $('.petStatus').html('In adoption process');
                },
                error: function (data, textStatus, errorThrown) {
                    //VENTANA ERROR
                    swalOError(data.responseJSON);
                }
            });
        });
    });
    /*
    $('#editForm').on('submit', function (e) {
        e.preventDefault();

        let idPet = $('#idPet').val();
        let namePet = $('#name').val();
        let sex = $('#sexPet').val();
        let weight = $('#weight').val();
        let isMonth = $('#isMonth').val();
        let energyLevel = $('#energyLevel').val();
        let age = $('#age').val();
        let breed = $('#breed').val();

        var file_data = $('#Photo').prop('files')[0];
        var form_data = new FormData($(this));
        form_data.append('file', file_data);

        alert(form_data); 

        $.ajax({
            url: saveChangesURL,
            data: {
                Photo: file_data,
                idPet: idPet,
                namePet: namePet,
                idSex: sex,
                weight: weight,
                isMonth: isMonth,
                energyLevel: energyLevel,
                age: age,
                breed: breed
            },
            type: 'post',
            datatype: 'text',
            async: false,
            processData: false,
            success: function (data) {
                swalOSuccess(data);

                $('.namePet').html(namePet);

                $('#btnEdit').addClass('d-none');
                $('#namePet').addClass('d-none');

                $('#breed').prop("disabled", true);
                $('#Photo').prop("disabled", true);
                $('#description').prop("disabled", true);
                $('#sexPet').prop("disabled", true);
                $('#age').prop("disabled", true);
                $('#weight').prop("disabled", true);
                $('#isMonth').prop("disabled", true);
                $('#energyLevel').prop("disabled", true);
            },
            error: function (data, textStatus, errorThrown) {
                //VENTANA ERROR
                swalOError(data.responseJSON);
            }
        });
    });*/

    $('#editPet').on('click', function () {
        $('#btnEdit').removeClass('d-none');
        $('#namePet').removeClass('d-none');

        $('#breed').prop("disabled", false);
        $('#Photo').prop("disabled", false);
        $('#description').prop("disabled", false);
        $('#sexPet').prop("disabled", false);
        $('#age').prop("disabled", false);
        $('#weight').prop("disabled", false);
        $('#isMonth').prop("disabled", false);
        $('#energyLevel').prop("disabled", false);
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