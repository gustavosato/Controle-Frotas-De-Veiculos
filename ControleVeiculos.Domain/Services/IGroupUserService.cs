using ControleVeiculos.Domain.Command.Groups;
using ControleVeiculos.Domain.Command.GroupsUsers;
using ControleVeiculos.Domain.Entities.Groups;
using ControleVeiculos.Domain.Entities.GroupsUsers;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IGroupUserService : IDisposable
    {
        void Add(MaintenanceGroupUserCommand command);
        void Delete(int groupID, int usersID);
        IPagedList<Group> GetAllAssociateGroupByUserID(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Group> GetAllNoAssociateGroupByUserID(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
