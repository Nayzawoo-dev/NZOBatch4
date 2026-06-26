let currentStudentId = 0;
$(document).ready(function () {
    loadData();
    $('#btnAddNew').click(function () {

        Notiflix.Notify.merge({
            width: '300px',
            position: 'right-top', // ညာဘက်အပေါ်ထောင့်မှာ ပြမယ်
            timeout: 5000, // ၃ စက္ကန့်ကြာရင် ပျောက်သွားမယ်
            showOnlyTheLastOne: true
        });

        AllClear();
        $('#btnSave').text("Save");
        $('#studentModalLabel').text("Add New Student");
    });
});




function loadData() {

    Notiflix.Loading.circle('Loading...');

    $.ajax({
        url: '/Student/Index',
        type: 'POST',
        success: function (response) {
            Notiflix.Loading.remove();
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

            // Click Event များကို ပြန်ပတ်ပေးခြင်း
            bindDeleteClick();
            bindEditClick();
        },
        error: function (requests, status, error) {
            Notiflix.Loading.remove();
            Notiflix.Notify.failure('Data ဆွဲယူရာတွင် အမှားအယွင်းရှိနေပါသည်။');
        }
    });
}

// ၂။ Edit Button နှိပ်လိုက်တဲ့အခါ (Modal ဖွင့်ပြီး Data ဖြည့်ပေးခြင်း)
function bindEditClick() {
    $('.btn-edit').click(function () {
        const id = $(this).data('id');
        const item = { Id: id };

        Notiflix.Loading.circle('Fetching data...');

        $.ajax({
            url: `/Student/Edit`,
            type: "POST",
            data: { requestModel: item },
            success: function (response) {
                Notiflix.Loading.remove();
                if (!response.IsSuccess) {
                    Notiflix.Notify.failure("Error: " + response.Message);
                    return;
                }

                // Controller က ပြန်လာတဲ့ Student Data ကို Form ထဲ ဖြည့်ပေးခြင်း
                // (မှတ်ချက် - response.Data.Id စသည်ဖြင့် သင့် Backend Data Structure အတိုင်း ပြင်ပေးပါ)
                currentStudentId = response.Data.Id;
                $('#rollno').val(response.Data.Roll_No);
                $('#name').val(response.Data.Name);

                // Modal Title ကို Edit Student ဟု ပြောင်းပြီး Modal ကို ဖွင့်ပေးခြင်း
                $('#studentModalLabel').text("Edit Student");
                $('#btnSave').text("Update");
                $('#createModal').modal('show');
            },
            error: function (request, status, error) {
                Notiflix.Loading.remove();
                Notiflix.Notify.failure('Error occurred!');
            }
        });
    });
}

// ၃။ Save/Update Button ကို နှိပ်ခြင်း
$('#btnSave').click(function () {
    // Id ပါရင် Update ဖြစ်ပြီး၊ Id မပါရင် Create ဖြစ်ပါမယ်
    const item = {
        Id: currentStudentId,
        Roll_No: $('#rollno').val(),
        Name: $('#name').val()
    };

    Notiflix.Loading.circle('Saving...');

    $.ajax({
        url: '/Student/Save', // Controller ဘက်က ဒီ Action တစ်ခုထဲနဲ့ Create/Update နှစ်ခုလုံး လက်ခံနိုင်ရပါမယ်
        type: 'POST',
        data: { requestModel: item },
        success: function (response) {
            Notiflix.Loading.remove();
            if (!response.IsSuccess) {
                Notiflix.Notify.failure("Error: " + response.Message); // Failure Alert
                return;
            }

            Notiflix.Notify.success(response.Message); // Success Alert 🎉
            $("#createModal").modal("hide");
            loadData();
        },
        error: function (requests, status, error) {
            Notiflix.Loading.remove();
            Notiflix.Notify.failure('သိမ်းဆည်းရာတွင် အမှားအယွင်းရှိခဲ့သည်။');
        }
    });
});

// ၄။ Delete Button နှိပ်ခြင်း
function bindDeleteClick() {
    $('.btn-delete').click(function () {
        const id = $(this).data('id');

        // Confirm Box ကိုပါ Notiflix ရဲ့ လှပတဲ့ Confirm Box နဲ့ အစားထိုးလိုက်ခြင်း
        Notiflix.Confirm.show(
            'Confirmation',
            'Are you sure you want to delete this student?',
            'Yes',
            'No',
            function okCb() { // 'Yes' နှိပ်ရင် အလုပ်လုပ်မယ့် အပိုင်း
                Notiflix.Loading.circle('Deleting...');
                const item = { Id: id };

                $.ajax({
                    url: `/Student/Delete`,
                    type: "POST",
                    data: { requestModel: item },
                    success: function (response) {
                        Notiflix.Loading.remove();
                        if (!response.IsSuccess) {
                            Notiflix.Notify.failure("Error: " + response.Message);
                            return;
                        }
                        Notiflix.Notify.success(response.Message); // Delete အောင်မြင်ရင် ပြမယ့် Success 🎉
                        loadData();
                    },
                    error: function (request, status, error) {
                        Notiflix.Loading.remove();
                        Notiflix.Notify.failure('Error occurred!');
                    }
                });
            },
            function cancelCb() { // 'No' နှိပ်ရင် ဘာမှမလုပ်ဘူး
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