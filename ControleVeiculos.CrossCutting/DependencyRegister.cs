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
            container.RegisterType<AccountingEntrieService>().As<IAccountingEntrieService>();
            container.RegisterType<ApplicationSystemService>().As<IApplicationSystemService>();
            container.RegisterType<AttachmentService>().As<IAttachmentService>();
            container.RegisterType<CustomerService>().As<ICustomerService>();
            container.RegisterType<CustomerUserService>().As<ICustomerUserService>();
            container.RegisterType<DailyLogService>().As<IDailyLogService>();
            container.RegisterType<DailyLogCommentService>().As<IDailyLogCommentService>();
            container.RegisterType<DemandService>().As<IDemandService>();
            container.RegisterType<DemandUserService>().As<IDemandUserService>();
            container.RegisterType<GroupUserService>().As<IGroupUserService>();
            container.RegisterType<EncryptService>().As<IEncryptService>();
            container.RegisterType<ExpenseService>().As<IExpenseService>();
            container.RegisterType<ExportManagerService>().As<IExportManagerService>();
            container.RegisterType<FeatureService>().As<IFeatureService>();
            container.RegisterType<LicenseGeneratorService>().As<ILicenseGeneratorService>();
            container.RegisterType<MailService>().As<IMailService>();
            container.RegisterType<MovimentEmployeeService>().As<IMovimentEmployeeService>();
            container.RegisterType<ParameterService>().As<IParameterService>();
            container.RegisterType<ParameterValueService>().As<IParameterValueService>();
            container.RegisterType<StringUtilityService>().As<IStringUtilityService>();
            container.RegisterType<SystemFeatureService>().As<ISystemFeatureService>();
            container.RegisterType<SystemMenuService>().As<ISystemMenuService>();
            container.RegisterType<SystemParameterService>().As<ISystemParameterService>();
            container.RegisterType<TimeReleaseService>().As<ITimeReleaseService>();
            container.RegisterType<UserService>().As<IUserService>();
            container.RegisterType<TestPackageService>().As<ITestPackageService>();
            container.RegisterType<DefectService>().As<IDefectService>();
            container.RegisterType<EquipmentAccessorieService>().As<IEquipmentAccessorieService>();
            container.RegisterType<PipelineService>().As<IPipelineService>();
            container.RegisterType<ResumeService>().As<IResumeService>();
            container.RegisterType<PipelineEventService>().As<IPipelineEventService>();
            container.RegisterType<ContractService>().As<IContractService>();
            container.RegisterType<AnnexContractService>().As<IAnnexContractService>();
            container.RegisterType<ContractAdditiveService>().As<IContractAdditiveService>();
            container.RegisterType<ContactService>().As<IContactService>();
            container.RegisterType<HistoricalService>().As<IHistoricalService>();
            container.RegisterType<GroupService>().As<IGroupService>();
            container.RegisterType<VacancieService>().As<IVacancieService>();
            container.RegisterType<PositionsSalarieService>().As<IPositionsSalarieService>();
			container.RegisterType<SkillService>().As<ISkillService>();
            container.RegisterType<TaskService>().As<ITaskService>();
            container.RegisterType<ProfileService>().As<IProfilesService>();
            container.RegisterType<ElementService>().As<IElementService>();
            container.RegisterType<DashboardService>().As<IDashboardService>();
            container.RegisterType<SupportService>().As<ISupportService>();
            container.RegisterType<TestLogService>().As<ITestLogService>();
            container.RegisterType<TestScenarioService>().As<ITestScenarioService>();
            container.RegisterType<TestCaseService>().As<ITestCaseService>();
            container.RegisterType<TestScenarioFeatureService>().As<ITestScenarioFeatureService>();
            container.RegisterType<VacancieResumeService>().As<IVacancieResumeService>();
            container.RegisterType<ResumeVacancieService>().As<IResumeVacancieService>();

            //Repository
            container.RegisterType<AccountingEntrieRepository>().As<IAccountingEntrieRepository>();
            container.RegisterType<ApplicationSystemRepository>().As<IApplicationSystemRepository>();
            container.RegisterType<AttachmentRepository>().As<IAttachmentRepository>();
            container.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            container.RegisterType<CustomerUserRepository>().As<ICustomerUserRepository>();
            container.RegisterType<DailyLogRepository>().As<IDailyLogRepository>();
            container.RegisterType<DailyLogCommentRepository>().As<IDailyLogCommentRepository>();
            container.RegisterType<DemandRepository>().As<IDemandRepository>();
            container.RegisterType<DemandUserRepository>().As<IDemandUserRepository>();
            container.RegisterType<GroupUserRepository>().As<IGroupUserRepository>();
            container.RegisterType<ExpenseRepository>().As<IExpenseRepository>();
            container.RegisterType<FeatureRepository>().As<IFeatureRepository>();
            container.RegisterType<MovimentEmployeeRepository>().As<IMovimentEmployeeRepository>();
            container.RegisterType<ParameterRepository>().As<IParameterRepository>();
            container.RegisterType<ParameterValueRepository>().As<IParameterValueRepository>();
            container.RegisterType<SystemFeatureRepository>().As<ISystemFeatureRepository>();
            container.RegisterType<SystemMenuRepository>().As<ISystemMenuRepository>();
            container.RegisterType<SystemParameterRepository>().As<ISystemParameterRepository>();
            container.RegisterType<TimeReleaseRepository>().As<ITimeReleaseRepository>();
            container.RegisterType<UserRepository>().As<IUserRepository>();
            container.RegisterType<TestPackageRepository>().As<ITestPackageRepository>();
            container.RegisterType<EquipmentAccessorieRepository>().As<IEquipmentAccessorieRepository>();
            container.RegisterType<DefectRepository>().As<IDefectRepository>();
            container.RegisterType<ResumeRepository>().As<IResumeRepository>();
            container.RegisterType<PipelineRepository>().As<IPipelineRepository>();
            container.RegisterType<PipelineEventRepository>().As<IPipelineEventRepository>();
            container.RegisterType<ContactRepository>().As<IContactRepository>();
            container.RegisterType<ContractRepository>().As<IContractRepository>();
            container.RegisterType<AnnexContractRepository>().As<IAnnexContractRepository>();
            container.RegisterType<ContractAdditiveRepository>().As<IContractAdditiveRepository>();
            container.RegisterType<HistoricalRepository>().As<IHistoricalRepository>();
            container.RegisterType<GroupRepository>().As<IGroupRepository>();
            container.RegisterType<VacancieRepository>().As<IVacancieRepository>();
            container.RegisterType<PositionsSalarieRepository>().As<IPositionsSalarieRepository>();
            container.RegisterType<TaskRepository>().As<ITaskRepository>();
			container.RegisterType<SkillRepository>().As<ISkillRepository>();
            container.RegisterType<ProfileRepository>().As<IProfileRepository>();
            container.RegisterType<ElementRepository>().As<IElementRepository>();
            container.RegisterType<DashboardRepository>().As<IDashboardRepository>();
            container.RegisterType<SupportRepository>().As<ISupportRepository>();
            container.RegisterType<TestLogRepository>().As<ITestLogRepository>();
            container.RegisterType<TestScenarioRepository>().As<ITestScenarioRepository>();
            container.RegisterType<TestCaseRepository>().As<ITestCaseRepository>();
            container.RegisterType<TestScenarioFeatureRepository>().As<ITestScenarioFeatureRepository>();
            container.RegisterType<VacancieResumeRepository>().As<IVacancieResumeRepository>();
            container.RegisterType<ResumeVacancieRepository>().As<IResumeVacancieRepository>();


            return container.Build();
        }
    }
}
