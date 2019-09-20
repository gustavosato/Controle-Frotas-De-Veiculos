using FluentValidation;
using ControleVeiculos.MVC.Models.TestScenarioFeatures;

namespace ControleVeiculos.MVC.Validations.TestScenarioFeatures
{
    public class TestScenarioFeatureValidator : AbstractValidator<TestScenarioFeatureModel>
    {
        public TestScenarioFeatureValidator()
        {
            
            RuleFor(x => x.FeatureName).NotEmpty().WithMessage("O campo é obrigatório");
           
        }
    }
}