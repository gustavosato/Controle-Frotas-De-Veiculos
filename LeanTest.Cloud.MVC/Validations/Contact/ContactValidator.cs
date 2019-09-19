using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Contacts;

namespace Lean.Test.Cloud.MVC.Validations.Contacts
{
    public class ContactValidator : AbstractValidator<ContactModel>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ContactName).Length(6, 200).WithMessage("O campo excedeu o limite de 200 caracteres");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Email).Matches(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").WithMessage("O e-mail é inválido.");
            RuleFor(x => x.FunctionID).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}  