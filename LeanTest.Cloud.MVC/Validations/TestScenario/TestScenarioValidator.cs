using FluentValidation;
using Lean.Test.Cloud.MVC.Models.TestScenarios;

namespace Lean.Test.Cloud.MVC.Validations.TestScenarios
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