using FluentValidation;
using ControleVeiculos.MVC.Models.Vacancies;

namespace ControleVeiculos.MVC.Validations.Vacancies
{
    public class VacancieValidator : AbstractValidator<VacancieModel>
    {
        public VacancieValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.VacanciesTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.InternalApplicantID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExternalApplicantID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ContractTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ValidityID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.WorkPlace).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.Summary).MaximumLength(250).WithMessage("O campo excedeu o limite de  250 caracteres");

        }
    }
}