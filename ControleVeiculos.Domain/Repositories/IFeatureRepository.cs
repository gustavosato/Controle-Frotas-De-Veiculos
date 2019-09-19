using Lean.Test.Cloud.Domain.Command.Features;
using Lean.Test.Cloud.Domain.Entities.Features;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IFeatureRepository
    {
        void Add(Feature feature);
        void Update(Feature feature);
        Feature GetByID(int featureID);
        List<Feature> GetAll(int userID, FilterFeatureCommand command);
        List<Feature> GetAll(string applicationSystemID);
        void Delete(int featureID);
        string GetFeatureNameByID(int featureID);

    }
}
