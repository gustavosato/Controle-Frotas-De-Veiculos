using FluentValidation;
using Lean.Test.Cloud.MVC.Models.ContractAdditives;

namespace Lean.Test.Cloud.MVC.Validations.ContractAdditive
{
    public class ContractAdditiveValidator : AbstractValidator<ContractAdditiveModel>
    {
        public ContractAdditiveValidator()
        {
            //Campos Obrigatórios
            RuleFor(x => x.ContractID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PeriodValidityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExtencionID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExtencionPeriodID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ResetModalityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AdditiveObject).NotEmpty().WithMessage("O campo é obrigatório");


            //Limitação de caracteres
            RuleFor(x => x.BillingCondition).MaximumLength(50).WithMessage("O campo excedeu o limite de  50 caracteres");

        }
    }
}