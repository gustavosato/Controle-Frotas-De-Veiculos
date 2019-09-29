using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain.Entities.ParameterValues;
using ControleVeiculos.Domain.Entities.ApplicationSystems;
using ControleVeiculos.Domain.Entities.SystemFeatures;
using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.Domain.Entities.SystemParameters;
using Lean.Test.Cloud.Repository.Map;

namespace ControleVeiculos.Repository.Map
{
    public static class DapperUtils
    {

        public static ParameterValueDapper Map(this ParameterValue parameterValue, int primaryKey)
        {
            ParameterValueDapper parameterValueDapper = new ParameterValueDapper();

            parameterValueDapper.parameterValueID = primaryKey;
            parameterValueDapper.parameterValue = parameterValue.parameterValue;
            parameterValueDapper.parameterID = parameterValue.parameterID;
            parameterValueDapper.parentID = parameterValue.parentID;
            parameterValueDapper.isSystem = parameterValue.isSystem;
            parameterValueDapper.description = parameterValue.description;
            parameterValueDapper.creationDate = parameterValue.creationDate;
            parameterValueDapper.createdByID = parameterValue.createdByID;
            parameterValueDapper.lastModifiedDate = parameterValue.lastModifiedDate;
            parameterValueDapper.modifiedByID = parameterValue.modifiedByID;

            return parameterValueDapper;
        }

        public static ParameterDapper Map(this Parameter parameter, int primaryKey)
        {
            ParameterDapper parameterDapper = new ParameterDapper();

            parameterDapper.parameterID = primaryKey;
            parameterDapper.parameterName = parameter.parameterName;
            parameterDapper.systemFeatureID = parameter.systemFeatureID;
            parameterDapper.createdByID = parameter.createdByID;
            parameterDapper.creationDate = parameter.creationDate;
            parameterDapper.lastModifiedDate = parameter.lastModifiedDate;
            parameterDapper.modifiedByID = parameter.modifiedByID;

            return parameterDapper;
        }

        public static UserDapper Map(this User user, int primaryKey)
        {
            UserDapper userDapper = new UserDapper();

            userDapper.userID = primaryKey;
            userDapper.userName = user.userName;
            userDapper.email = user.email;
            userDapper.password = user.password;
            userDapper.cellNumber = user.cellNumber;
            userDapper.functionID = user.functionID;
            userDapper.functionLevelID = user.functionLevelID;
            userDapper.levelClassificationID = user.levelClassificationID;
            userDapper.departmentID = user.departmentID;
            userDapper.totalCost = user.totalCost;
            userDapper.supervisorID = user.supervisorID;
            userDapper.description = user.description;
            userDapper.firstAccess = user.firstAccess;
            userDapper.isAdmin = user.isAdmin;
            userDapper.lastAccessDate = user.lastAccessDate;
            userDapper.lastIPAccess = user.lastIPAccess;
            userDapper.isActive = user.isActive;
            userDapper.accessToDate = user.accessToDate;
            userDapper.updateRecordTo = user.updateRecordTo;
            userDapper.releaseDateUpdateRecordTo = user.releaseDateUpdateRecordTo;
            userDapper.startJob = user.startJob;
            userDapper.endJob = user.endJob;
            userDapper.contractTypeID = user.contractTypeID;
            userDapper.hourTypeID = user.hourTypeID;
            userDapper.rg = user.rg;
            userDapper.cpf = user.cpf;
            userDapper.dateOfBirth = user.dateOfBirth;
            userDapper.homeAddress = user.homeAddress;
            userDapper.cep = user.cep;
            userDapper.district = user.district;
            userDapper.city = user.city;
            userDapper.state = user.state;
            userDapper.homePhone = user.homePhone;
            userDapper.typeBankAccountID = user.typeBankAccountID;
            userDapper.typePersonID = user.typePersonID;
            userDapper.agency = user.agency;
            userDapper.bankAccount = user.bankAccount;
            userDapper.bankName = user.bankName;
            userDapper.socialReason = user.socialReason;
            userDapper.cnpj = user.cnpj;
            userDapper.optingSimple = user.optingSimple;
            userDapper.registeredCity = user.registeredCity;
            userDapper.isEmployee = user.isEmployee;
            userDapper.createdByID = user.createdByID;
            userDapper.creationDate = user.creationDate;
            userDapper.modifiedByID = user.modifiedByID;
            userDapper.lastModifiedDate = user.lastModifiedDate;

            return userDapper;
        }

        public static ApplicationSystemDapper Map(this ApplicationSystem applicationSystem, int primaryKey)
        {
            ApplicationSystemDapper aplicationSystemDapper = new ApplicationSystemDapper();

            aplicationSystemDapper.applicationSystemID = primaryKey;
            aplicationSystemDapper.applicationSystemName = applicationSystem.applicationSystemName;
            aplicationSystemDapper.description = applicationSystem.description;
            aplicationSystemDapper.applicationTypeID = applicationSystem.applicationTypeID;
            aplicationSystemDapper.customerID = applicationSystem.customerID;
            aplicationSystemDapper.createdByID = applicationSystem.createdByID;
            aplicationSystemDapper.creationDate = applicationSystem.creationDate;
            aplicationSystemDapper.lastModifiedDate = applicationSystem.lastModifiedDate;
            aplicationSystemDapper.modifiedByID = applicationSystem.modifiedByID;

            return aplicationSystemDapper;

        }

        public static SystemFeatureDapper Map(this SystemFeature systemFeature, int primaryKey)
        {
            SystemFeatureDapper systemFeatureDapper = new SystemFeatureDapper();

            systemFeatureDapper.systemFeatureID = primaryKey;
            systemFeatureDapper.systemFeatureName = systemFeature.systemFeatureName;
            systemFeatureDapper.systemFeatureTypeID = systemFeature.systemFeatureTypeID;
            systemFeatureDapper.createdByID = systemFeature.createdByID;
            systemFeatureDapper.creationDate = systemFeature.creationDate;
            systemFeatureDapper.lastModifiedDate = systemFeature.lastModifiedDate;
            systemFeatureDapper.modifiedByID = systemFeature.modifiedByID;

            return systemFeatureDapper;
        }

        public static SystemMenuDapper Map(this SystemMenu systemMenu, int primaryKey)
        {
            SystemMenuDapper systemMenuDapper = new SystemMenuDapper();

            systemMenuDapper.menuID = primaryKey;
            systemMenuDapper.textMenu = systemMenu.textMenu;
            systemMenuDapper.description = systemMenu.description;
            systemMenuDapper.ordem = systemMenu.ordem;
            systemMenuDapper.urlAction = systemMenu.urlAction;
            systemMenuDapper.controller = systemMenu.controller;
            systemMenuDapper.icon = systemMenu.icon;
            systemMenuDapper.itsAdmin = systemMenu.itsAdmin;
            systemMenuDapper.systemFeatureID = systemMenu.systemFeatureID;
            systemMenuDapper.createdByID = systemMenu.createdByID;
            systemMenuDapper.creationDate = systemMenu.creationDate;
            systemMenuDapper.lastModifiedDate = systemMenu.lastModifiedDate;
            systemMenuDapper.modifiedByID = systemMenu.modifiedByID;

            return systemMenuDapper;
        }

        public static SystemParameterDapper Map(this SystemParameter systemParameter, int primaryKey)
        {
            SystemParameterDapper systemParameterDapper = new SystemParameterDapper();

            systemParameterDapper.parameterID = primaryKey;
            systemParameterDapper.paramterName = systemParameter.paramterName;
            systemParameterDapper.paramterValue = systemParameter.paramterValue;
            systemParameterDapper.paramterDefaultValue = systemParameter.paramterDefaultValue;
            systemParameterDapper.createdByID = systemParameter.createdByID;
            systemParameterDapper.creationDate = systemParameter.creationDate;
            systemParameterDapper.lastModifiedDate = systemParameter.lastModifiedDate;
            systemParameterDapper.modifiedByID = systemParameter.modifiedByID;

            return systemParameterDapper;
        }

        public static ClienteDapper Map(this Clientes clientes, int primaryKey)
        {
            ClienteDapper clientesDapper = new ClienteDapper();

            clientesDapper.clienteID = primaryKey;
            clientesDapper.nomeCliente = clientes.nomeCliente;
            clientesDapper.ramo = clientes.ramo;
            clientesDapper.estado = clientes.estado;
            clientesDapper.cidade = clientes.cidade;
            clientesDapper.telefone = clientes.telefone;
            clientesDapper.email = clientes.email;
            clientesDapper.status = clientes.status;

            return clientesDapper;
        }

        public static StatusDapper Map(this Status status, int primaryKey)
        {
            StatusDapper statusDapper = new StatusDapper();

            statusDapper.statusID = primaryKey;
            statusDapper.disponibilidade = status.disponibilidade;
            statusDapper.emUso = status.emUso;
            statusDapper.emManutencao = status.emManutencao;
            statusDapper.reservado = status.reservado;
            statusDapper.veiculoID = status.veiculoID;
            
            return statusDapper;
        }
        
        public static FuncionarioDapper Map(this Funcionarios funcionarios, int primaryKey)
        {
            FuncionarioDapper funcionariosDapper = new FuncionarioDapper();

            funcionariosDapper.funcionarioID = primaryKey;
            funcionariosDapper.nomeFuncionario = funcionarios.nomeFuncionario;
            funcionariosDapper.endereco = funcionarios.endereco;
            funcionariosDapper.cpf = funcionarios.cpf;
            funcionariosDapper.funcao = funcionarios.funcao;
            funcionariosDapper.setor = funcionarios.setor;
            funcionariosDapper.telefone = funcionarios.telefone;
            funcionariosDapper.numeroCnh = funcionarios.numeroCnh;

            return funcionariosDapper;
        }
        public static CnhDapper Map(this Cnh cnh, int primaryKey)
        {
            CnhDapper cnhDapper = new CnhDapper();

            cnhDapper.numeroCnh = primaryKey;
            cnhDapper.validade = cnh.validade;
            cnhDapper.categoria = cnh.categoria;
            cnhDapper.funcionarioID = cnh.funcionarioID;
            
            return cnhDapper;
        }

        public static ReservaDapper Map(this Reservas reservas, int primaryKey)
        {
            ReservaDapper reservasDapper = new ReservaDapper();

            reservasDapper.reservaID = primaryKey;
            reservasDapper.dataReserva = reservas.dataReserva;
            reservasDapper.finalidade = reservas.finalidade;
            reservasDapper.destino = reservas.destino;
            reservasDapper.funcionarioID = reservas.funcionarioID;
            reservasDapper.numeroCnh = reservas.numeroCnh;
            reservasDapper.veiculoID = reservas.veiculoID;
            
            return reservasDapper;
        }

        public static VeiculoDapper Map(this Veiculos veiculos, int primaryKey)
        {
            VeiculoDapper veiculosDapper = new VeiculoDapper();

            veiculosDapper.veiculoID = primaryKey;
            veiculosDapper.modelo = veiculos.packageName;
            veiculosDapper.cor = veiculos.description;
            veiculosDapper.placa = veiculos.demandID;
            veiculosDapper.status = veiculos.statusID;
            veiculosDapper.ano = veiculos.release;
            veiculosDapper.numeroChassi = veiculos.cycle;
            veiculosDapper.motor = veiculos.emailsToSendReport;
            veiculosDapper.manutencaoID = veiculos.tecnologyID;
            veiculosDapper.abastecimentoID = veiculos.browserID;
            
            return veiculosDapper;
        }

        public static ManutencaoDapper Map(this Manutencao manutencao, int primaryKey)
        {
            ManutencaoDapper manutencaoDapper = new ManutencaoDapper();

            manutencaoDapper.manutencaoID = primaryKey;
            manutencaoDapper.responsavel = manutencao.responsavel;
            manutencaoDapper.dataManutencao = manutencao.dataManutencao;
            manutencaoDapper.descricao = manutencao.descricao;
            manutencaoDapper.veiculoID = manutencao.veiculoID;
            
            return manutencaoDapper;
        }
        public static AbastecimentoDapper Map(this Abastecimentos abastecimentos, int primaryKey)
        {
            AbastecimentoDapper abastecimentosDapper = new AbastecimentoDapper();

            abastecimentosDapper.abastecimentoID = primaryKey;
            abastecimentosDapper.tipoCombustivel = abastecimentos.summary;
            abastecimentosDapper.responsavel = abastecimentos.description;
            abastecimentosDapper.data = abastecimentos.statusID;
            abastecimentosDapper.kmAtual = abastecimentos.severityID;
            abastecimentosDapper.veiculoID = abastecimentos.priorityID;
            
            return abastecimentosDapper;
        }
        public static EmprestimoDapper Map(this Emprestimo emprestimo, int primaryKey)
        {
            EmprestimoDapper emprestimoDapper = new EmprestimoDapper();

            emprestimoDapper.emprestimoID = primaryKey;
            emprestimoDapper.kmInicial = emprestimo.kmInicial;
            emprestimoDapper.kmFinal = emprestimo.kmFinal;
            emprestimoDapper.dataSaida = emprestimo.dataSaida;
            emprestimoDapper.dataRetorno = emprestimo.dataRetorno;
            emprestimoDapper.destino = emprestimo.destino;
            emprestimoDapper.veiculoID = emprestimo.veiculoID;
            emprestimoDapper.funcionarioID = emprestimo.funcionarioID;
 
            return emprestimoDapper;
        }

        public static MotoristaDapper Map(this Motoristas motoristas, int primaryKey)
        {
            MotoristaDapper motoristasDapper = new MotoristaDapper();

            motoristasDapper.motoristaID = primaryKey;
            motoristasDapper.nomeMotorista = motoristas.kmInicial;
            motoristasDapper.numeroCnh = motoristas.kmFinal;
           
            return motoristasDapper;
        }

        public static DepartamentoDapper Map(this Departamento departamento, int primaryKey)
        {
            DepartamentoDapper departamentoDapper = new DepartamentoDapper();

            departamentoDapper.departamentoID = primaryKey;
            departamentoDapper.nomeDepartamento = departamento.nomeDepartamento;
            departamentoDapper.descricao = departamento.descricao;
            departamentoDapper.funcionarioID = departamento.funcionarioID;

            return departamentoDapper;
        }

        public static RotaDapper Map(this Rotas rotas, int primaryKey)
        {
            RotaDapper rotasDapper = new RotaDapper();

            rotasDapper.rotaID = primaryKey;
            rotasDapper.cidade = rotas.cidade;
            rotasDapper.estado = rotas.estado;
            rotasDapper.distancia = rotas.distancia;
            rotasDapper.pedagio = rotas.pedagio;
            rotasDapper.dataIda = rotas.dataIda;
            rotasDapper.dataVolta = rotas.dataVolta;

            return rotasDapper;
        }

        public static SeguroDapper Map(this Seguro seguro, int primaryKey)
        {
            SeguroDapper seguroDapper = new SeguroDapper();

            seguroDapper.seguroID = primaryKey;
            seguroDapper.apolice = seguro.apolice;
            seguroDapper.seguradora = seguro.seguradora;
            seguroDapper.franquia = seguro.franquia;
            seguroDapper.tipoSeguro = seguro.tipoSeguro;
            seguroDapper.dataContratacao = seguro.dataContratacao;
            seguroDapper.vigencia = seguro.vigencia;
            seguroDapper.fimContratacao = seguro.fimContratacao;
            seguroDapper.renovacao = seguro.renovacao;
            seguroDapper.telefoneSeguradora = seguro.telefoneSeguradora;
            seguroDapper.periodoCarencia = seguro.periodoCarencia;
            seguroDapper.indenizacao = seguro.indenizacao;
            seguroDapper.sinistroID = seguro.sinistroID;
            seguroDapper.veiculoID = seguro.veiculoID;


            return seguroDapper;
        }

        public static SinistroDapper Map(this Sinistro sinistro, int primaryKey)
        {
            SinistroDapper sinistroDapper = new SinistroDapper();

            sinistroDapper.sinistroID = primaryKey;
            sinistroDapper.apolice = sinistro.apolice;
            sinistroDapper.franquia = sinistro.franquia;
            sinistroDapper.tipoSinistro = sinistro.tipoSinistro;
           
            return sinistroDapper;
        }

        public static FilialDapper Map(this Filiais filiais, int primaryKey)
        {
            FilialDapper filiaisDapper = new FilialDapper();

            filiaisDapper.filialID = primaryKey;
            filiaisDapper.nomeFilial = filiais.nomeFilial;
            filiaisDapper.cidade = filiais.cidade;
            filiaisDapper.estado = filiais.estado;

            return filiaisDapper;
        }

        public static EntradaSaidaDapper Map(this EntradaSaida entradaSaida, int primaryKey)
        {
            EntradaSaidaDapper entradaSaidaDapper = new EntradaSaidaDapper();

            entradaSaidaDapper.entradaSaidaID = primaryKey;
            entradaSaidaDapper.emprestimoID = entradaSaida.emprestimoID;
            entradaSaidaDapper.servicosID = entradaSaida.servicosID;
            entradaSaidaDapper.veiculoID = entradaSaida.veiculoID;

            return entradaSaidaDapper;
        }

        public static FinancaDapper Map(this Financas financas, int primaryKey)
        {
            FinancaDapper financasDapper = new FinancaDapper();

            financasDapper.financaID = primaryKey;
            financasDapper.valorCarro = financas.valorCarro;
            financasDapper.valorSeguro = financas.valorSeguro;
            financasDapper.valorAgua = financas.valorAgua;
            financasDapper.valorLuz = financas.valorLuz;
            financasDapper.valorInternet = financas.valorInternet;
            financasDapper.valorManutencao = financas.valorManutencao;
            financasDapper.salarios = financas.salarios;
            financasDapper.gastosExtras = financas.gastosExtras;
           
            return financasDapper;
        }

        public static FinancaDapper Map(this Financas financas, int primaryKey)
        {
            FinancaDapper financasDapper = new FinancaDapper();

            financasDapper.financaID = primaryKey;
            financasDapper.valorCarro = financas.valorCarro;
            financasDapper.valorSeguro = financas.valorSeguro;
            financasDapper.valorAgua = financas.valorAgua;
            financasDapper.valorLuz = financas.valorLuz;
            financasDapper.valorInternet = financas.valorInternet;
            financasDapper.valorManutencao = financas.valorManutencao;
            financasDapper.salarios = financas.salarios;
            financasDapper.gastosExtras = financas.gastosExtras;

            return financasDapper;
        }

        public static KilometragemDapper Map(this Kilometragem kilometragem, int primaryKey)
        {
            KilometragemDapper kilometragemDapper = new KilometragemDapper();

            kilometragemDapper.kilometragemID = primaryKey;
            kilometragemDapper.kilometragemTotal = kilometragem.kilometragemTotal;
            kilometragemDapper.veiculoID = kilometragem.veiculoID;
           
            return kilometragemDapper;
        }

        public static MultaDapper Map(this Multas multas, int primaryKey)
        {
            MultaDapper multasDapper = new MultaDapper();

            multasDapper.multaID = primaryKey;
            multasDapper.veiculoID = multas.veiculoID;
            multasDapper.clienteID = multas.clienteID;
            multasDapper.cnhID = multas.cnhID;
            
            return multasDapper;
        }

        public static AcessorioDapper Map(this Acessorios acessorios, int primaryKey)
        {
            AcessorioDapper acessoriosDapper = new AcessorioDapper();

            acessoriosDapper.acessorioID = primaryKey;
            acessoriosDapper.gps = acessorios.gps;
            acessoriosDapper.airBag = acessorios.airBag;
            acessoriosDapper.arCondicionado = acessorios.arCondicionado;
            acessoriosDapper.direcao = acessorios.direcao;
            acessoriosDapper.travasEletricas = acessorios.travasEletricas;
            acessoriosDapper.vidroEletrico = acessorios.vidroEletrico;
            acessoriosDapper.alarme = acessorios.alarme;

            return acessoriosDapper;
        }

        public static DocumentoDapper Map(this Documentos documentos, int primaryKey)
        {
            DocumentoDapper documentosDapper = new DocumentoDapper();

            documentosDapper.documentoID = primaryKey;
            documentosDapper.seguroID = documentos.seguroID;
            documentosDapper.numeroCnh = documentos.numeroCnh;
            documentosDapper.clienteID = documentos.clienteID;
            
            return documentosDapper;
        }
    } 
}
