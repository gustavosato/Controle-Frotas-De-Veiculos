using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.SystemFeatures;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.SystemFeatures;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class SystemFeatureService : BaseAppService, ISystemFeatureService
    {
        private readonly ISystemFeatureRepository _systemFeatureRepository;

        public SystemFeatureService(ISystemFeatureRepository systemFeatureRepository)
        {
            _systemFeatureRepository = systemFeatureRepository;
        }

        public void Add(MaintenanceSystemFeatureCommand command)
        {
            SystemFeature systemFeature = new SystemFeature();

            systemFeature = systemFeature.Map(command);

            _systemFeatureRepository.Add(systemFeature);
        }

        public void Update(MaintenanceSystemFeatureCommand command)
        {
            SystemFeature systemFeature = new SystemFeature();

            systemFeature = systemFeature.Map(command);

            _systemFeatureRepository.Update(systemFeature);
        }

        public Result<SystemFeature> GetByID(int systemFeatureID)
        {
            var systemFeature = _systemFeatureRepository.GetByID(systemFeatureID);

            return Result.Ok<SystemFeature>(0, "", systemFeature);
        }

        public IPagedList<SystemFeature> GetAll(FilterSystemFeatureCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var systemFeature = _systemFeatureRepository.GetAll(command);

            return new PagedList<SystemFeature>(systemFeature, pageIndex, pageSize);
        }

        public IList<SystemFeature> GetAll()
        {
            var systemFeature = _systemFeatureRepository.GetAll();

            return new List<SystemFeature>(systemFeature);
        }

        public void Delete(int systemFeatureID)
        {
            _systemFeatureRepository.Delete(systemFeatureID);
        }
    }
}

