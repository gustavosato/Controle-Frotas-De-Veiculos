using Lean.Test.Cloud.Domain.Command.Workflows;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Workflows
{
    public static class WorkflowExtensions
    {
        public static Result<Workflow> GetWorkflow(this Workflow workflow)
        {
            return Result.Ok(0, "", workflow);
        }

        public static Workflow Map(this Workflow workflow, MaintenanceWorkflowCommand command)
        {

            workflow.workflowID = command.WorkflowID;
            workflow.systemFeatureID = command.SystemFeatureID;
            workflow.groupID = command.GroupID;
            workflow.statusID = command.StatusID;
            workflow.statusToID = command.StatusToID;
            workflow.createdByID = command.CreatedByID;
            workflow.creationDate = command.CreationDate;
            workflow.modifiedByID = command.ModifiedByID;
            workflow.lastModifiedDate = DateTime.Now.ToString();

            return workflow;
        }
    }
}
