using ControleVeiculos.Domain.Command.ChangeRequests;
using System;

namespace ControleVeiculos.Domain.Entities.ChangeRequests
{
    public static class ChangeRequestExtensions
    {
        public static Result<ChangeRequest> GetApplicationSystem(this ChangeRequest changeRequest)
        {
            return Result.Ok(0, "", changeRequest);
        }

        public static ChangeRequest Map(this ChangeRequest changeRequest, MaintenanceChangeRequestCommand command)
        {

            changeRequest.changeRequestID = command.ChangeRequestID;
            changeRequest.summary = command.Summary;
            changeRequest.managementEffort = command.ManagementEffort;
            changeRequest.planningEffort = command.PlanningEffort;
            changeRequest.executionEffort = command.ExecutionEffort;
            changeRequest.statusID = command.StatusID;
            changeRequest.targetDate = command.TargetDate;
            changeRequest.approvedDate = command.ApprovedDate;
            changeRequest.approvedByID = command.ApprovedByID;
            changeRequest.description = command.Description;
            changeRequest.demandID = command.DemandID;
            changeRequest.requestByID = command.RequestByID;
            changeRequest.createdByID = command.CreatedByID;
            changeRequest.creationDate = command.CreationDate;
            changeRequest.modifiedByID = command.ModifiedByID;
            changeRequest.lastModifiedDate = DateTime.Now.ToString();

            return changeRequest;
        }
    }
}
