using ControleVeiculos.Domain.Command.Licenses;
using ControleVeiculos.Domain.Entities.Licenses;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ILicenseService : IDisposable
    {
        void Add(MaintenanceLicenseCommand command);
        void Update(MaintenanceLicenseCommand command);
        Result<License> GetByID(int licenseID);
        IPagedList<License> GetAll(FilterLicenseCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int licenseID);
    }
}
