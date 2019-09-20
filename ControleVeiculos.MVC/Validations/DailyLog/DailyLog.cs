using FluentValidation;
using ControleVeiculos.MVC.Models.DailyLogs;

namespace ControleVeiculos.MVC.Validations.DailyLog
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