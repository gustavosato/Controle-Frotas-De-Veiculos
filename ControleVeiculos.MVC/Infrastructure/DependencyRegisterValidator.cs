using Autofac;
using FluentValidation;
using ControleVeiculos.MVC.Models.Users;
using ControleVeiculos.MVC.Models.ApplicationSystems;
using ControleVeiculos.MVC.Models.Features;
using ControleVeiculos.MVC.Models.SystemParameter;
using ControleVeiculos.MVC.Models.SystemFeatures;
using ControleVeiculos.MVC.Models.TestPackages;
using ControleVeiculos.MVC.Models.Parameters;
using ControleVeiculos.MVC.Models.ParameterValues;
using ControleVeiculos.MVC.Models.Vacancies;
using ControleVeiculos.MVC.Models.Groups;
using ControleVeiculos.MVC.Models.Elements;
using ControleVeiculos.MVC.Models.SystemMenus;
using ControleVeiculos.MVC.Models.Profiles;
using ControleVeiculos.MVC.Models.TestLogs;
using ControleVeiculos.MVC.Models.TestScenarios;
using ControleVeiculos.MVC.Models.TestCases;

using ControleVeiculos.MVC.Validations.Users;
using ControleVeiculos.MVC.Validations.ApplicationSystem;
using ControleVeiculos.MVC.Validations.Features;
using ControleVeiculos.MVC.Validations.SystemParameters;
using ControleVeiculos.MVC.Validations.SystemFeatures;
using ControleVeiculos.MVC.Validations.TestPackages;
using ControleVeiculos.MVC.Validations.Parameters;
using ControleVeiculos.MVC.Validations.ParameterValues;
using ControleVeiculos.MVC.Validations.Vacancies;
using ControleVeiculos.MVC.Validations.Groups;
using ControleVeiculos.MVC.Validations.Elements;
using ControleVeiculos.MVC.Validations.SystemMenus;
using ControleVeiculos.MVC.Validations.Profiles;
using ControleVeiculos.MVC.Validations.TestLogs;
using ControleVeiculos.MVC.Validations.TestScenarios;
using ControleVeiculos.MVC.Validations.TestCases;


namespace ControleVeiculos.Infrastructure
{
    public static class DependencyRegisterValidator
    {
        public static ContainerBuilder RegisterValidator(this ContainerBuilder builder)
        {

            builder.RegisterType<UserValidator>()
                .Keyed<IValidator>(typeof(IValidator<UserModel>))
                .As<IValidator>();
            
            builder.RegisterType<ApplicationSystemValidator>()
                .Keyed<IValidator>(typeof(IValidator<ApplicationSystemModel>))
                .As<IValidator>();

            builder.RegisterType<FeatureValidator>()
                .Keyed<IValidator>(typeof(IValidator<FeatureModel>))
                .As<IValidator>();
           
            builder.RegisterType<SystemParameterValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemParameterModel>))
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
           
            builder.RegisterType<TestLogValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestLogModel>))
                .As<IValidator>();

            builder.RegisterType<TestScenarioValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestScenarioModel>))
                .As<IValidator>();

            builder.RegisterType<TestCaseValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestCaseModel>))
                .As<IValidator>();
           
            return builder;
        }
    }
}