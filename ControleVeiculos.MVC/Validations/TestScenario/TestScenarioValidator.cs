using FluentValidation;
using ControleVeiculos.MVC.Models.TestScenarios;

namespace ControleVeiculos.MVC.Validations.TestScenarios
{
    public class TestScenarioValidator : AbstractValidator<TestScenarioModel>
    {
        public TestScenarioValidator()
        {
            RuleFor(x => x.TestScenario).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TestScenario).MaximumLength(200).WithMessage("O nome do cenário não deve conter mais que 200 caracteres");
            RuleFor(x => x.TestTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExecutionTypeID).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}