using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Features;

namespace Lean.Test.Cloud.MVC.Validations.Features
{
    public class FeatureValidator : AbstractValidator<FeatureModel>
    {
        public FeatureValidator()
        {
            RuleFor(x => x.FeatureName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FeatureName).MaximumLength(100).WithMessage("O campo excedeu o limite de  100 caracteres");

            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ApplicationSystemID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.DeveloperID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FeatureTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TimeEffort).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TargetDate).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}