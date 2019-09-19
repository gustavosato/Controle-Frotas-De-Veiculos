using Lean.Test.Cloud.Domain.Entities.Historicals;
using Lean.Test.Cloud.MVC.Models.Historicals;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class HistoricalMappingExtensions
    {
        public static HistoricalModel ToModel(this Historical entity)
        {
            if (entity == null)
                return null;

            var model = new HistoricalModel
            {
                HistoricalID = entity.historicalID,
                OldValue = entity.oldValue,
                NewValue = entity.newValue,
                RecordID = entity.recordID,
                SystemFeatureID = entity.systemFeatureID,
                FieldName = entity.fieldName,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}