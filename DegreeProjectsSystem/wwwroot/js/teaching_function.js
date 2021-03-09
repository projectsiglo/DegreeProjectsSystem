let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/TeachingFunction/GetAllTeachingFunctions"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "active",
                "render": function (data) {
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
                    return `
                            <div class="text-center">
                                <a href="/Admin/TeachingFunction/InsertOrUpdateTeachingFunction/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="far fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/TeachingFunction/DeleteTeachingFunction/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="far fa-trash-alt"></i>
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
        title: "Esta seguro que quiere eliminar la función docente?",
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