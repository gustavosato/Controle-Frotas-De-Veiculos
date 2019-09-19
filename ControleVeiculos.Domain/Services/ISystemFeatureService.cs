using Lean.Test.Cloud.Domain.Command.SystemFeatures;
using Lean.Test.Cloud.Domain.Entities.SystemFeatures;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ISystemFeatureService : IDisposable
    {
        void Add(MaintenanceSystemFeatureCommand command);
        void Update(MaintenanceSystemFeatureCommand command);
        Result<SystemFeature> GetByID(int systemFeatureID);
        IPagedList<SystemFeature> GetAll(FilterSystemFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<SystemFeature> GetAll();
        void Delete(int systemFeatureID);
    }
}
