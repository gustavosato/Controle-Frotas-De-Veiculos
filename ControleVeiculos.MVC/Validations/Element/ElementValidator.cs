using FluentValidation;
using ControleVeiculos.MVC.Models.Elements;

namespace ControleVeiculos.MVC.Validations.Elements
{
    public class ElementValidator : AbstractValidator<ElementModel>
    {
        public ElementValidator()
        {
            RuleFor(x => x.Element).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ActionID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.DefaultValue).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AutomationID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TypeIdentificationID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FeatureID).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}  