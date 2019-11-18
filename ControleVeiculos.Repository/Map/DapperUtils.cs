using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain.Entities.ParameterValues;
using ControleVeiculos.Domain.Entities.ApplicationSystems;
using ControleVeiculos.Domain.Entities.SystemFeatures;
using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.Domain.Entities.SystemParameters;
using ControleVeiculos.Repository.Map;
using ControleVeiculos.Domain.Entities.Clientes;
using ControleVeiculos.Domain.Entities.Status;
using ControleVeiculos.Domain.Entities.Funcionarios;
using ControleVeiculos.Domain.Entities.Cnhs;
using ControleVeiculos.Domain.Entities.Reservas;
using ControleVeiculos.Domain.Entities.Veiculos;
using ControleVeiculos.Domain.Entities.Manutencoes;
using ControleVeiculos.Domain.Entities.Abastecimentos;
using ControleVeiculos.Domain.Entities.Emprestimos;
using ControleVeiculos.Domain.Entities.Motoristas;
using ControleVeiculos.Domain.Entities.Departamentos;
using ControleVeiculos.Domain.Entities.Rotas;
using ControleVeiculos.Domain.Entities.Seguros;
using ControleVeiculos.Domain.Entities.Sinistros;
using ControleVeiculos.Domain.Entities.Filiais;
using ControleVeiculos.Domain.Entities.EntradaSaidas;
using ControleVeiculos.Domain.Entities.Financas;
using ControleVeiculos.Domain.Entities.Kilometragens;
using ControleVeiculos.Domain.Entities.Multas;
using ControleVeiculos.Domain.Entities.Acessorios;
using ControleVeiculos.Domain.Entities.Documentos;

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
            
            return parameterValueDapper;
        }

        public static ParameterDapper Map(this Parameter parameter, int primaryKey)
        {
            ParameterDapper parameterDapper = new ParameterDapper();

            parameterDapper.parameterID = primaryKey;
            parameterDapper.parameterName = parameter.parameterName;
            parameterDapper.systemFeatureID = parameter.systemFeatureID;
            
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
            userDapper.departamentoID = user.departamentoID;
            userDapper.description = user.description;
            userDapper.firstAccess = user.firstAccess;
            userDapper.isAdmin = user.isAdmin;
            userDapper.isActive = user.isActive;
            userDapper.rg = user.rg;
            userDapper.cpf = user.cpf;
            userDapper.dateOfBirth = user.dateOfBirth;
            userDapper.homeAddress = user.homeAddress;
            userDapper.cep = user.cep;
            userDapper.district = user.district;
            userDapper.city = user.city;
            userDapper.state = user.state;
            userDapper.homePhone = user.homePhone;

            return userDapper;
        }

        public static ApplicationSystemDapper Map(this ApplicationSystem applicationSystem, int primaryKey)
        {
            ApplicationSystemDapper aplicationSystemDapper = new ApplicationSystemDapper();

            aplicationSystemDapper.applicationSystemID = primaryKey;
            aplicationSystemDapper.applicationSystemName = applicationSystem.applicationSystemName;
            aplicationSystemDapper.description = applicationSystem.description;
            aplicationSystemDapper.applicationTypeID = applicationSystem.applicationTypeID;
            
            return aplicationSystemDapper;

        }

        public static SystemFeatureDapper Map(this SystemFeature systemFeature, int primaryKey)
        {
            SystemFeatureDapper systemFeatureDapper = new SystemFeatureDapper();

            systemFeatureDapper.systemFeatureID = primaryKey;
            systemFeatureDapper.systemFeatureName = systemFeature.systemFeatureName;
            systemFeatureDapper.systemFeatureTypeID = systemFeature.systemFeatureTypeID;
            
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
            
            return systemMenuDapper;
        }

        public static SystemParameterDapper Map(this SystemParameter systemParameter, int primaryKey)
        {
            SystemParameterDapper systemParameterDapper = new SystemParameterDapper();

            systemParameterDapper.parameterID = primaryKey;
            systemParameterDapper.paramterName = systemParameter.paramterName;
            systemParameterDapper.paramterValue = systemParameter.paramterValue;
            systemParameterDapper.paramterDefaultValue = systemParameter.paramterDefaultValue;
            
            return systemParameterDapper;
        }

        public static ClienteDapper Map(this Cliente cliente, int primaryKey)
        {
            ClienteDapper clienteDapper = new ClienteDapper();

            clienteDapper.clienteID = primaryKey;
            clienteDapper.nomeCliente = cliente.nomeCliente;
            clienteDapper.ramo = cliente.ramo;
            clienteDapper.estado = cliente.estado;
            clienteDapper.cidade = cliente.cidade;
            clienteDapper.telefone = cliente.telefone;
            clienteDapper.email = cliente.email;
            clienteDapper.status = cliente.status;

            return clienteDapper;
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
        
        public static FuncionarioDapper Map(this Funcionario funcionario, int primaryKey)
        {
            FuncionarioDapper funcionarioDapper = new FuncionarioDapper();

            funcionarioDapper.funcionarioID = primaryKey;
            funcionarioDapper.nomeFuncionario = funcionario.nomeFuncionario;
            funcionarioDapper.endereco = funcionario.endereco;
            funcionarioDapper.cpf = funcionario.cpf;
            funcionarioDapper.funcao = funcionario.funcao;
            funcionarioDapper.setor = funcionario.setor;
            funcionarioDapper.telefone = funcionario.telefone;
            funcionarioDapper.numeroCnh = funcionario.numeroCnh;

            return funcionarioDapper;
        }
        public static CnhDapper Map(this Cnh cnh, int primaryKey)
        {
            CnhDapper cnhDapper = new CnhDapper();

            cnhDapper.cnhID = primaryKey;
            cnhDapper.numeroCnh = cnh.numeroCnh;
            cnhDapper.validade = cnh.validade;
            cnhDapper.categoria = cnh.categoria;
            cnhDapper.funcionarioID = cnh.funcionarioID;
            
            return cnhDapper;
        }

        public static ReservaDapper Map(this Reserva reserva, int primaryKey)
        {
            ReservaDapper reservaDapper = new ReservaDapper();

            reservaDapper.reservaID = primaryKey;
            reservaDapper.dataReserva = reserva.dataReserva;
            reservaDapper.finalidade = reserva.finalidade;
            reservaDapper.destino = reserva.destino;
            reservaDapper.funcionarioID = reserva.funcionarioID;
            reservaDapper.numeroCnh = reserva.numeroCnh;
            reservaDapper.veiculoID = reserva.veiculoID;
            
            return reservaDapper;
        }

        public static VeiculoDapper Map(this Veiculo veiculo, int primaryKey)
        {
            VeiculoDapper veiculoDapper = new VeiculoDapper();

            veiculoDapper.veiculoID = primaryKey;
            veiculoDapper.modelo = veiculo.modelo;
            veiculoDapper.cor = veiculo.cor;
            veiculoDapper.placa = veiculo.placa;
            veiculoDapper.status = veiculo.status;
            veiculoDapper.ano = veiculo.ano;
            veiculoDapper.numeroChassi = veiculo.numeroChassi;
            veiculoDapper.motor = veiculo.motor;
            veiculoDapper.manutencaoID = veiculo.manutencaoID;
            veiculoDapper.abastecimentoID = veiculo.abastecimentoID;
            
            return veiculoDapper;
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
        public static AbastecimentoDapper Map(this Abastecimento abastecimento, int primaryKey)
        {
            AbastecimentoDapper abastecimentoDapper = new AbastecimentoDapper();

            abastecimentoDapper.abastecimentoID = primaryKey;
            abastecimentoDapper.tipoCombustivel = abastecimento.tipoCombustivel;
            abastecimentoDapper.responsavel = abastecimento.responsavel;
            abastecimentoDapper.data = abastecimento.data;
            abastecimentoDapper.kmAtual = abastecimento.kmAtual;
            abastecimentoDapper.veiculoID = abastecimento.veiculoID;
            
            return abastecimentoDapper;
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

        public static MotoristaDapper Map(this Motorista motorista, int primaryKey)
        {
            MotoristaDapper motoristaDapper = new MotoristaDapper();

            motoristaDapper.motoristaID = primaryKey;
            motoristaDapper.nomeMotorista = motorista.nomeMotorista;
            motoristaDapper.numeroCnh = motorista.numeroCnh;
           
            return motoristaDapper;
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

        public static RotaDapper Map(this Rota rota, int primaryKey)
        {
            RotaDapper rotaDapper = new RotaDapper();

            rotaDapper.rotaID = primaryKey;
            rotaDapper.cidade = rota.cidade;
            rotaDapper.estado = rota.estado;
            rotaDapper.distancia = rota.distancia;
            rotaDapper.pedagio = rota.pedagio;
            rotaDapper.dataIda = rota.dataIda;
            rotaDapper.dataVolta = rota.dataVolta;

            return rotaDapper;
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

        public static FilialDapper Map(this Filial filial, int primaryKey)
        {
            FilialDapper filialDapper = new FilialDapper();

            filialDapper.filialID = primaryKey;
            filialDapper.nomeFilial = filial.nomeFilial;
            filialDapper.cidade = filial.cidade;
            filialDapper.estado = filial.estado;

            return filialDapper;
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

        public static FinancaDapper Map(this Financa financa, int primaryKey)
        {
            FinancaDapper financaDapper = new FinancaDapper();

            financaDapper.financaID = primaryKey;
            financaDapper.valorCarro = financa.valorCarro;
            financaDapper.valorSeguro = financa.valorSeguro;
            financaDapper.valorAgua = financa.valorAgua;
            financaDapper.valorLuz = financa.valorLuz;
            financaDapper.valorInternet = financa.valorInternet;
            financaDapper.valorManutencao = financa.valorManutencao;
            financaDapper.salarios = financa.salarios;
            financaDapper.gastosExtras = financa.gastosExtras;
           
            return financaDapper;
        }

        public static KilometragemDapper Map(this Kilometragem kilometragem, int primaryKey)
        {
            KilometragemDapper kilometragemDapper = new KilometragemDapper();

            kilometragemDapper.kilometragemID = primaryKey;
            kilometragemDapper.kilometragemTotal = kilometragem.kilometragemTotal;
            kilometragemDapper.veiculoID = kilometragem.veiculoID;
           
            return kilometragemDapper;
        }

        public static MultaDapper Map(this Multa multa, int primaryKey)
        {
            MultaDapper multaDapper = new MultaDapper();

            multaDapper.multaID = primaryKey;
            multaDapper.veiculoID = multa.veiculoID;
            multaDapper.funcionarioID = multa.funcionarioID;
            
            return multaDapper;
        }

        public static AcessorioDapper Map(this Acessorio acessorio, int primaryKey)
        {
            AcessorioDapper acessorioDapper = new AcessorioDapper();

            acessorioDapper.acessorioID = primaryKey;
            acessorioDapper.gps = acessorio.gps;
            acessorioDapper.airBag = acessorio.airBag;
            acessorioDapper.arCondicionado = acessorio.arCondicionado;
            acessorioDapper.direcao = acessorio.direcao;
            acessorioDapper.travasEletricas = acessorio.travasEletricas;
            acessorioDapper.vidroEletrico = acessorio.vidroEletrico;
            acessorioDapper.alarme = acessorio.alarme;

            return acessorioDapper;
        }

        public static DocumentoDapper Map(this Documento documento, int primaryKey)
        {
            DocumentoDapper documentoDapper = new DocumentoDapper();

            documentoDapper.documentoID = primaryKey;
            documentoDapper.seguroID = documento.seguroID;
            documentoDapper.cnhID = documento.numeroCnh;
            documentoDapper.clienteID = documento.clienteID;
            
            return documentoDapper;
        }
    } 
}
