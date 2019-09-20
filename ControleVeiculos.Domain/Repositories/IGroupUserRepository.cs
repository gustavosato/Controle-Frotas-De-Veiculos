using ControleVeiculos.Domain.Command.Groups;
using ControleVeiculos.Domain.Command.GroupsUsers;
using ControleVeiculos.Domain.Entities.Groups;
using ControleVeiculos.Domain.Entities.GroupsUsers;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IGroupUserRepository
    {
        void Add(GroupUser GroupUser);
        void Delete(int groupID, int usersID);
        List<Group> GetAllAssociateGroupByUserID(FilterGroupCommand command);
        List<Group> GetAllNoAssociateGroupByUserID(FilterGroupCommand command);
    }
}
