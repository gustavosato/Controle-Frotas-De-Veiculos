using ControleVeiculos.Domain.Command.MovimentEmployees;
using System;

namespace ControleVeiculos.Domain.Entities.MovimentEmployees
{
    public static class MovimentEmployeeExtensions
    {
        public static Result<MovimentEmployee> GetMovimentEmployee(this MovimentEmployee movimentEmployee)
        {
            return Result.Ok(0, "", movimentEmployee);
        }

        public static MovimentEmployee Map(this MovimentEmployee movimentEmployee, MaintenanceMovimentEmployeeCommand command)
        {

            movimentEmployee.movimentEmployeeID = command.MovimentEmployeeID;
            movimentEmployee.employeeID = command.EmployeeID;
            movimentEmployee.startDate = command.StartDate;
            movimentEmployee.endDate = command.EndDate;
            movimentEmployee.statusID = command.StatusID;
            movimentEmployee.movimentEmployeeTypeID = command.MovimentEmployeeTypeID;
            movimentEmployee.approvedDate = command.ApprovedDate;
            movimentEmployee.approvedByID = command.ApprovedByID;
            movimentEmployee.description = command.Description;
            movimentEmployee.createdByID = command.CreatedByID;
            movimentEmployee.creationDate = command.CreationDate;
            movimentEmployee.modifiedByID = command.ModifiedByID;
            movimentEmployee.lastModifiedDate = DateTime.Now.ToString();

            return movimentEmployee;
        }
    }
}
