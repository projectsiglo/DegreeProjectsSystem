var dataTable;
var active;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json"
        },
        "ajax": {
            "url": "/Admin/Faculty/GetAllFaculties"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "active",
                "render": function (data) {
                    active = data;
                    if (data) {
                        return `
                                  <div class="status-active text-center">Activo</div>
                               `
                    }
                    else {
                        return `      
                                  <div class="status-inactive text-center">Inactivo</div>
                               `
                    }
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    if (!active) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/Faculty/InsertOrUpdateFaculty/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit p-1"></i>
                                </a>
                                <a onclick=Delete("/Admin/Faculty/DeleteFaculty/${data}") class="btn btn-danger disabled text-white" disabled style="cursor:pointer;">
                                    <i class="far fa-trash-alt p-1"></i>
                                </a>
                            </div>
                         `;
                    }
                    else {
                        return `
                            <div class="text-center">
                                <a href="/Admin/Faculty/InsertOrUpdateFaculty/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit p-1"></i>
                                </a>
                                <a onclick=Delete("/Admin/Faculty/DeleteFaculty/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="far fa-trash-alt p-1"></i>
                                </a>
                            </div>
                         `;
                    }
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar la Facultad?",
        text: "Este registro se puede  recuperar actualizando su estado a Activo",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((erase) => {
        if (erase) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                        dataTable.ajax.reload();
                    }
                }
            });
        }
    });
}