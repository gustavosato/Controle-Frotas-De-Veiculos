using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.MovimentEmployees;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.MovimentEmployees;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class MovimentEmployeeService : BaseAppService, IMovimentEmployeeService
    {
        private readonly IMovimentEmployeeRepository _movimentEmployeeRepository;

        public MovimentEmployeeService(IMovimentEmployeeRepository movimentEmployeeRepository)
        {
            _movimentEmployeeRepository = movimentEmployeeRepository;
        }

        public void Add(MaintenanceMovimentEmployeeCommand command)
        {
            MovimentEmployee movimentEmployee = new MovimentEmployee();

            movimentEmployee = movimentEmployee.Map(command);

            _movimentEmployeeRepository.Add(movimentEmployee);
        }

        public void Update(MaintenanceMovimentEmployeeCommand command)
        {
            MovimentEmployee movimentEmployee = new MovimentEmployee();

            movimentEmployee = movimentEmployee.Map(command);

            _movimentEmployeeRepository.Update(movimentEmployee);
        }

        public Result<MovimentEmployee> GetByID(int movimentEmployeeID)
        {
            var movimentEmployee = _movimentEmployeeRepository.GetByID(movimentEmployeeID);

            return Result.Ok<MovimentEmployee>(0, "", movimentEmployee);
        }

        public IPagedList<MovimentEmployee> GetAll(FilterMovimentEmployeeCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var movimentEmployee = _movimentEmployeeRepository.GetAll(command);

            return new PagedList<MovimentEmployee>(movimentEmployee, pageIndex, pageSize);
        }

        public string GetApropriateByRangeTime(int movimentEmployeeID, int employeeID, string startDate, string endDate)
        {
            return _movimentEmployeeRepository.GetApropriateByRangeTime(movimentEmployeeID, employeeID, startDate, endDate);
        }

        public void Delete(int movimentEmployeeID)
        {
            _movimentEmployeeRepository.Delete(movimentEmployeeID);
        }
    }
}

