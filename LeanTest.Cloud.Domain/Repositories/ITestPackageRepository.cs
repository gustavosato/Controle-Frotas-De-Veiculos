using Lean.Test.Cloud.Domain.Command.TestPackages;
using Lean.Test.Cloud.Domain.Entities.TestPackages;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITestPackageRepository
    {
        void Add(TestPackage testPackage);
        void Update(TestPackage testPackage);
        TestPackage GetByID(int testPackageID);
        List<TestPackage> GetAll(FilterTestPackageCommand command);
        void Delete(int testPackageID);
    }
}
