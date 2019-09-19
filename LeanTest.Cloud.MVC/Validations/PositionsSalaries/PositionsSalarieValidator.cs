﻿using FluentValidation;
using Lean.Test.Cloud.MVC.Models.PositionsSalaries;

namespace Lean.Test.Cloud.MVC.Validations.PositionsSalaries
{
    public class PositionsSalarieValidator : AbstractValidator<PositionsSalarieModel>
    {
        public PositionsSalarieValidator()
        {
            RuleFor(x => x.FunctionID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ClassificationID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AmountPJ).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AmountCLT).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StartingDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ClosingDate).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}  