using FluentValidation;
using ControleVeiculos.MVC.Models.Skills;

namespace ControleVeiculos.MVC.Validations.Skills
{
    public class SkillValidator : AbstractValidator<SkillModel>
    {
        public SkillValidator()
        {
            // Campos obrigatórios
            RuleFor(x => x.Summary).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SkillTypeID).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.Summary).MaximumLength(250).WithMessage("O campo excedeu o limite de  250 caracteres"); 
        }
    }
}