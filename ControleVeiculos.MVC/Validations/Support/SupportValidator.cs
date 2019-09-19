using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Supports;

namespace Lean.Test.Cloud.MVC.Validations.Supports
{
    public class SupportValidator : AbstractValidator<SupportModel>
    {
        public SupportValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SeverityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PriorityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AssingToID).NotEmpty().WithMessage("O campo é obrigatório");


            //Limitação de caracteres
            RuleFor(x => x.Summary).MaximumLength(200).WithMessage("O campo excedeu o limite de  200 caracteres");

        }
    }
}  