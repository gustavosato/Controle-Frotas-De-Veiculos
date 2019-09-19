using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Contacts;

namespace Lean.Test.Cloud.MVC.Validations.Contacts
{
    public class ContactValidator : AbstractValidator<ContactModel>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FunctionID).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}  