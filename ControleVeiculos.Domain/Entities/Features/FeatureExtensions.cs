using ControleVeiculos.Domain.Command.Features;
using System;

namespace ControleVeiculos.Domain.Entities.Features
{
    public static class FeatureExtensions
    {
        public static Result<Feature> GetFeature(this Feature feature)
        {
            return Result.Ok(0, "", feature);
        }

        public static Feature Map(this Feature feature, MaintenanceFeatureCommand command)
        {

            feature.featureID = command.FeatureID;
            feature.featureName = command.FeatureName;
            feature.statusID = command.StatusID;
            feature.description = command.Description;
            feature.customerID = command.CustomerID;
            feature.applicationSystemID = command.ApplicationSystemID;
            feature.developerID = command.DeveloperID;
            feature.featureTypeID = command.FeatureTypeID;
            feature.metaScript = command.MetaScript;
            feature.automationScript = command.AutomationScript;
            feature.testPoints = command.TestPoints;
            feature.targetDate = command.TargetDate;
            feature.timeEffort = command.TimeEffort;
            feature.createdByID = command.CreatedByID;
            feature.creationDate = command.CreationDate;
            feature.modifiedByID = command.ModifiedByID;
            feature.lastModifiedDate = DateTime.Now.ToString();

            return feature;
        }
    }
}
