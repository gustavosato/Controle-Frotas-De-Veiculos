using ControleVeiculos.Domain.Command.Supports;
using System;

namespace ControleVeiculos.Domain.Entities.Supports
{
    public static class SupportExtensions
    {
        public static Result<Support> GetMovimentEmployee(this Support support)
        {
            return Result.Ok(0, "", support);
        }

        public static Support Map(this Support support, MaintenanceSupportCommand command)
        {

            support.supportID = command.SupportID;
            support.summary = command.Summary;
            support.description = command.Description;
            support.severityID = command.SeverityID;
            support.statusID = command.StatusID;
            support.customerID = command.CustomerID;
            support.priorityID = command.PriorityID;
            support.typeID = command.TypeID;
            support.assingToID = command.AssingToID;
            support.resolutionDate = command.ResolutionDate;
            support.createdByID = command.CreatedByID;
            support.creationDate = command.CreationDate;
            support.modifiedByID = command.ModifiedByID;
            support.lastModifiedDate = command.LastModifiedDate;

            return support;
        }
    }
}
