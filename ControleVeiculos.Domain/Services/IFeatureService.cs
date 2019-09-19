using Lean.Test.Cloud.Domain.Command.Features;
using Lean.Test.Cloud.Domain.Entities.Features;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IFeatureService : IDisposable
    {
        void Add(MaintenanceFeatureCommand command);
        void Update(MaintenanceFeatureCommand command);
        Result<Feature> GetByID(int featureID);
        IPagedList<Feature> GetAll(int userID, FilterFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Feature> GetAll(string applicationSystemID);
        void Delete(int featureID);
        string GetFeatureNameByID(int featureID);

    }
}
