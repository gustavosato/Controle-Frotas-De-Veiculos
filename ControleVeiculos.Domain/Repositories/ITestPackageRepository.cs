using ControleVeiculos.Domain.Command.TestPackages;
using ControleVeiculos.Domain.Entities.TestPackages;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
