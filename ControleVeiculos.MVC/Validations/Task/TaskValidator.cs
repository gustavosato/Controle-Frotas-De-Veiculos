using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Tasks;

namespace Lean.Test.Cloud.MVC.Validations.Tasks
{
    public class TaskValidator : AbstractValidator<TaskModel>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TargetDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AssignToID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.DemandID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}