let currentStudentId = 0;
loadData();
$(document).ready(function () {


    $('#btnAddNew').click(function () {
        Notiflix.Notify.init({
            width: '300px',
            position: 'right-top', // ညာဘက်အပေါ်ထောင့်မှာ ပြမယ်
            timeout: 5000,         // ၅ စက္ကန့်ကြာရင် ပျောက်သွားမယ်
            showOnlyTheLastOne: true
        });

        AllClear();
        $('#btnSave').text("Save");
        $('#studentModalLabel').text("Add New Student");
    });
});

// ၁။ Data ဆွဲယူခြင်း (Table ပေါ်တင်ပဲ Loading ပြအောင် ပြင်ထားပါတယ်)
function loadData() {
    Notiflix.Block.hourglass('#myTable', 'Loading...');

    $.ajax({
        url: '/Student/Index',
        type: 'POST',
        success: function (response) {
            // Block ကို ပြန်ဖြုတ်မယ်
            Notiflix.Block.remove('#myTable');

            $("#tbDataTable").html('');
            for (let i = 0; i < response.Data.length; i++) {
                let item = response.Data[i];
                let row = `
                <tr>
                    <td>
                        <button data-id="${item.Id}" class="btn btn-edit btn-outline-success">Edit</button>
                        <button data-id="${item.Id}" class="btn btn-delete btn-outline-danger">Delete</button>
                    </td>
                    <td>${item.Roll_No}</td>
                    <td>${item.Name}</td>
                </tr>`;
                $('#tbDataTable').append(row);
            }


            bindDeleteClick();
            bindEditClick();
            let table = new DataTable('#myTable');
        },
        error: function (requests, status, error) {
            Notiflix.Block.remove('#tbDataTable');
            Notiflix.Notify.failure('Data ဆွဲယူရာတွင် အမှားအယွင်းရှိနေပါသည်။');
        }
    });
}

// ၂။ Edit Button နှိပ်လိုက်တဲ့အခါ
function bindEditClick() {
    $('.btn-edit').click(function () {
        const id = $(this).data('id');
        const item = { Id: id };

        // 💡 ပြင်ဆင်ချက်: Edit နှိပ်ရင်လည်း Page အပြည့်မဖြစ်အောင် Table ကိုပဲ Block ပြပါမယ်
        Notiflix.Block.hourglass('#tbDataTable', 'Fetching data...');

        $.ajax({
            url: `/Student/Edit`,
            type: "POST",
            data: { requestModel: item },
            success: function (response) {
                Notiflix.Block.remove('#tbDataTable');

                if (!response.IsSuccess) {
                    Notiflix.Notify.failure("Error: " + response.Message);
                    return;
                }

                currentStudentId = response.Data.Id;
                $('#rollno').val(response.Data.Roll_No);
                $('#name').val(response.Data.Name);

                $('#studentModalLabel').text("Edit Student");
                $('#btnSave').text("Update");
                $('#createModal').modal('show');
            },
            error: function (request, status, error) {
                Notiflix.Block.remove('#tbDataTable');
                Notiflix.Notify.failure('Error occurred!');
            }
        });
    });
}

// ၃။ Save/Update Button ကို နှိပ်ခြင်း
$('#btnSave').click(function () {
    const item = {
        Id: currentStudentId,
        Roll_No: $('#rollno').val(),
        Name: $('#name').val()
    };

    // Modal Content ကိုပဲ လှပစွာ Block ထားမယ်
    Notiflix.Block.hourglass('#createModal .modal-body', 'Saving...');

    $.ajax({
        url: '/Student/Save',
        type: 'POST',
        data: { requestModel: item },
        success: function (response) {
            Notiflix.Block.remove('#createModal .modal-body');

            if (!response.IsSuccess) {
                Notiflix.Notify.failure("Error: " + response.Message);
                return;
            }

            Notiflix.Notify.success(response.Message);
            $("#createModal").modal("hide");


            loadData();
        },
        error: function (requests, status, error) {
            Notiflix.Block.remove('#createModal .modal-body');
            Notiflix.Notify.failure('သိမ်းဆည်းရာတွင် အမှားအယွင်းရှိခဲ့သည်။');
        }
    });
});

// ၄။ Delete Button နှိပ်ခြင်း
function bindDeleteClick() {
    $('.btn-delete').click(function () {
        const id = $(this).data('id');

        Notiflix.Confirm.show(
            'Confirmation',
            'Are you sure you want to delete this student?',
            'Yes',
            'No',
            function okCb() {
                // 💡 ပြင်ဆင်ချက်: Delete လုပ်ချိန်မှာလည်း Table နေရာလေးတင် Block လုပ်ပါမယ်
                Notiflix.Block.hourglass('#myTable', 'Deleting...');
                const item = { Id: id };

                $.ajax({
                    url: `/Student/Delete`,
                    type: "POST",
                    data: { requestModel: item },
                    success: function (response) {
                        Notiflix.Block.remove('#myTable');
                        if (!response.IsSuccess) {
                            Notiflix.Notify.failure("Error: " + response.Message);
                            return;
                        }
                        Notiflix.Notify.success(response.Message);
                        loadData();
                    },
                    error: function (request, status, error) {
                        Notiflix.Block.remove('#myTable');
                        Notiflix.Notify.failure('Error occurred!');
                    }
                });
            },
            function cancelCb() {
                return;
            }
        );
    });
}

function AllClear() {
    currentStudentId = 0;
    $('#rollno').val('');
    $('#name').val('');
}

function datePicker() {
    $('.date-picker').datepicker({
        format: 'dd-mm-yyyy',
        autoHide: true
    });
}