using FluentValidation;
using ControleVeiculos.MVC.Models.Contracts;
using System;

namespace ControleVeiculos.MVC.Validations.Contract
{
    public class ContractValidator : AbstractValidator<ContractModel>
    {
        public ContractValidator()
        {
            RuleFor(x => x.OportunityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ContractTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ContractorCustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ContractingCustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("O campo é obrigatório");
            
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.EndDate).NotEqual(X => X.StartDate);

            RuleFor(x => x.PeriodValidityID).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.BillingCondition).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.BillingCondition).MaximumLength(200).WithMessage("O campo excedeu o limite de  200 caracteres");
            RuleFor(x => x.ResetModalityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExtencionID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExtencionPeriodID).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.ObjectContract).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}