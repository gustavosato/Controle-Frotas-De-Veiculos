using Lean.Test.Cloud.Domain.Command.Issues;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Issues
{
    public static class IssueExtensions
    {
        public static Result<Issue> GetIssue(this Issue issue)
        {
            return Result.Ok(0, "", issue);
        }

        public static Issue Map(this Issue issue, MaintenanceIssueCommand command)
        {

            issue.issueID = command.IssueID;
            issue.summary = command.Summary;
            issue.description = command.Description;
            issue.statusID = command.StatusID;
            issue.severityID = command.SeverityID;
            issue.priorityID = command.PriorityID;
            issue.assingToID = command.AssingToID;
            issue.typeID = command.TypeID;
            issue.resolutionID = command.ResolutionID;
            issue.resolution = command.Resolution; 
            issue.resolutionDate = command.ResolutionDate;
            issue.createdByID = command.CreatedByID;
            issue.creationDate = command.CreationDate;
            issue.modifiedByID = command.ModifiedByID;
            issue.lastModifiedDate = DateTime.Now.ToString();

            return issue;
        }
    }
}
