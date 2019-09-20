using ControleVeiculos.Domain.Command.TestPackages;
using ControleVeiculos.Domain.Entities.TestPackages;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface ITestPackageService : IDisposable
    {
        void Add(MaintenanceTestPackageCommand command);
        void Update(MaintenanceTestPackageCommand command);
        Result<TestPackage> GetByID(int testPackageID);
        IPagedList<TestPackage> GetAll(FilterTestPackageCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int testPackageID);
    }
}
