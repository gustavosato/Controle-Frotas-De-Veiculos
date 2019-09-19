using Lean.Test.Cloud.Domain.Command.Tasks;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Tasks
{
    public static class TaskExtensions
    {
        public static Result<Task> GetTask(this Task task)
        {
            return Result.Ok(0, "", task);
        }

        public static Task Map(this Task task, MaintenanceTaskCommand command)
        {

            task.taskID = command.TaskID;
            task.summary = command.Summary;
            task.description = command.Description;
            task.assignToID = command.AssignToID;
            task.demandID = command.DemandID;
            task.customerID = command.CustomerID;
            task.statusID = command.StatusID;
            task.targetDate = command.TargetDate;
            task.closingDate = command.ClosingDate;
            task.createdByID = command.CreatedByID;
            task.creationDate = command.CreationDate;
            task.modifiedByID = command.ModifiedByID;
            task.lastModifiedDate = command.LastModifiedDate;

            return task;
        }
    }
}
