$(document).ready(function () {
    let table = $('#adoptions').DataTable();

    $('.acceptAdoption').on('click', function () {
        let idPet = $(this).attr('idPet');
        let namePet = $(this).attr('namePet');
        let idRequest = $(this).attr('idRequest');

        Swal.fire({
            icon: 'warning',
            title: 'Confirm Adoption',
            text: 'Are you sure you want to confirm ' + namePet + '\'s adoption?',
            showConfirmButton: true,
            showCancelButton: false,
            showCloseButton: true,
            confirmButtonText: 'Sure',
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: acceptAdoption,
                    data: {
                        idAdoptionRequest: idRequest
                    },
                    type: 'post',
                    datatype: 'json',
                    async: false,
                    success: function (data) {
                        swalOSuccess(data);
                    },
                    error: function (data, textStatus, errorThrown) {
                        //VENTANA ERROR
                        swalOError(data.responseJSON);
                    }
                });
            }
        });;
    });

    $('.cancelAdoption').on('click', function () {
        let idPet = $(this).attr('idPet');
        let namePet = $(this).attr('namePet');
        let idRequest = $(this).attr('idRequest');

        Swal.fire({
            icon: 'warning',
            title: 'Cancel Adoption',
            text: 'Are you sure you want to cancel ' + namePet + '\'s adoption?',
            showConfirmButton: true,
            showCancelButton: false,
            showCloseButton: true,
            confirmButtonText: 'Sure',
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: cancelAdoption,
                    data: {
                        idAdoptionRequest: idRequest
                    },
                    type: 'post',
                    datatype: 'json',
                    async: false,
                    success: function (data) {
                        swalOSuccess(data);

                        $('.request-' + idRequest).remove();
                    },
                    error: function (data, textStatus, errorThrown) {
                        //VENTANA ERROR
                        swalOError(data.responseJSON);
                    }
                });
            }
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