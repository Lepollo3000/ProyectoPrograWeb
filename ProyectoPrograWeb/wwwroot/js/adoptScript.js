$(document).ready(function () {
    $('form').on('submit', function (e) {
        e.preventDefault();
    });

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            let min = parseInt($('#minAge').val(), 10);
            let max = parseInt($('#maxAge').val(), 10);
            let typeAge = $('#typeAge').val();
            let specie = $('#specie').val();

            let ageRow = (data[4] || 0).split(" ");
            let typeAgeRow = ageRow[50];
            let age = parseFloat(ageRow[1]); // use data for the age column
            let kk = (typeAgeRow.includes(typeAge) || !typeAge);

            if ((isNaN(min) && isNaN(max)) ||
                (isNaN(min) && age <= max) ||
                (min <= age && isNaN(max) && (typeAgeRow.includes(typeAge) || !typeAge)) ||
                (min <= age && age <= max) && (typeAgeRow.includes(typeAge) || !typeAge)) {
                return true;
            }
            else if ()
            return false;
        }
    );

    let table = $('#pets').DataTable();

    $('#minAge, #maxAge').change(function () {
        table.draw();
    });

    $('#typeAge').change(function () {
        table.draw();
    });

    $('#specie').change(function () {
        table.draw();
    });

    let htmlSwal = `
    <div class="row mt-2">
        <div class="col-8 mx-auto">
            <ul class="text-justify">
                <li class="mt-1">Your name and email will be taken from your account.</li>
                <li class="mt-1">We are going to receive your message and you'll be notified via email or phone number if accepted or rejected.</li>
                <li class="mt-1">If accepted, we'll try to fix an appointment to meet and interview you.</li>
                <li class="mt-1">Please answer the following form.</li>
            </ul>
        </div>
    </div>
    <div class="row align-content-center">
        <div class="col-10 offset-1">
            <form>
                <div class="form-group">
                    <input id="inputCellphone" name="cellphone" type="text"
                           class="form-control" placeholder="Cellphone (optional)" />
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
    });
});