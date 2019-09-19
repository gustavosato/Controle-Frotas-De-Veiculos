using Lean.Test.Cloud.Domain.Command.SystemFeatures;
using Lean.Test.Cloud.Domain.Entities.SystemFeatures;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ISystemFeatureRepository
    {
        void Add(SystemFeature systemFeature);
        void Update(SystemFeature systemFeature);
        SystemFeature GetByID(int systemFeatureID);
        List<SystemFeature> GetAll(FilterSystemFeatureCommand command);
        List<SystemFeature> GetAll();
        void Delete(int systemFeatureID);
    }
}
