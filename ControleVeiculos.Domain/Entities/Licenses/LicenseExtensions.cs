using ControleVeiculos.Domain.Command.Licenses;
using System;

namespace ControleVeiculos.Domain.Entities.Licenses
{
    public static class LicenseExtensions
    {
        public static Result<License> GetLicense(this License license)
        {
            return Result.Ok(0, "", license);
        }

        public static License Map(this License license, MaintenanceLicenseCommand command)
        {

            license.licenseID = command.LicenseID;
            license.licenseCode = command.LicenseCode;
            license.customerID = command.CustomerID;
            license.expirationDate = command.ExpirationDate;
            license.license = command.License;
            license.licenseTypeID = command.LicenseTypeID;
            license.hostName = command.HostName;
            license.macAddress = command.MacAddress;
            license.description = command.Description;
            license.approvedByID = command.ApprovedByID;
            license.approvedDate = command.ApprovedDate;
            license.createdByID = command.CreatedByID;
            license.creationDate = command.CreationDate;
            license.modifiedByID = command.ModifiedByID;
            license.lastModifiedDate = command.LastModifiedDate;

            return license;
        }
    }
}
