using FluentValidation;
using ControleVeiculos.MVC.Models.TestPackages;

namespace ControleVeiculos.MVC.Validations.TestPackages
{
    public class TestPackageValidator : AbstractValidator<TestPackageModel>
    {
        public TestPackageValidator()
        {
            RuleFor(x => x.PackageName).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.PackageName).MaximumLength(50).WithMessage("O nome do pacote não deve conter mais que 50 caracteres");
            RuleFor(x => x.TecnologyID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.BrowserID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.MethodologyID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.DemandID).NotEmpty().WithMessage("O campo é obrigatório");

        }
    }
}