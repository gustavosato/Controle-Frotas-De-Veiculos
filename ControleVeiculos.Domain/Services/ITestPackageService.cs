using Lean.Test.Cloud.Domain.Command.TestPackages;
using Lean.Test.Cloud.Domain.Entities.TestPackages;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
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
