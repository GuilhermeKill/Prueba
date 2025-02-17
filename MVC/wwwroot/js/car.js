if (typeof (Car) == "undefined") {
    Car = {};
}

if (typeof (Countrie) == "undefined") {
    Countrie = {};
}

Car.routeLocationUrl = window.location.origin;
Car.urlGridCars = Car.routeLocationUrl + "/Home/GridCars";

Countrie.routeLocationUrl = window.location.origin;
Countrie.urlGridCars = Car.routeLocationUrl + "/Country/ConsumeContries";
const currentYear = new Date().getFullYear();


Car.Init = function () {
    Car.GridCars();
}

Countrie.Init = function () {
    Countrie.ConsumeContries();
}

let countries = []

Countrie.ConsumeContries = function () {
    return $.ajax({
        url: "/Country/ConsumeContries",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            countries = data;
        },
        error: function (xhr) {
            alert("Erro ao carregar países: " + (xhr.responseJSON?.message || xhr.statusText));
        }
    });
};


$(document).ready(function () {
    Countrie.Init();
});


Car.GridCars = function () {
    let table = $("#gridCar").DataTable({
        responsive: true,
        "destroy": true,
        "scrollX": false,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": Car.urlGridCars,
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.countryFilter = $('#customFilter').val();
                d.yearFilter = $('#yearFilter').val();
            }
        },
        "columnDefs": [
            {
                "targets": "_all",
                "orderable": false,
                "className": "text-center"
            }
        ],
        "columns": [
            { "data": "country", "name": "country", "autoWidth": true },
            { "data": "brand", "name": "brand", "autoWidth": true },
            { "data": "model", "name": "model", "autoWidth": true },
            { "data": "year", "name": "year", "autoWidth": true },
            { "data": "patent", "name": "patent", "autoWidth": true },
            { "data": "codeVin", "name": "codeVin", "autoWidth": true },
            {
                "data": null,
                "data": null,
                "render": function (data, type, full, meta) {
                    return `
                    <button class="btn btn-primary btn-sm" onclick="modalCar(${full.id}, '${full.country}', '${full.brand}', '${full.model}', ${full.year}, '${full.patent}', '${full.codeVin}')">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-danger btn-sm" onclick="deleteCar(${full.id})">
                        <i class="bi bi-trash"></i>
                    </button>
                `;
                },
                "orderable": false,
                "searchable": false
            }
        ],
        "order": [],
        "initComplete": function () {

            $("#gridCar_wrapper .dataTables_filter label").contents().filter(function () {
                return this.nodeType === 3; 
            }).remove();

            $("#gridCar_wrapper .dataTables_filter input").attr("placeholder", "Pesquisar...");


            let tableWrapper = $("#gridCar_wrapper .table");

            if (!tableWrapper.closest('.table-responsive').length) {
                $("#gridCar_wrapper").addClass("table-responsive");
            }


            let contrySearch = $("#gridCar_wrapper .dataTables_filter");

            if (!$('#customFilter').length) {
                contrySearch.append(`
                    <select id="customFilter" class="form-select form-select-sm ms-2">
                        <option value="">Selecione um país</option>
                    </select>
                `);


                $select = $("#customFilter");

                countries.forEach(country => {
                    $select.append(`<option value="${country.name}">${country.name}</option>`);
                });
            }

            $select = $("#editCountry");

            countries.forEach(country => {
                $select.append(`<option value="${country.name}">${country.name}</option>`);
            });


            if (!$('#yearFilter').length) {
                contrySearch.append(`
                       
                            <input type="number" id="yearFilter" class="form-control">
                      
                `);
            }

            $("#yearFilter").attr("placeholder", "Ano Ex: YYYY");

            if (!$('#createButton').length) {
                $("#gridCar_wrapper .dataTables_filter").append(`
                    <button id="createButton" class="btn btn-success btn-sm ms-2">
                        <i class="bi bi-plus"></i>
                    </button>
                `);

                $('#createButton').on('click', function () {
                    modalCar();
                });

                $('#customFilter').on('change', function () {
                    table.draw();
                });

                $('#yearFilter').on('change', function () {
                    table.draw();
                });

            }
        }
    });
}
function deleteCar(id) {
    if (confirm("Tem certeza que deseja excluir este carro?")) {
        $.ajax({
            url: "/Home/DeleteCar",
            type: "POST",
            data: { id: id },
            success: function (response) {
                showAlert("Coche eliminado con éxito", "success", false);
                $("#gridCar").DataTable().ajax.reload();
            },
            error: function () {
                alert("Erro ao excluir o carro.");
            }
        });
    }
}
function modalCar(id = null, country = "", brand = "", model = "", year = "", patent = "", codeVin = "", typeModal = "create") {
    var idElemento = $(".modal").attr("id");

    if (id != null) {
        $("#editCarId").val(id);
        $("#editCountry").val(country);
        $("#editBrand").val(brand);
        $("#editModel").val(model);
        $("#editYear").val(year);
        $("#editPatent").val(patent);
        $("#editCodeVin").val(codeVin);

        if (idElemento == "createCarModal") {
            $("#createCarModal").attr('id', 'editCarModal');
        }
        $("#modalType").text("Editar auto");

        $("#editCarModal").modal("show");

    } else {
        $("#editCarModal").attr('id', 'createCarModal');

        $("#modalType").text("crear auto");
        $("#editCarId").val(0);
        $("#editCountry").val("");
        $("#editBrand").val("");
        $("#editModel").val("");
        $("#editYear").val(null);
        $("#editPatent").val("");
        $("#editCodeVin").val("");

        typeModal == "edit" ? $("#editCarModal").modal("show") : $("#createCarModal").modal("show");
    }
}
function showAlert(message, type, isModal) {
    let alertHtml = $(`
        <div class="alert alert-${type} alert-dismissible fade show" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `);

    if (isModal) {
        $("#alertModal").append(alertHtml);
    } else {
        $("#alert").append(alertHtml);
    }

    setTimeout(() => {
        alertHtml.alert('close');
    }, 5000);
}

function saveCarChanges() {
    var idElemento = $(".modal").attr("id");

    var carData = {
        Id: $("#editCarId").val(),
        Country: $("#editCountry").val(),
        Brand: $("#editBrand").val(),
        Model: $("#editModel").val(),
        Year: $("#editYear").val(),
        Patent: $("#editPatent").val(),
        CodeVin: $("#editCodeVin").val()
    };


    if (carData.Country == "" || carData.Country == null) {
        showAlert("El campo país es obligatorio", "danger", true);
        return
    }
    if (carData.Brand == "" || carData.Brand == null) {
        showAlert("El campo marca es obligatorio!", "danger", true);
        return
    }
    if (carData.Model == "" || carData.Model == null) {
        showAlert("El campo modelo es obligatorio!", "danger", true);
        return
    }
    if (carData.Year == null || carData.Year.length < 4) {
        showAlert("Introduzca un año válido!", "danger", true);
        return
    }
    if (carData.Year > currentYear) {
        showAlert(`El año no puede ser mayor que ${currentYear}!`, "danger", true);
        return
    }
    if (carData.Patent.length < 6 || carData.Patent.length > 8) {
        showAlert("El campo de patente debe tener entre 6 y 8 caracteres!", "danger", true);
        return
    }
    if (carData.CodeVin.length < 14 || carData.CodeVin.length > 17) {
        showAlert("El campo de Código vin debe tener entre 14 y 17 caracteres!", "danger", true);
        return
    }
    if (idElemento == "editCarModal") {
        $.ajax({
            url: "/Home/UpdateCar",
            type: "POST", 
            contentType: "application/json; charset=utf-8", 
            data: JSON.stringify(carData), 
            dataType: "json",
            success: function (response) {
                showAlert("Coche editado exitosamente", "success", false);
                $("#editCarModal").modal("hide");
                $("#gridCar").DataTable().ajax.reload();

            },
            error: function (xhr) {
                alert("Erro ao atualizar o carro: " + xhr.responseJSON.message);
            }
        });
    } else {
        $.ajax({
            url: "/Home/CreateCar",
            type: "POST", 
            contentType: "application/json; charset=utf-8",  
            data: JSON.stringify(carData), 
            dataType: "json",
            success: function (response) {
                showAlert("coche creado exitosamente", "success", false);
                $("#createCarModal").modal("hide");
                $("#gridCar").DataTable().ajax.reload(); 
            },
            error: function (xhr) {
                alert("Erro ao atualizar o carro: " + xhr.responseJSON.message);
            }
        });
    }
}
