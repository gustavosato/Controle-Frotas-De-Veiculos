function GerarChassi() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'Chassi' },
        dataType: 'json',
        success: function (response, status) {
            $('#Chassi').focus();
            $('#Chassi').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
    })
}

function GerarPlaca() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'Placa' },
        dataType: 'json',
        success: function (response, status) {
            $('#Placa').focus();
            $('#Placa').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
})
}

function GerarRenavam() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'Renavam' },
        dataType: 'json',
        success: function (response, status) {
            $('#Renavam').focus();
            $('#Renavam').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
    })
}

function GerarNumContrato() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'NumContrato' },
        dataType: 'json',
        success: function (response, status) {
            $('#NumContrato').focus();
            $('#NumContrato').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
    })
}

function GerarNumGravame() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'NumGravame' },
        dataType: 'json',
        success: function (response, status) {
            $('#NumGravame').focus();
            $('#NumGravame').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
    })
}

function GerarCnpjAgente() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'CnpjAgente' },
        dataType: 'json',
        success: function (response, status) {
            $('#CnpjAgente').focus();
            $('#CnpjAgente').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
    })
}

function GerarCodAgente() {
    $.ajax({
        type: 'GET',
        url: '/Gravame/GerarDados',
        data: { tipo: 'CodAgente' },
        dataType: 'json',
        success: function (response, status) {
            $('#CodAgente').focus();
            $('#CodAgente').val(JSON.parse(response))
        },
        error: function (xhr, status, erro) {
            console.log('error');
        }
    })
}

