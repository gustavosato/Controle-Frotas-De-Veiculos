using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Entities.Features;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Command.Features;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class TestScenarioFeatureService : BaseAppService, ITestScenarioFeatureService
    {
        private readonly ITestScenarioFeatureRepository _testScenarioFeatureRepository;

        public TestScenarioFeatureService(ITestScenarioFeatureRepository testScenarioFeatureRepository)
        {
            _testScenarioFeatureRepository = testScenarioFeatureRepository;
        }

        public void Add(MaintenanceTestScenarioFeatureCommand command)
        {
            TestScenarioFeature testScenarioFeature = new TestScenarioFeature();

            testScenarioFeature = testScenarioFeature.Map(command);

            _testScenarioFeatureRepository.Add(testScenarioFeature);
        }
       
        public void Delete(int testScenarioFeatureID)
        {
            _testScenarioFeatureRepository.Delete(testScenarioFeatureID);

        }

        public IPagedList<TestScenarioFeature> GetAllAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var features = _testScenarioFeatureRepository.GetAllAssociateTestScenarioByFeatureID(command);

            return new PagedList<TestScenarioFeature>(features, pageIndex, pageSize);
        }

        public IPagedList<TestScenarioFeature> GetAllNoAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var features = _testScenarioFeatureRepository.GetAllNoAssociateTestScenarioByFeatureID(command);

            return new PagedList<TestScenarioFeature>(features, pageIndex, pageSize);
        }
    }
}

