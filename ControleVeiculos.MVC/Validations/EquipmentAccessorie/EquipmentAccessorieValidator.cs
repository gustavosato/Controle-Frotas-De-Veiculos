using FluentValidation;
using Lean.Test.Cloud.MVC.Models.EquipmentAccessories;

namespace Lean.Test.Cloud.MVC.Validations.EquipmentAccessories
{
    public class EquipmentAccessorieValidator : AbstractValidator<EquipmentAccessorieModel>
    {
        public EquipmentAccessorieValidator()
        {
            //Campos obrigatórios
            RuleFor(x => x.ModelNames).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SerialNumbers).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.AssignToID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("O campo é obrigatório");

            //Limitação de caracteres
            RuleFor(x => x.SerialNumbers).MaximumLength(50).WithMessage("O campo excedeu o limite de  50 caracteres");
            RuleFor(x => x.ModelNames).MaximumLength(50).WithMessage("O campo excedeu o limite de  50 caracteres"); 

        }
    }
}