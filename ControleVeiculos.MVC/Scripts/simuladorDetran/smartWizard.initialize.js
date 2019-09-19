$(document).ready(function () {

    $('#smartwizard').smartWizard({
        useURLhash: false,
        lang: {
            next: 'Proximo',
            previous: 'Anterior'
        },
        theme: 'arrows'
    });
    $("#smartwizard").on("leaveStep", function (e, anchorObject, stepNumber, stepDirection) {

        //Validações de preenchimento Dados Veiculo
        if (stepNumber == 0 && stepDirection === 'forward') {
            var isValid = true;

            if ($('#AnoFabricacao').val() == 0) {
                iziToast.error({
                    message: 'O campo ano fabricação é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#AnoModelo').val() == 0) {
                iziToast.error({
                    message: 'O campo ano modelo é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#Renavam').val() === '') {
                iziToast.error({
                    message: 'O campo renavam é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#Placa').val() === '') {
                iziToast.error({
                    message: 'O campo placa é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }


            if ($('#Chassi').val() === '') {
                iziToast.error({
                    message: 'O campo chassi é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if (!isValid)
                return false;
        }

        //Validações de preenchimento Dados Financiamento
        if (stepNumber == 1 && stepDirection === 'forward') {
            var isValid = true;

            if ($('#NomeFinanciador').val() === '') {
                iziToast.error({
                    message: 'O campo nome financiador é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#CnpjAgente').val() === '') {
                iziToast.error({
                    message: 'O campo cnpj do agente é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#CodAgente').val() === '') {
                iziToast.error({
                    message: 'O campo código do agente é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#NumGravame').val() === '') {
                iziToast.error({
                    message: 'O campo número do gravame é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if ($('#NumContrato').val() === '') {
                iziToast.error({
                    message: 'O campo número do contrato é obrigatório',
                    position: 'topRight',
                    timeout: 5000
                })

                isValid = false;
            }

            if (!isValid)
                return false;
        }

        var elmForm = $("#form-step-" + stepNumber);
        var elments = new Array();

        $("span[id$='-error']").each(function (i, el) {
            elments.push(el);
        })

        if (elments.length > 0) {
            return false;
        }

        return true;
    });
});