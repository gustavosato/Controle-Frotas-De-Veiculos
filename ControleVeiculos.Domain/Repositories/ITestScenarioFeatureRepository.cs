using ControleVeiculos.Domain.Command.TestScenarioFeatures;
using ControleVeiculos.Domain.Command.Features;
using ControleVeiculos.Domain.Entities.TestScenarioFeatures;
using ControleVeiculos.Domain.Entities.Features;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ITestScenarioFeatureRepository
    {
        void Add(TestScenarioFeature TestScenarioFeature);
        void Delete(int testScenarioFeatureID);
        List<TestScenarioFeature> GetAllAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command);
        List<TestScenarioFeature> GetAllNoAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command);
    }
}
