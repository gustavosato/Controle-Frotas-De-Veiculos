using Lean.Test.Cloud.Domain.Command.SystemFeatures;
using System;

namespace Lean.Test.Cloud.Domain.Entities.SystemFeatures
{
    public static class SystemFeatureExtensions
    {
        public static Result<SystemFeature> GetSystemFeature(this SystemFeature systemFeature)
        {
            return Result.Ok(0, "", systemFeature);
        }

        public static SystemFeature Map(this SystemFeature systemFeature, MaintenanceSystemFeatureCommand command)
        {

            systemFeature.systemFeatureID = command.SystemFeatureID;
            systemFeature.systemFeatureName = command.SystemFeatureName;
            systemFeature.systemFeatureTypeID = command.SystemFeatureTypeID;
            systemFeature.createdByID = command.CreatedByID;
            systemFeature.creationDate = command.CreationDate;
            systemFeature.modifiedByID = command.ModifiedByID;
            systemFeature.lastModifiedDate = command.LastModifiedDate;

            return systemFeature;
        }
    }
}
