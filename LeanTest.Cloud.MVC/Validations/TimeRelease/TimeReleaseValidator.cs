using FluentValidation;
using Lean.Test.Cloud.MVC.Models.TimeReleases;

namespace Lean.Test.Cloud.MVC.Validations.TimeRelease
{
    public class TimeReleaseValidator : AbstractValidator<TimeReleaseModel>
    {
        public TimeReleaseValidator()
        {
            RuleFor(x => x.RegisterDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StartWork).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.EndWork).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.DemandID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ActivityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Description).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("A descrição das atividades devem conter mais que 10 caracters");

        }
    }
}