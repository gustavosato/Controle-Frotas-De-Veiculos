using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Pipelines;

namespace Lean.Test.Cloud.MVC.Validations.Pipelines
{
    public class PipelineValidator : AbstractValidator<PipelineModel>
    {
        public PipelineValidator()
        {
            //requeried
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.OfferID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.OwnerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CostCenterID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FaseID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Probability).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SaleManagerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PowerSponsor).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PowerSponsor).MaximumLength(150).WithMessage("O campo excedeu o limite de 150 caracteres");
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PreSalesID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Sponsor).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Sponsor).MaximumLength(150).WithMessage("O campo excedeu o limite de 150 caracteres");
            RuleFor(x => x.TargetDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.OperationManagerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PriorityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExpectedValue).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");

            //limit
            RuleFor(x => x.ExpectedValue).MaximumLength(100).WithMessage("O campo excedeu o limite de 100 caracteres");
            RuleFor(x => x.Quarter1).MaximumLength(10).WithMessage("O campo excedeu o limite de 10 caracteres");
            RuleFor(x => x.Quarter2).MaximumLength(10).WithMessage("O campo excedeu o limite de 10 caracteres");
            RuleFor(x => x.Quarter3).MaximumLength(10).WithMessage("O campo excedeu o limite de 10 caracteres");
            RuleFor(x => x.Quarter4).MaximumLength(10).WithMessage("O campo excedeu o limite de 10 caracteres");
            RuleFor(x => x.Summary).MaximumLength(200).WithMessage("O campo excedeu o limite de 200 caracteres");

        }
    }
}  