using ControleVeiculos.Domain.Command.ApplicationSystems;
using System;

namespace ControleVeiculos.Domain.Entities.ApplicationSystems
{
    public static class ApplicationSystemExtensions
    {
        public static Result<ApplicationSystem> GetApplicationSystem(this ApplicationSystem applicationSystem)
        {
            return Result.Ok(0, "", applicationSystem);
        }

        public static ApplicationSystem Map(this ApplicationSystem applicationSystem, MaintenanceApplicationSystemCommand command)
        {

            applicationSystem.applicationSystemID = command.ApplicationSystemID;
            applicationSystem.applicationSystemName = command.ApplicationSystemName;
            applicationSystem.description = command.Description;
            applicationSystem.applicationTypeID = command.ApplicationTypeID;
            applicationSystem.customerID = command.CustomerID;
            applicationSystem.creationDate = command.CreationDate;
            applicationSystem.modifiedByID = command.ModifiedByID;
            applicationSystem.lastModifiedDate = command.LastModifiedDate;

            return applicationSystem;
        }
    }
}
