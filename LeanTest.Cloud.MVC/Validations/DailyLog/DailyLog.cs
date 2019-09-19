using FluentValidation;
using Lean.Test.Cloud.MVC.Models.DailyLogs;

namespace Lean.Test.Cloud.MVC.Validations.DailyLog
{
    public class DailyLogValidator : AbstractValidator<DailyLogModel>
    {
        public DailyLogValidator()
        {
            RuleFor(x => x.DemandID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Description).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}