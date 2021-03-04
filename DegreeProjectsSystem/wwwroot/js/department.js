var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Department/GetAllDepartments"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "active",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `

                    div class="text-center">
                        <a href="Admin/Department/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                            Editar
                            <i class="far fa-edit p-1"></i>
                        </a>
                        <a onclick=Delete("/Admin/Categoria/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                            Eliminar
                            <i class="far fa-trash-alt p-1"></i>
                        </a>
                    </div>
                        `;
                }, "width": "20%"
            }
        ]
    });
}


function Delete(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar la Categoria?",
        text: "Este Registro no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}