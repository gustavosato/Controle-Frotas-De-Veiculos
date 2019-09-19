using Lean.Test.Cloud.Domain.Entities.MovimentEmployees;
using Lean.Test.Cloud.MVC.Models.MovimentEmployees;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class MovimentEmployeeMappingExtensions
    {
        public static MovimentEmployeeModel ToModel(this MovimentEmployee entity)
        {
            if (entity == null)
                return null;

            var model = new MovimentEmployeeModel
            {
                MovimentEmployeeID = entity.movimentEmployeeID,
                EmployeeID = entity.employeeID,
                StartDate = entity.startDate,
                EndDate = entity.endDate,
                StatusID = entity.statusID,
                MovimentEmployeeTypeID = entity.movimentEmployeeTypeID,
                ApprovedDate = entity.approvedDate,
                ApprovedByID = entity.approvedByID,
                Description = entity.description,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}