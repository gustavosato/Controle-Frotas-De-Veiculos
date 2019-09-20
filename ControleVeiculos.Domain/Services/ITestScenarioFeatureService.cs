using ControleVeiculos.Domain.Command.TestScenarioFeatures;
using ControleVeiculos.Domain.Command.Features;
using ControleVeiculos.Domain.Entities.TestScenarioFeatures;
using ControleVeiculos.Domain.Entities.Features;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ITestScenarioFeatureService : IDisposable
    {
        void Add(MaintenanceTestScenarioFeatureCommand command);
        void Delete(int testScenarioFeatureID);
        IPagedList<TestScenarioFeature> GetAllAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<TestScenarioFeature> GetAllNoAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
