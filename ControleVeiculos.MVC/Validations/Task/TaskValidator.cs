using FluentValidation;
using ControleVeiculos.MVC.Models.Tasks;

namespace ControleVeiculos.MVC.Validations.Tasks
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