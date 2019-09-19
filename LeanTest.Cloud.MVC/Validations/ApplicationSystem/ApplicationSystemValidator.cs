using FluentValidation;
using Lean.Test.Cloud.MVC.Models.ApplicationSystems;

namespace Lean.Test.Cloud.MVC.Validations.ApplicationSystem
{
    public class ApplicationSystemValidator : AbstractValidator<ApplicationSystemModel>
    {
        public ApplicationSystemValidator()
        {
            RuleFor(x => x.ApplicationSystemName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ApplicationSystemName).MaximumLength(200); 
            RuleFor(x => x.SearchApplicationSystemName).MaximumLength(200); 
            RuleFor(x => x.ApplicationTypeID).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}