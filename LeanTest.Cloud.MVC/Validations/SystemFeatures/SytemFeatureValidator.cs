using FluentValidation;
using Lean.Test.Cloud.MVC.Models.SystemFeatures;

namespace Lean.Test.Cloud.MVC.Validations.SystemFeatures
{
    public class SystemFeatureValidator : AbstractValidator<SystemFeatureModel>
    {
        public SystemFeatureValidator()
        {
            RuleFor(x => x.SystemFeatureName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SystemFeatureTypeID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}
