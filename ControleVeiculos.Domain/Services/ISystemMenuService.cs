using ControleVeiculos.Domain.Command.SystemMenus;
using ControleVeiculos.Domain.Entities.SystemMenus;
using System;
namespace ControleVeiculos.Domain.Services
{
    public interface ISystemMenuService : IDisposable
    {
        void Add(MaintenanceSystemMenuCommand command);
        void Update(MaintenanceSystemMenuCommand command);
        Result<SystemMenu> GetByID(int menuID);
        IPagedList<SystemMenu> GetAll(FilterSystemMenuCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int menuID);
    }
}
