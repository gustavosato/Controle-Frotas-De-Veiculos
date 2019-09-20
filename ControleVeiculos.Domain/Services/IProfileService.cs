using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain.Entities.Profiles;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IProfilesService : IDisposable
    {
        void Add(MaintenanceProfileCommand command);
        void Update(MaintenanceProfileCommand command);
        Result<Profile> GetByID(int profileID);
        string GetAllow(FilterProfileCommand command);
        IPagedList<Profile> GetAll(FilterProfileCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int profileID);
    }
}
