using Autofac;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Repository.Data;
using ControleVeiculos.ApplicationService;

namespace ControleVeiculos.CrossCutting
{
    public class DependencyRegister
    {
        public static IContainer Register(ContainerBuilder container)
        {
            //Application Service
            container.RegisterType<ApplicationSystemService>().As<IApplicationSystemService>();
            container.RegisterType<ParameterService>().As<IParameterService>();
            container.RegisterType<ParameterValueService>().As<IParameterValueService>();
            container.RegisterType<SystemFeatureService>().As<ISystemFeatureService>();
            container.RegisterType<SystemMenuService>().As<ISystemMenuService>();
            container.RegisterType<SystemParameterService>().As<ISystemParameterService>();
            container.RegisterType<UserService>().As<IUserService>();
            container.RegisterType<FuncionarioService>().As<IFuncionarioService>();
            container.RegisterType<CnhService>().As<ICnhService>();
            container.RegisterType<ReservaService>().As<IReservaService>();
            container.RegisterType<VeiculoService>().As<IVeiculoService>();
            container.RegisterType<ManutencaoService>().As<IManutencaoService>();
            container.RegisterType<AbastecimentoService>().As<IAbastecimentoService>();
            container.RegisterType<StatusService>().As<IStatusService>();
            container.RegisterType<EmprestimoService>().As<IEmprestimoService>();
            container.RegisterType<ClienteService>().As<IClienteService>();
            container.RegisterType<MotoristaService>().As<IMotoristaService>();
            container.RegisterType<DepartamentoService>().As<IDepartamentoService>();
            container.RegisterType<RotaService>().As<IRotaService>();
            container.RegisterType<SeguroService>().As<ISeguroService>();
            container.RegisterType<SinistroService>().As<ISinistroService>();
            container.RegisterType<FilialService>().As<IFilialService>();
            container.RegisterType<EntradaSaidaService>().As<IEntradaSaidaService>();
            container.RegisterType<FinancaService>().As<IFinancaService>();
            container.RegisterType<KilometragemService>().As<IKilometragemService>();
            container.RegisterType<MultaService>().As<IMultaService>();
            container.RegisterType<AcessorioService>().As<IAcessorioService>();
            container.RegisterType<DocumentoService>().As<IDocumentoService>();
            container.RegisterType<EncryptService>().As<IEncryptyService>();
            container.RegisterType<StringUtilityService>().As<IStringUtilityService>();

            //Repository
            container.RegisterType<ApplicationSystemRepository>().As<IApplicationSystemRepository>();
            container.RegisterType<ParameterRepository>().As<IParameterRepository>();
            container.RegisterType<ParameterValueRepository>().As<IParameterValueRepository>();
            container.RegisterType<SystemFeatureRepository>().As<ISystemFeatureRepository>();
            container.RegisterType<SystemMenuRepository>().As<ISystemMenuRepository>();
            container.RegisterType<SystemParameterRepository>().As<ISystemParameterRepository>();
            container.RegisterType<UserRepository>().As<IUserRepository>();
            container.RegisterType<FuncionarioRepository>().As<IFuncionarioRepository>();
            container.RegisterType<CnhRepository>().As<ICnhRepository>();
            container.RegisterType<ReservaRepository>().As<IReservaRepository>();
            container.RegisterType<VeiculoRepository>().As<IVeiculoRepository>();
            container.RegisterType<ManutencaoRepository>().As<IManutencaoRepository>();
            container.RegisterType<AbastecimentoRepository>().As<IAbastecimentoRepository>();
            container.RegisterType<StatusRepository>().As<IStatusRepository>();
            container.RegisterType<EmprestimoRepository>().As<IEmprestimoRepository>();
            container.RegisterType<ClienteRepository>().As<IClienteRepository>();
            container.RegisterType<MotoristaRepository>().As<IMotoristaRepository>();
            container.RegisterType<DepartamentoRepository>().As<IDepartamentoRepository>();
            container.RegisterType<RotaRepository>().As<IRotaRepository>();
            container.RegisterType<SeguroRepository>().As<ISeguroRepository>();
            container.RegisterType<SinistroRepository>().As<ISinistroRepository>();
            container.RegisterType<FilialRepository>().As<IFilialRepository>();
            container.RegisterType<EntradaSaidaRepository>().As<IEntradaSaidaRepository>();
            container.RegisterType<FinancaRepository>().As<IFinancaRepository>();
            container.RegisterType<KilometragemRepository>().As<IKilometragemRepository>();
            container.RegisterType<MultaRepository>().As<IMultaRepository>();
            container.RegisterType<AcessorioRepository>().As<IAcessorioRepository>();
            container.RegisterType<DocumentoRepository>().As<IDocumentoRepository>();

            return container.Build();
        }
    }
}
