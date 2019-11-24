using FluentValidation;
using ControleVeiculos.MVC.Models.Reservas;

namespace ControleVeiculos.MVC.Validations.Reservas
{
    public class ReservaValidator : AbstractValidator<ReservaModel>
    {
        public ReservaValidator()
        {
            //RuleFor(x => x.Release).MaximumLength(50).WithMessage("A release não deve conter mais que 50 caracteres");
            //RuleFor(x => x.Cycle).MaximumLength(50).WithMessage("O cycle do teste não deve conter mais que 50 caracteres");
            //RuleFor(x => x.Precondition).MaximumLength(200).WithMessage("A pre condição não deve conter mais que 200 caracteres");
            //RuleFor(x => x.ExpectedResult).MaximumLength(200).WithMessage("O resultado esperado não deve conter mais que 200 caracteres");
            //RuleFor(x => x.TestCase).MaximumLength(200).WithMessage("O nome do teste não deve conter mais que 200 caracteres");
            //RuleFor(x => x.TestCase).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.TestTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.FlowTestID).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.FeatureID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}