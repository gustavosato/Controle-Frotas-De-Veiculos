using FluentValidation;
using ControleVeiculos.MVC.Models.ParameterValues;

namespace ControleVeiculos.MVC.Validations.ParameterValues
{
    public class ParameterValueValidator : AbstractValidator<ParameterValueModel>
    {
        public ParameterValueValidator()
        {
            RuleFor(x => x.ParameterValue).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ParameterID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ParentID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}