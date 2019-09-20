using FluentValidation;
using ControleVeiculos.MVC.Models.SystemMenus;

namespace ControleVeiculos.MVC.Validations.SystemMenus
{
    public class SystemMenuValidator : AbstractValidator<SystemMenuModel>
    {
        public SystemMenuValidator()
        {
            RuleFor(x => x.TextMenu).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TextMenu).MaximumLength(50).WithMessage("O campo execedeu o limite de 50 caracteres");
            RuleFor(x => x.SystemFeatureID).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}
