using Autofac;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lean.Test.Cloud.MVC.Infrastructure
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer _container;

        public AutofacValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator = _container.ResolveOptionalKeyed<IValidator>(validatorType);

            return validator;
        }
    }
}