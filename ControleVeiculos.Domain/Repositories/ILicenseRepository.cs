using ControleVeiculos.Domain.Command.Licenses;
using ControleVeiculos.Domain.Entities.Licenses;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ILicenseRepository
    {
        void Add(License license);
        void Update(License license);
        License GetByID(int applicationID);
        List<License> GetAll(FilterLicenseCommand command);
        void Delete(int licenseID);
    }
}
