using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Defects;

namespace Lean.Test.Cloud.MVC.Validations.Defects
{
    public class DefectValidator : AbstractValidator<DefectModel>
    {
        public DefectValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Summary).MaximumLength(200).WithMessage("O campo excedeu o limite de  200 caracteres"); ;

            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SeverityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PriorityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CreatedByID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Description).NotEmpty().WithMessage("O campo é obrigatório");


        }
    }
}  