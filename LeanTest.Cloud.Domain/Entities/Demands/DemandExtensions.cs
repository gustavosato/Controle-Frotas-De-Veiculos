using Lean.Test.Cloud.Domain.Command.Demands;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Demands
{
    public static class DemandExtensions
    {
        public static Result<Demand> GetDemand(this Demand demand)
        {
            return Result.Ok(0, "", demand);
        }

        public static Demand Map(this Demand demand, MaintenanceDemandCommand command)
        {

            demand.demandID = command.DemandID;
            demand.demandName = command.DemandName;
            demand.typeID = command.TypeID;
            demand.statusID = command.StatusID;
            demand.scope = command.Scope;
            demand.serviceID = command.ServiceID;
            demand.externalCode = command.ExternalCode;
            demand.demandCode = command.DemandCode;
            demand.responsibleID = command.ResponsibleID;
            demand.assignToTargetID = command.AssignToTargetID;
            demand.planningStartDate = command.PlanningStartDate;
            demand.planningEndDate = command.PlanningEndDate;
            demand.managementEffort = command.ManagementEffort;
            demand.planningEffort = command.PlanningEffort;
            demand.executionEffort = command.ExecutionEffort;
            demand.description = command.Description;
            demand.customerID = command.CustomerID;
            demand.oportunityID = command.OportunityID;
            demand.isActive = command.IsActive;
            demand.createdByID = command.CreatedByID;
            demand.creationDate = command.CreationDate;
            demand.modifiedByID = command.ModifiedByID;
            demand.lastModifiedDate = command.LastModifiedDate;

            return demand;
        }
    }
}
