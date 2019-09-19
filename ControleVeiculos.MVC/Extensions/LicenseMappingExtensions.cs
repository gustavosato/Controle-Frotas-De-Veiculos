using Lean.Test.Cloud.Domain.Entities.Licenses;
using Lean.Test.Cloud.MVC.Models.Licenses;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class LicenseMappingExtensions
    {
        public static LicenseModel ToModel(this License entity)
        {
            if (entity == null)
                return null;

            var model = new LicenseModel
            {
                LicenseID = entity.licenseID,
                LicenseCode = entity.licenseCode,
                Description = entity.description,
                License = entity.license,
                CustomerID = entity.customerID,
                LicenseTypeID = entity.licenseTypeID,
                ExpirationDate = entity.expirationDate,
                HostName = entity.hostName,
                MacAddress = entity.macAddress,
                ApprovedByID = entity.approvedByID,
                ApprovedDate = entity.approvedDate,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}