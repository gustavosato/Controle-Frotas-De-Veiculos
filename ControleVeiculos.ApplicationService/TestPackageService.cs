using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.TestPackages;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.TestPackages;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class TestPackageService : BaseAppService, ITestPackageService
    {
        private readonly ITestPackageRepository _testPackageRepository;

        public TestPackageService(ITestPackageRepository testPackageRepository)
        {
            _testPackageRepository = testPackageRepository;
        }

        public void Add(MaintenanceTestPackageCommand command)
        {
            TestPackage testPackage = new TestPackage();

            testPackage = testPackage.Map(command);

            _testPackageRepository.Add(testPackage);
        }

        public void Update(MaintenanceTestPackageCommand command)
        {
            TestPackage testPackage = new TestPackage();

            testPackage = testPackage.Map(command);

            _testPackageRepository.Update(testPackage);
        }

        public Result<TestPackage> GetByID(int testPackageID)
        {
            var testPackage = _testPackageRepository.GetByID(testPackageID);

            return Result.Ok<TestPackage>(0, "", testPackage);
        }

        public IPagedList<TestPackage> GetAll(FilterTestPackageCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var testPackage = _testPackageRepository.GetAll(command);

            return new PagedList<TestPackage>(testPackage, pageIndex, pageSize);
        }

        public void Delete(int testPackageID)
        {
            _testPackageRepository.Delete(testPackageID);
        }
    }
}

