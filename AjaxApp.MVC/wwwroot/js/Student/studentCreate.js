
loadData();
function loadData() {
    $.ajax({
        url: '/Student/Index',
        type: 'Post',

        success: function (response) {
            $("#tbDataTable").html('');
            for (let i = 0; i < response.Data.length; i++) {
                let item = response.Data[i];
                let row = `
                   <tr>
                <td>
                    <button data-id="${item.Id}" class="btn btn-outline-success">Edit</button>
                    <button data-id="${item.Id}" class="btn btn-delete btn-outline-danger">Delete</button>
                </td>
               button
                <td>${item.Roll_No}</td>
                <td>${item.Name}</td>
            </tr>
                   `;
                $('#tbDataTable').append(row);
            }

            bindDeleteClick();
        },
        error: function (requests, status, error) {
            console.error('Error:', error);
        }
    });
}

function bindDeleteClick() {
    $('.btn-delete').click(function () {
        const id = $(this).data('id');
        if (!confirm("Are you sure you want to delete this student?")) {
            return;
        }

        const item = {
            Id: id
        }

        $.ajax({
            url: `/Student/Delete`,
            type: "POST",
            data: { requestModel: item },
            success: function (response) {
                console.log({ response });
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

$('#btnSave').click(function () {
    const item = {
        Roll_No: $('#rollno').val(),
        Name: $('#name').val()
    }

    $.ajax({
        url: '/Student/Save',
        type: 'Post',
        data: { requestModel: item },
        success: function (response) {
            console.log({ response });
            if (!response.IsSuccess) {
                alert("Error" + response.Message);
                return;
            }
            alert(response.Message);
            window.location.href = "/Student/Index";

            // $("#createModal").modal("hide");

        },
        error: function (requests, status, error) {
            console.error('Error:', error);
        }
    });

});
