using FluentValidation;
using ControleVeiculos.MVC.Models.AccountingEntries;

namespace ControleVeiculos.MVC.Validations.AccountingEntries
{
    public class AccountingEntrieValidator : AbstractValidator<AccountingEntrieModel>
    {
        public AccountingEntrieValidator()
        {
            RuleFor(x => x.ValueToBeRealized).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo obrigatório.");
            RuleFor(x => x.DemandID).NotEmpty().WithMessage("O campo obrigatório.");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo obrigatório.");
            RuleFor(x => x.CompetitionDate).NotEmpty().WithMessage("O campo obrigatório.");

        }
    }
}