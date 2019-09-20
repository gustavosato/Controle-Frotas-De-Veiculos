using FluentValidation;
using ControleVeiculos.MVC.Models.Expenses;

namespace ControleVeiculos.MVC.Validations.Expenses
{
    public class ExpenseValidator : AbstractValidator<ExpenseModel>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.RegisterDate).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.TypeExpenseID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.AmountExpense).NotNull().WithMessage("Preencha o valor da despesa");


            //RuleFor(x => x.AmountExpense).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.DepartmentID).NotEmpty().WithMessage("Campo obrigatório.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Campo obrigatório.");


            //Limitação de caracteres
            RuleFor(x => x.SubTotal).MaximumLength(30).WithMessage("O campo excedeu o limite de  30 caracteres");
            RuleFor(x => x.Kilometer).MaximumLength(30).WithMessage("O campo excedeu o limite de  30 caracteres");


        }
    }
}