using FluentValidation;
using ControleVeiculos.MVC.Models.ApplicationSystems;

namespace ControleVeiculos.MVC.Validations.ApplicationSystem
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