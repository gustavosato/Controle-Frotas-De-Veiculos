using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Features;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Features;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class FeatureService : BaseAppService, IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public void Add(MaintenanceFeatureCommand command)
        {
            Feature feature = new Feature();

            feature = feature.Map(command);

            _featureRepository.Add(feature);
        }

        public void Update(MaintenanceFeatureCommand command)
        {
            Feature feature = new Feature();

            feature = feature.Map(command);

            _featureRepository.Update(feature);
        }

        public Result<Feature> GetByID(int featureID)
        {
            var feature = _featureRepository.GetByID(featureID);

            return Result.Ok<Feature>(0, "", feature);
        }

        public IPagedList<Feature> GetAll(int userID, FilterFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var feature = _featureRepository.GetAll(userID, command);

            return new PagedList<Feature>(feature, pageIndex, pageSize);
        }

        public IList<Feature> GetAll(string applicationSystemID)
        {
            var user = _featureRepository.GetAll(applicationSystemID);

            return new List<Feature>(user);
        }

        public void Delete(int featureID)
        {
            _featureRepository.Delete(featureID);
        }

        public string GetFeatureNameByID(int featureID)
        {
            return _featureRepository.GetFeatureNameByID(featureID);
        }
    }
}

