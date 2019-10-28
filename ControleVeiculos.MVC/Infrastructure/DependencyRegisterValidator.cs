﻿using Autofac;
using FluentValidation;
using ControleVeiculos.MVC.Models.Users;
using ControleVeiculos.MVC.Models.ApplicationSystems;
using ControleVeiculos.MVC.Models.SystemParameter;
using ControleVeiculos.MVC.Models.SystemFeatures;
using ControleVeiculos.MVC.Models.Parameters;
using ControleVeiculos.MVC.Models.SystemMenus;
using ControleVeiculos.MVC.Models.TestCases;
using ControleVeiculos.MVC.Models.Funcionarios;
using ControleVeiculos.MVC.Models.ParameterValues;
using ControleVeiculos.MVC.Models.Veiculos;


using ControleVeiculos.MVC.Validations.Users;
using ControleVeiculos.MVC.Validations.ApplicationSystem;
using ControleVeiculos.MVC.Validations.SystemParameters;
using ControleVeiculos.MVC.Validations.SystemFeatures;
using ControleVeiculos.MVC.Validations.Parameters;
using ControleVeiculos.MVC.Validations.ParameterValues;
using ControleVeiculos.MVC.Validations.SystemMenus;
using ControleVeiculos.MVC.Validations.TestCases;
using ControleVeiculos.MVC.Validations.Funcionarios;
using ControleVeiculos.MVC.Validations.Veiculos;

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
                        
            builder.RegisterType<SystemParameterValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemParameterModel>))
                .As<IValidator>();

            builder.RegisterType<SystemFeatureValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemFeatureModel>))
                .As<IValidator>();
                        
            builder.RegisterType<ParameterValidator>()
                .Keyed<IValidator>(typeof(IValidator<ParameterModel>))
                .As<IValidator>();

            builder.RegisterType<ParameterValueValidator>()
                .Keyed<IValidator>(typeof(IValidator<ParameterValueModel>))
                .As<IValidator>();
                       
            builder.RegisterType<SystemMenuValidator>()
                .Keyed<IValidator>(typeof(IValidator<SystemMenuModel>))
               .As<IValidator>();
                       
            builder.RegisterType<TestCaseValidator>()
                .Keyed<IValidator>(typeof(IValidator<TestCaseModel>))
                .As<IValidator>();

            builder.RegisterType<FuncionarioValidator>()
                .Keyed<IValidator>(typeof(IValidator<FuncionarioModel>))
                .As<IValidator>();

            builder.RegisterType<VeiculoValidator>()
                .Keyed<IValidator>(typeof(IValidator<VeiculoModel>))
                .As<IValidator>();


            return builder;
        }
    }
}