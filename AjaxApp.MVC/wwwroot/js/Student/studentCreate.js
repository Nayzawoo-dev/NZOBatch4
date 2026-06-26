let currentStudentId = 0;
$(document).ready(function () {
    loadData();
    $('#btnAddNew').click(function () {
        AllClear();
        $('#btnSave').text("Save");
        $('#studentModalLabel').text("Add New Student");
    });
});




function loadData() {
    $.ajax({
        url: '/Student/Index',
        type: 'POST',
        success: function (response) {
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
            console.error('Error:', error);
        }
    });
}

// ၂။ Edit Button နှိပ်လိုက်တဲ့အခါ (Modal ဖွင့်ပြီး Data ဖြည့်ပေးခြင်း)
function bindEditClick() {
    $('.btn-edit').click(function () {
        const id = $(this).data('id');
        const item = { Id: id };

        $.ajax({
            url: `/Student/Edit`,
            type: "POST",
            data: { requestModel: item },
            success: function (response) {
                if (!response.IsSuccess) {
                    alert("Error: " + response.Message);
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
                alert(request.responseText);
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

    $.ajax({
        url: '/Student/Save', // Controller ဘက်က ဒီ Action တစ်ခုထဲနဲ့ Create/Update နှစ်ခုလုံး လက်ခံနိုင်ရပါမယ်
        type: 'POST',
        data: { requestModel: item },
        success: function (response) {
            if (!response.IsSuccess) {
                alert("Error: " + response.Message);
                return;
            }

            alert(response.Message);

            // Modal ကို ပိတ်မယ်
            $("#createModal").modal("hide");
            AllClear();
            // Page Refresh မလုပ်ဘဲ Table Data ကိုပဲ Update ဖြစ်အောင် loadData() ကို ပြန်ခေါ်မယ်
            loadData();
        },
        error: function (requests, status, error) {
            console.error('Error:', error);
        }
    });
});

// ၄။ Delete Button နှိပ်ခြင်း
function bindDeleteClick() {
    $('.btn-delete').click(function () {
        const id = $(this).data('id');
        if (!confirm("Are you sure you want to delete this student?")) {
            return;
        }

        const item = { Id: id };

        $.ajax({
            url: `/Student/Delete`,
            type: "POST",
            data: { requestModel: item },
            success: function (response) {
                if (!response.IsSuccess) {
                    alert("Error: " + response.Message);
                    return;
                }
                alert(response.Message);
                loadData();
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
    });
}
function AllClear() {
    currentStudentId = 0;
    $('#rollno').val('');
    $('#name').val('');
}