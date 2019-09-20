using FluentValidation;
using ControleVeiculos.MVC.Models.Customers;

namespace ControleVeiculos.MVC.Validations.Customer
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            //Campos obrigatórios
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SegmentID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.Site).MaximumLength(50).WithMessage("O campo excedeu o limite de  50 caracteres");
            RuleFor(x => x.Address).MaximumLength(200).WithMessage("O campo excedeu o limite de  200 caracteres");
            RuleFor(x => x.CustomerName).MaximumLength(60).WithMessage("O campo excedeu o limite de  60 caracteres");


        }
    }
}