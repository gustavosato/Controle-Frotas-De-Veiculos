using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Licenses;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Licenses;
using System.Collections.Generic;
using System;

namespace ControleVeiculos.ApplicationService
{
    public class LicenseService : BaseAppService, ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;

        public LicenseService(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public void Add(MaintenanceLicenseCommand command)
        {
            License license = new License();

            license = license.Map(command);
            _licenseRepository.Add(license);
        }

        public void Update(MaintenanceLicenseCommand command)
        {
            License license = new License();

            license = license.Map(command);

            _licenseRepository.Update(license);
        }

        public Result<License> GetByID(int licenseID)
        {
            var license = _licenseRepository.GetByID(licenseID);

            return Result.Ok<License>(0, "", license);
        }

        public IPagedList<License> GetAll(FilterLicenseCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var license = _licenseRepository.GetAll(command);

            return new PagedList<License>(license, pageIndex, pageSize);
        }

        public void Delete(int licenseID)
        {
            _licenseRepository.Delete(licenseID);
        }
    }
}

