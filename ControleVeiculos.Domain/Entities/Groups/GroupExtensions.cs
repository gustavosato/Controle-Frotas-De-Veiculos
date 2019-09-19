using Lean.Test.Cloud.Domain.Command.Groups;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Groups
{
    public static class GroupExtensions
    {
        public static Result<Group> GetMovimentEmployee(this Group group)
        {
            return Result.Ok(0, "", group);
        }

        public static Group Map(this Group group, MaintenanceGroupCommand command)
        {

            group.groupID = command.GroupID;
            group.groupName = command.GroupName;
            group.isSystem = command.IsSystem;
            group.description = command.Description;
            group.createdByID = command.CreatedByID;
            group.creationDate = command.CreationDate;
            group.modifiedByID = command.ModifiedByID;
            group.lastModifiedDate = DateTime.Now.ToString();

            return group;
        }
    }
}
