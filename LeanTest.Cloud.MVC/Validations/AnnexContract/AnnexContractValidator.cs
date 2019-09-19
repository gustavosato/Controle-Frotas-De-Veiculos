using FluentValidation;
using Lean.Test.Cloud.MVC.Models.AnnexContracts;

namespace Lean.Test.Cloud.MVC.Validations.AnnexContract
{
    public class AnnexContractValidator : AbstractValidator<AnnexContractModel>
    {
        public AnnexContractValidator()
        {
            // Campos obrigatórios
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExtencionPeriodID).NotEmpty().WithMessage("O campo é obrigatório");

            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.Summary).MaximumLength(60).WithMessage("O campo excedeu o limite de  60 caracteres");

        }
    }
}