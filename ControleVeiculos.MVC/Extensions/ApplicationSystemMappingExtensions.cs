using Lean.Test.Cloud.Domain.Entities.ApplicationSystems;
using Lean.Test.Cloud.MVC.Models.ApplicationSystems;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class ApplicationSystemMappingExtensions
    {
        public static ApplicationSystemModel ToModel(this ApplicationSystem entity)
        {
            if (entity == null)
                return null;

            var model = new ApplicationSystemModel
            {
                ApplicationSystemID = entity.applicationSystemID,
                ApplicationSystemName = entity.applicationSystemName,
                CustomerID = entity.customerID,
                Description = entity.description,
                ApplicationTypeID = entity.applicationTypeID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}