﻿using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Parameters;

namespace Lean.Test.Cloud.MVC.Validations.Parameters
{
    public class ParameterValidator : AbstractValidator<ParameterModel>
    {
        public ParameterValidator()
        {
            RuleFor(x => x.ParameterName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ParameterName).MaximumLength(50).WithMessage("O campo excedeu o limite de  50 caracteres"); ;
            RuleFor(x => x.SystemFeatureID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}