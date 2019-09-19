using Lean.Test.Cloud.Domain.Command.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Command.Features;
using Lean.Test.Cloud.Domain.Entities.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Entities.Features;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ITestScenarioFeatureService : IDisposable
    {
        void Add(MaintenanceTestScenarioFeatureCommand command);
        void Delete(int testScenarioFeatureID);
        IPagedList<TestScenarioFeature> GetAllAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<TestScenarioFeature> GetAllNoAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
