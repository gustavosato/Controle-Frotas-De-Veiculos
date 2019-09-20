using ControleVeiculos.Domain.Command.ApplicationSystems;
using ControleVeiculos.Domain.Entities.ApplicationSystems;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IApplicationSystemRepository
    {
        void Add(ApplicationSystem applicationSystem);
        void Update(ApplicationSystem applicationSystem);
        ApplicationSystem GetByID(int applicationSystemID);
        List<ApplicationSystem> GetAll(FilterApplicationSystemCommand command);
        List<ApplicationSystem> GetAll(int userID);
        void Delete(int applicationSystemID);
        string GetApplicationSystemNameByID(int applicationSystemID);

    }
}
