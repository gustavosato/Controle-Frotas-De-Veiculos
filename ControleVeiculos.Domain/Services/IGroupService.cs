using ControleVeiculos.Domain.Command.Groups;
using ControleVeiculos.Domain.Entities.Groups;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IGroupService : IDisposable
    {
        void Add(MaintenanceGroupCommand command);
        void Update(MaintenanceGroupCommand command);
        Result<Group> GetByID(int groupID);
        IPagedList<Group> GetAll(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Group> GetAll(int groupID);
        void Delete(int groupID);
        string GetGroupNameByID(int groupID);
    }
}
