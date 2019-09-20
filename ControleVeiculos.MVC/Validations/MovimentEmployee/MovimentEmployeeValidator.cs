using FluentValidation;
using ControleVeiculos.MVC.Models.MovimentEmployees;

namespace ControleVeiculos.MVC.Validations.MovimentEmployee
{
    public class MovimentEmployeeValidator : AbstractValidator<MovimentEmployeeModel>
    {
        public MovimentEmployeeValidator()
        {
            RuleFor(x => x.EmployeeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.MovimentEmployeeTypeID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}