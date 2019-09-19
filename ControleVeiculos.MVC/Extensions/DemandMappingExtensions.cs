using Lean.Test.Cloud.Domain.Entities.Demands;
using Lean.Test.Cloud.MVC.Models.Demands;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class DemandMappingExtensions
    {
        public static DemandModel ToModel(this Demand entity)
        {
            if (entity == null)
                return null;

            var model = new DemandModel
            {
                DemandID = entity.demandID,
                DemandName = entity.demandName,
                TypeID = entity.typeID,
                StatusID = entity.statusID,
                Scope = entity.scope,
                ServiceID = entity.serviceID,
                ExternalCode = entity.externalCode,
                DemandCode = entity.demandCode,
                ResponsibleID = entity.responsibleID,
                AssignToTargetID = entity.assignToTargetID,
                PlanningStartDate = entity.planningStartDate,
                ManagementEffort = entity.managementEffort,
                PlanningEndDate = entity.planningEndDate,
                PlanningEffort = entity.planningEffort,
                ExecutionEffort = entity.executionEffort,
                Descriptions = entity.description,
                CustomerID = entity.customerID,
                OportunityID = entity.oportunityID,
                IsActive = entity.isActive,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                TotalTime = entity.totalTime

            };

            return model;
        }
    }
}