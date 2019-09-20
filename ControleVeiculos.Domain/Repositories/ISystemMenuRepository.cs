using ControleVeiculos.Domain.Command.SystemMenus;
using ControleVeiculos.Domain.Entities.SystemMenus;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ISystemMenuRepository
    {
        void Add(SystemMenu systemMenu);
        void Update(SystemMenu systemMenu);
        SystemMenu GetByID(int menuID);
        List<SystemMenu> GetAll(FilterSystemMenuCommand command);
        List<SystemMenu> GetAll(int menuID);
        void Delete(int menuID);
    }
}
