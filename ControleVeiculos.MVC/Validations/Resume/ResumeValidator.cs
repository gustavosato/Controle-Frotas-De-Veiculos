using FluentValidation;
using ControleVeiculos.MVC.Models.Resumes;

namespace ControleVeiculos.MVC.Validations.Resumes
{
    public class ResumeValidator : AbstractValidator<ResumeModel>
    {
        public ResumeValidator()
        {
            // Campos obrigatórios
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FunctionID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.FunctionLevelID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ExpectedSalary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.ContractTypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.MaritalStatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AvailabilityToStart).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Description).NotEmpty().WithMessage("O campo é obrigatório");


            //Limitação de caracteres
            RuleFor(x => x.Summary).MaximumLength(60).WithMessage("O campo excedeu o limite de 60 caracteres");
        }
    }
}