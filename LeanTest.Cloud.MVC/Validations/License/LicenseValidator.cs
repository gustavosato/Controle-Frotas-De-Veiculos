using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Licenses;

namespace Lean.Test.Cloud.MVC.Validations.Licenses
{
    public class LicenseValidator : AbstractValidator<LicenseModel>
    {
        public LicenseValidator()
        {
            RuleFor(x => x.LicenseCode).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExpirationDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.LicenseTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CreatedByID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}