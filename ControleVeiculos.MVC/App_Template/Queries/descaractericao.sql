use LeanTestManager

update Users set cellNumber = '(00) 00000-0000', totalCost = '3000,00', startJob = '21/02/2019', rg = '30.000.000-3', cpf = '300.333.111-00', dateOfBirth = '10/10/1990', homeAddress = 'Rua Amador Bueno 138', cep = '030300-000', homePhone = '(00) 00000-0000', agency = '111', bankAccount = '1234567', cnpj = '30.000.011/0001-00'

update Expenses set SubTotal = '10,00', Kilometer = '10', AmountExpense = '18,00'

update AccountingEntries set valueToBeRealized = '1000,00', realizedValue = '1000,00', invoiceNumber = '100', description = 'dados descaracterizados'

update Pipelines set expectedValue = '1000,00', billed = '1000,00'



