﻿using ControleVeiculos.Domain.Command.GroupsUsers;

namespace ControleVeiculos.Domain.Entities.GroupsUsers
{
    public static class GroupUserExtensions
    {
        public static Result<GroupUser> GetGroupUser(this GroupUser groupUser)
        {
            return Result.Ok(0, "", groupUser);
        }

        public static GroupUser Map(this GroupUser groupUser, MaintenanceGroupUserCommand command)
        {
            groupUser.userID = command.UserID;
            groupUser.groupID = command.GroupID;

            return groupUser;
        }
    }
}
