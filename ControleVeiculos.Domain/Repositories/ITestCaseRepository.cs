using ControleVeiculos.Domain.Command.TestCases;
using ControleVeiculos.Domain.Entities.TestCases;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ITestCaseRepository
    {
        void Add(TestCase testCase);
        void Update(TestCase testCase);
        TestCase GetByID(int testCaseID);
        List<TestCase> GetAll(FilterTestCaseCommand command);
        void Delete(int testCaseID);
    }
}
