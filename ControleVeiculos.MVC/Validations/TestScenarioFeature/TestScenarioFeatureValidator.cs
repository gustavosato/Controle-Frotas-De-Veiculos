using FluentValidation;
using Lean.Test.Cloud.MVC.Models.TestScenarioFeatures;

namespace Lean.Test.Cloud.MVC.Validations.TestScenarioFeatures
{
    public class TestScenarioFeatureValidator : AbstractValidator<TestScenarioFeatureModel>
    {
        public TestScenarioFeatureValidator()
        {
            
            RuleFor(x => x.FeatureName).NotEmpty().WithMessage("O campo é obrigatório");
           
        }
    }
}