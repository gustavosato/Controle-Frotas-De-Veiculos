using Lean.Test.Cloud.Domain.Command.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Command.Features;
using Lean.Test.Cloud.Domain.Entities.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Entities.Features;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITestScenarioFeatureRepository
    {
        void Add(TestScenarioFeature TestScenarioFeature);
        void Delete(int testScenarioFeatureID);
        List<TestScenarioFeature> GetAllAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command);
        List<TestScenarioFeature> GetAllNoAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command);
    }
}
