using FluentValidation;
using Lean.Test.Cloud.MVC.Models.SystemParameter;

namespace Lean.Test.Cloud.MVC.Validations.SystemParameters
{
    public class SystemParameterValidator : AbstractValidator<SystemParameterModel>
    {
        public SystemParameterValidator()
        {
            // Campos obrigatórios
            RuleFor(x => x.ParamterName).NotEmpty().WithMessage("O campo é obrigatório");

            RuleFor(x => x.ParamterValue).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.ParamterName).MaximumLength(50).WithMessage("O campo excedeu o limite de  50 caracteres"); ;
            RuleFor(x => x.ParamterValue).MaximumLength(30).WithMessage("O campo excedeu o limite de  30 caracteres"); ;
            RuleFor(x => x.ParamterDefaultValue).MaximumLength(30).WithMessage("O campo excedeu o limite de  30 caracteres"); ;
        }
    }
}