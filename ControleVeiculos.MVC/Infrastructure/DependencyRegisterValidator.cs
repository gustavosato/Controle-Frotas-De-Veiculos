using Autofac;
using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Customers;
using Lean.Test.Cloud.MVC.Models.Users;
using Lean.Test.Cloud.MVC.Models.Expenses;
using Lean.Test.Cloud.MVC.Models.Demands;
using Lean.Test.Cloud.MVC.Models.TimeReleases;
using Lean.Test.Cloud.MVC.Models.AccountingEntries;
using Lean.Test.Cloud.MVC.Models.DailyLogs;
using Lean.Test.Cloud.MVC.Models.ApplicationSystems;
using Lean.Test.Cloud.MVC.Models.Features;
using Lean.Test.Cloud.MVC.Models.Licenses;
using Lean.Test.Cloud.MVC.Models.MovimentEmployees;
using Lean.Test.Cloud.MVC.Models.SystemParameter;
using Lean.Test.Cloud.MVC.Models.Defects;
using Lean.Test.Cloud.MVC.Models.SystemFeatures;
using Lean.Test.Cloud.MVC.Models.TestPackages;
using Lean.Test.Cloud.MVC.Models.Parameters;
using Lean.Test.Cloud.MVC.Models.ParameterValues;
using Lean.Test.Cloud.MVC.Models.Pipelines;
using Lean.Test.Cloud.MVC.Models.Resumes;
using Lean.Test.Cloud.MVC.Models.EquipmentAccessories;
using Lean.Test.Cloud.MVC.Models.PipelineEvents;
using Lean.Test.Cloud.MVC.Models.Contacts;
using Lean.Test.Cloud.MVC.Models.Contracts;
using Lean.Test.Cloud.MVC.Models.AnnexContracts;
using Lean.Test.Cloud.MVC.Models.ContractAdditives;
using Lean.Test.Cloud.MVC.Models.PositionsSalaries;
using Lean.Test.Cloud.MVC.Models.Tasks;
using Lean.Test.Cloud.MVC.Models.Skills;
using Lean.Test.Cloud.MVC.Models.Vacancies;
using Lean.Test.Cloud.MVC.Models.Groups;
using Lean.Test.Cloud.MVC.Models.Elements;
using Lean.Test.Cloud.MVC.Models.SystemMenus;
using Lean.Test.Cloud.MVC.Models.Profiles;
using Lean.Test.Cloud.MVC.Models.Supports;
using Lean.Test.Cloud.MVC.Models.TestLogs;
using Lean.Test.Cloud.MVC.Models.TestScenarios;
using Lean.Test.Cloud.MVC.Models.TestCases;
using Lean.Test.Cloud.MVC.Models.TestScenarioFeatures;


using Lean.Test.Cloud.MVC.Validations.Users;
using Lean.Test.Cloud.MVC.Validations.Expenses;
using Lean.Test.Cloud.MVC.Validations.Customer;
using Lean.Test.Cloud.MVC.Validations.Demands;
using Lean.Test.Cloud.MVC.Validations.TimeRelease;
using Lean.Test.Cloud.MVC.Validations.AccountingEntries;
using Lean.Test.Cloud.MVC.Validations.DailyLog;
using Lean.Test.Cloud.MVC.Validations.ApplicationSystem;
using Lean.Test.Cloud.MVC.Validations.Features;
using Lean.Test.Cloud.MVC.Validations.Licenses;
using Lean.Test.Cloud.MVC.Validations.MovimentEmployee;
using Lean.Test.Cloud.MVC.Validations.SystemParameters;
using Lean.Test.Cloud.MVC.Validations.Defects;
using Lean.Test.Cloud.MVC.Validations.SystemFeatures;
using Lean.Test.Cloud.MVC.Validations.TestPackages;
using Lean.Test.Cloud.MVC.Validations.Parameters;
using Lean.Test.Cloud.MVC.Validations.ParameterValues;
using Lean.Test.Cloud.MVC.Validations.EquipmentAccessories;
using Lean.Test.Cloud.MVC.Validations.Pipelines;
using Lean.Test.Cloud.MVC.Validations.Resumes;
using Lean.Test.Cloud.MVC.Validations.PipelineEvents;
using Lean.Test.Cloud.MVC.Validations.Contacts;
using Lean.Test.Cloud.MVC.Validations.Contract;
using Lean.Test.Cloud.MVC.Validations.AnnexContract;
using Lean.Test.Cloud.MVC.Validations.ContractAdditive;
using Lean.Test.Cloud.MVC.Validations.PositionsSalaries;
using Lean.Test.Cloud.MVC.Validations.Tasks;
using Lean.Test.Cloud.MVC.Validations.Skills;
using Lean.Test.Cloud.MVC.Validations.Vacancies;
using Lean.Test.Cloud.MVC.Validations.Groups;
using Lean.Test.Cloud.MVC.Validations.Elements;
using Lean.Test.Cloud.MVC.Validations.SystemMenus;
using Lean.Test.Cloud.MVC.Validations.Profiles;
using Lean.Test.Cloud.MVC.Validations.Supports;
using Lean.Test.Cloud.MVC.Validations.TestLogs;
using Lean.Test.Cloud.MVC.Validations.TestScenarios;
using Lean.Test.Cloud.MVC.Validations.TestCases;
using Lean.Test.Cloud.MVC.Validations.TestScenarioFeatures;




namespace Lean.Test.Cloud.MVC.Infrastructure
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