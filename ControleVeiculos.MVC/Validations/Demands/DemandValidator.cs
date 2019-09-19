
using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Demands;

namespace Lean.Test.Cloud.MVC.Validations.Demands
{
    public class DemandValidator : AbstractValidator<DemandModel>
    {
        public DemandValidator()
        {
            //Campos obrigatórios
            RuleFor(x => x.DemandName).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.PlanningStartDate).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.PlanningEndDate).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.ManagementEffort).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.PlanningEffort).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.ExecutionEffort).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.ServiceID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.AssignToTargetID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.ResponsibleID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.OportunityID).NotEmpty().WithMessage("Campo obrigatório.");

            //Limitação de caracteres
            RuleFor(x => x.DemandName).MaximumLength(100).WithMessage("O campo excedeu o limite de  100 caracteres");
            RuleFor(x => x.PlanningEffort).MaximumLength(20).WithMessage("O campo excedeu o limite de  20 caracteres");
            RuleFor(x => x.ExecutionEffort).MaximumLength(20).WithMessage("O campo excedeu o limite de  20 caracteres");
            RuleFor(x => x.ManagementEffort).MaximumLength(20).WithMessage("O campo excedeu o limite de  20 caracteres");
            RuleFor(x => x.ExternalCode).MaximumLength(20).WithMessage("O campo excedeu o limite de  20 caracteres");

        }
    }
}