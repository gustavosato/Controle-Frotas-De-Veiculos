using FluentValidation;
using Lean.Test.Cloud.MVC.Models.TestLogs;

namespace Lean.Test.Cloud.MVC.Validations.TestLogs
{
    public class TestLogValidator : AbstractValidator<TestLogModel>
    {
        public TestLogValidator()
        {
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StepName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExpectedResult).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ActualResult).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PathEvidence).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}