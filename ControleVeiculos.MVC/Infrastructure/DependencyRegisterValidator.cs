using Autofac;
using FluentValidation;
using ControleVeiculos.MVC.Models.Customers;
using ControleVeiculos.MVC.Models.Users;
using ControleVeiculos.MVC.Models.Expenses;
using ControleVeiculos.MVC.Models.Demands;
using ControleVeiculos.MVC.Models.TimeReleases;
using ControleVeiculos.MVC.Models.AccountingEntries;
using ControleVeiculos.MVC.Models.DailyLogs;
using ControleVeiculos.MVC.Models.ApplicationSystems;
using ControleVeiculos.MVC.Models.Features;
using ControleVeiculos.MVC.Models.Licenses;
using ControleVeiculos.MVC.Models.MovimentEmployees;
using ControleVeiculos.MVC.Models.SystemParameter;
using ControleVeiculos.MVC.Models.Defects;
using ControleVeiculos.MVC.Models.SystemFeatures;
using ControleVeiculos.MVC.Models.TestPackages;
using ControleVeiculos.MVC.Models.Parameters;
using ControleVeiculos.MVC.Models.ParameterValues;
using ControleVeiculos.MVC.Models.Pipelines;
using ControleVeiculos.MVC.Models.Resumes;
using ControleVeiculos.MVC.Models.EquipmentAccessories;
using ControleVeiculos.MVC.Models.PipelineEvents;
using ControleVeiculos.MVC.Models.Contacts;
using ControleVeiculos.MVC.Models.Contracts;
using ControleVeiculos.MVC.Models.AnnexContracts;
using ControleVeiculos.MVC.Models.ContractAdditives;
using ControleVeiculos.MVC.Models.PositionsSalaries;
using ControleVeiculos.MVC.Models.Tasks;
using ControleVeiculos.MVC.Models.Skills;
using ControleVeiculos.MVC.Models.Vacancies;
using ControleVeiculos.MVC.Models.Groups;
using ControleVeiculos.MVC.Models.Elements;
using ControleVeiculos.MVC.Models.SystemMenus;
using ControleVeiculos.MVC.Models.Profiles;
using ControleVeiculos.MVC.Models.Supports;
using ControleVeiculos.MVC.Models.TestLogs;
using ControleVeiculos.MVC.Models.TestScenarios;
using ControleVeiculos.MVC.Models.TestCases;
using ControleVeiculos.MVC.Models.TestScenarioFeatures;


using ControleVeiculos.MVC.Validations.Users;
using ControleVeiculos.MVC.Validations.Expenses;
using ControleVeiculos.MVC.Validations.Customer;
using ControleVeiculos.MVC.Validations.Demands;
using ControleVeiculos.MVC.Validations.TimeRelease;
using ControleVeiculos.MVC.Validations.AccountingEntries;
using ControleVeiculos.MVC.Validations.DailyLog;
using ControleVeiculos.MVC.Validations.ApplicationSystem;
using ControleVeiculos.MVC.Validations.Features;
using ControleVeiculos.MVC.Validations.Licenses;
using ControleVeiculos.MVC.Validations.MovimentEmployee;
using ControleVeiculos.MVC.Validations.SystemParameters;
using ControleVeiculos.MVC.Validations.Defects;
using ControleVeiculos.MVC.Validations.SystemFeatures;
using ControleVeiculos.MVC.Validations.TestPackages;
using ControleVeiculos.MVC.Validations.Parameters;
using ControleVeiculos.MVC.Validations.ParameterValues;
using ControleVeiculos.MVC.Validations.EquipmentAccessories;
using ControleVeiculos.MVC.Validations.Pipelines;
using ControleVeiculos.MVC.Validations.Resumes;
using ControleVeiculos.MVC.Validations.PipelineEvents;
using ControleVeiculos.MVC.Validations.Contacts;
using ControleVeiculos.MVC.Validations.Contract;
using ControleVeiculos.MVC.Validations.AnnexContract;
using ControleVeiculos.MVC.Validations.ContractAdditive;
using ControleVeiculos.MVC.Validations.PositionsSalaries;
using ControleVeiculos.MVC.Validations.Tasks;
using ControleVeiculos.MVC.Validations.Skills;
using ControleVeiculos.MVC.Validations.Vacancies;
using ControleVeiculos.MVC.Validations.Groups;
using ControleVeiculos.MVC.Validations.Elements;
using ControleVeiculos.MVC.Validations.SystemMenus;
using ControleVeiculos.MVC.Validations.Profiles;
using ControleVeiculos.MVC.Validations.Supports;
using ControleVeiculos.MVC.Validations.TestLogs;
using ControleVeiculos.MVC.Validations.TestScenarios;
using ControleVeiculos.MVC.Validations.TestCases;
using ControleVeiculos.MVC.Validations.TestScenarioFeatures;




namespace ControleVeiculos.Infrastructure
{
    public static class DependencyRegisterValidator
    {
        public static ContainerBuilder RegisterValidator(this ContainerBuilder builder)
        {

            builder.RegisterType<UserValidator>()
                .Keyed<IValidator>(typeof(IValidator<UserModel>))
                .As<IValidator>();

            builder.RegisterType<DemandValidator>()
                .Keyed<IValidator>(typeof(IValidator<DemandModel>))
                .As<IValidator>();

            builder.RegisterType<CustomerValidator>()
                .Keyed<IValidator>(typeof(IValidator<CustomerModel>))
                .As<IValidator>();

            builder.RegisterType<ExpenseValidator>()
                .Keyed<IValidator>(typeof(IValidator<ExpenseModel>))
                .As<IValidator>();

            builder.RegisterType<TimeReleaseValidator>()
                .Keyed<IValidator>(typeof(IValidator<TimeReleaseModel>))
                .As<IValidator>();

            builder.RegisterType<AccountingEntrieValidator>()
                .Keyed<IValidator>(typeof(IValidator<AccountingEntrieModel>))
                .As<IValidator>();

            builder.RegisterType<DailyLogValidator>()
                .Keyed<IValidator>(typeof(IValidator<DailyLogModel>))
                .As<IValidator>();

            builder.RegisterType<ApplicationSystemValidator>()
                .Keyed<IValidator>(typeof(IValidator<ApplicationSystemModel>))
                .As<IValidator>();

            builder.RegisterType<FeatureValidator>()
                .Keyed<IValidator>(typeof(IValidator<FeatureModel>))
                .As<IValidator>();

            builder.RegisterType<LicenseValidator>()
                .Keyed<IValidator>(typeof(IValidator<LicenseModel>))
                .As<IValidator>();

            builder.RegisterType<MovimentEmployeeValidator>()
                .Keyed<IValidator>(typeof(IValidator<MovimentEmployeeModel>))
                .As<IValidator>();

            builder.RegisterType<SystemParameterValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemParameterModel>))
                .As<IValidator>();

            builder.RegisterType<DefectValidator>()
                .Keyed<IValidator>(typeof(IValidator<DefectModel>))
                .As<IValidator>();

            builder.RegisterType<SystemFeatureValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemFeatureModel>))
                .As<IValidator>();

            builder.RegisterType<TestPackageValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestPackageModel>))
                .As<IValidator>();

            builder.RegisterType<ParameterValidator>()
                .Keyed<IValidator>(typeof(IValidator<ParameterModel>))
                .As<IValidator>();

            builder.RegisterType<ParameterValueValidator>()
                .Keyed<IValidator>(typeof(IValidator<ParameterValueModel>))
                .As<IValidator>();

            builder.RegisterType<PipelineValidator>()
                .Keyed<IValidator>(typeof(IValidator<PipelineModel>))
                .As<IValidator>();

            builder.RegisterType<ResumeValidator>()
                .Keyed<IValidator>(typeof(IValidator<ResumeModel>))
                .As<IValidator>();

            builder.RegisterType<EquipmentAccessorieValidator>()
                .Keyed<IValidator>(typeof(IValidator<EquipmentAccessorieModel>))
                .As<IValidator>();

            builder.RegisterType<PipelineEventValidator>()
                .Keyed<IValidator>(typeof(IValidator<PipelineEventModel>))
                .As<IValidator>();

            builder.RegisterType<ContactValidator>()
                .Keyed<IValidator>(typeof(IValidator<ContactModel>))
                .As<IValidator>();

            builder.RegisterType<ContractValidator>()
                .Keyed<IValidator>(typeof(IValidator<ContractModel>))
                .As<IValidator>();

            builder.RegisterType<AnnexContractValidator>()
                .Keyed<IValidator>(typeof(IValidator<AnnexContractModel>))
                .As<IValidator>();

            builder.RegisterType<ContractAdditiveValidator>()
                .Keyed<IValidator>(typeof(IValidator<ContractAdditiveModel>))
                .As<IValidator>();

            builder.RegisterType<PositionsSalarieValidator>()
                .Keyed<IValidator>(typeof(IValidator<PositionsSalarieModel>))
                .As<IValidator>();

            builder.RegisterType<TaskValidator>()
                .Keyed<IValidator>(typeof(IValidator<TaskModel>))
                .As<IValidator>();

          	builder.RegisterType<SkillValidator>()
				.Keyed<IValidator>(typeof(IValidator<SkillModel>))
				.As<IValidator>();

            builder.RegisterType<VacancieValidator>()
                .Keyed<IValidator>(typeof(IValidator<VacancieModel>))
                .As<IValidator>();

            builder.RegisterType<GroupValidator>()
                .Keyed<IValidator>(typeof(IValidator<GroupModel>))
                .As<IValidator>();

            builder.RegisterType<ElementValidator>()
                .Keyed<IValidator>(typeof(IValidator<ElementModel>))
                .As<IValidator>();

            builder.RegisterType<SystemMenuValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemMenuModel>))
               .As<IValidator>();

            builder.RegisterType<ProfileValidator>()
                .Keyed<IValidator>(typeof(IValidator<ProfileModel>))
                .As<IValidator>();

            builder.RegisterType<SupportValidator>()
                .Keyed<IValidator>(typeof(IValidator<SupportModel>))
                .As<IValidator>();

            builder.RegisterType<TestLogValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestLogModel>))
                .As<IValidator>();

            builder.RegisterType<TestScenarioValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestScenarioModel>))
                .As<IValidator>();

            builder.RegisterType<TestCaseValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestCaseModel>))
                .As<IValidator>();

            builder.RegisterType<TestScenarioFeatureValidator>()
               .Keyed<IValidator>(typeof(IValidator<TestScenarioFeatureModel>))
               .As<IValidator>();

            return builder;
        }
    }
}