var dataHideColums, dataSearchColumns;

$(document).ready(function () {
    createElemntsTimes();
    multiSelect();
    drodownDataSearch(columnsSearch, 'CustomName', 'searchParam');
});


function validateDates() {
    var startDate = $('#dtpStart').val();
    var endDate = $('#dtpEnd').val();
    console.log("Fecha inicial: " + startDate);
    console.log("Fecha final: " + endDate);

    if (startDate === "" || endDate === "") {
        Swal.fire({
            title: 'Debe seleccionar la fecha',
        });
    } else {
        ServiceGetTickets();
    }
}
function ServiceGetMessages() {
    Swal.fire({
        title: 'Loading...',
        allowOutsideClick: false,
        showConfirmButton: false,
        onBeforeOpen: (modal) => {
            modal.showLoading();
            modal.disableCloseButton();
        }
    });

    var data = {
        startDate: $("#dtpStart").val(),
        endDate: $("#dtpEnd").val(),
    };

    $.ajax({
        data: data,
        type: "POST",
        dataType: "json",
        url: '/Messages/Search',
    }).then(response => JSON.parse(JSON.stringify(response)))
        .then(data => {
            // Hide loading modal
            Swal.close();

            if (data.dataMessages.length == 0) {
                noData();
                return;
            }
            filtersData = data.filters;
            dropdowns.value = null;
            multiSelectInput.value = null;
            dropdowns.enabled = false;
            multiSelectInput.enabled = false;
            const btn = document.getElementById('button-filter');
            btn.disabled = false;
            dateDocuments = $("#dtpStart").val() + " " + $("#dtpEnd").val();
            let dataColumns = setColums(data.dataMessages, columnsToHide);
            let exportFunctions = addFnctionsGrid(['Excel', 'Csv']);
            dataColumns = addCommandsGridDetails(dataColumns);
            dropdowns.enabled = true;
            dataGridSave = data.dataMessages;
            setGrid(data.dataMessages, dataColumns, exportFunctions);
        })
        .catch(error => {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: error.name + ': ' + error.message
            });
        })
        .then(response => console.log('Success:', response));
}
function ServiceGetTickets() {
    var startDate = $('#dtpStart').val();
    var endDate = $('#dtpEnd').val();
    var max = 0;
    var componente = $('#componente').val();
    var data = { startDate: startDate, endDate: endDate, max: max, componente: componente }

    Swal.fire({
        title: 'Cargando...',
        allowOutsideClick: false,
        showConfirmButton: false,
        onBeforeOpen: (modal) => {
            modal.showLoading();
            modal.disableCloseButton();
        }
    });
    $.ajax({
        type: "GET",
        url: "/Tickets/GetTickets",
        data: data
    }).then(data => {
        // Hide loading modal
        Swal.close();

        if (data.dataMessages.length == 0) {
            noData();
            return;
        }
        filtersData = data.filters;
        dropdowns.value = null;
        multiSelectInput.value = null;
        dropdowns.enabled = false;
        multiSelectInput.enabled = false;
        const btn = document.getElementById('button-filter');
        btn.disabled = false;
        dateDocuments = $("#dtpStart").val() + " " + $("#dtpEnd").val();
        let dataColumns = setColums(data.dataMessages, columnsToHide);
        let exportFunctions = addFnctionsGrid(['Excel', 'Csv']);
        dataColumns = addCommandsGridDetails(dataColumns);
        dropdowns.enabled = true;
        dataGridSave = data.dataMessages;
        setGrid(data.dataMessages, dataColumns, exportFunctions);

    })
        success: function (response) {
            Swal.close();
            var tbody = $('#table tbody');
            tbody.empty();

            $.each(response, function (index, ticket) {
                var row = $('<tr>');
                console.log(ticket.id_ticket)
                row.append($('<td onclick="showMoreInformation(\'' + ticket.id_ticket + '\')" style="cursor: pointer; background: none;">').text('Ver más'));
                row.append($('<td>').text(ticket.id_ticket));
                row.append($('<td>').text(ticket.fecha_apertura));
                row.append($('<td>').text(ticket.id_componente));
                row.append($('<td>').text(ticket.tipoComponente));
                row.append($('<td>').text(ticket.estado_ticket));
                row.append($('<td>').text(ticket.nivel_falla));
                row.append($('<td>').text(ticket.codigo_falla));
                row.append($('<td>').text(ticket.diagnostico_causa));
                row.append($('<td>').text(ticket.fecha_arribo_locacion));
                row.append($('<td>').text(ticket.fecha_cierre));
                row.append($('<td>').text(ticket.componente_Parte));
                row.append($('<td>').text(ticket.tipo_reparacion));
                row.append($('<td>').text(ticket.descripcion_reparacion));
                row.append($('<td>').text(ticket.id_estacion));
                row.append($('<td>').text(ticket.id_vagon));
                row.append($('<td>').text(ticket.id_puerta));
                row.append($('<td>').text(ticket.identificacion));
                row.append($('<td>').text(ticket.tipo_mantenimiento));
                row.append($('<td>').text(ticket.tipo_causa));
                row.append($('<td>').text(ticket.tipo_ajuste_configuracion));
                row.append($('<td>').text(ticket.descripcion));

                tbody.append(row);
            });
        },
        error: function (xhr, status, error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: error
            });
        }
    });
}

const targetEl = document.getElementById('dropdownInformation');

// set the element that trigger the dropdown menu on click
const triggerEl = document.getElementById('dropdownInformationButton');

// options with default values
const options = {
    placement: 'bottom',
    onHide: () => {
        console.log('dropdown has been hidden');
    },
    onShow: () => {
        console.log('dropdown has been shown');
    }
};

var detailsData = function (args) {
    //alert(JSON.stringify(args.rowData));
}