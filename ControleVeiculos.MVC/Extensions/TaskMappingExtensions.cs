using ControleVeiculos.Domain.Entities.Tasks;
using ControleVeiculos.MVC.Models.Tasks;

namespace ControleVeiculos.MVC.Extensions
{
    public static class TaskMappingExtensions
    {
        public static TaskModel ToModel(this Task entity)
        {
            if (entity == null)
                return null;

            var model = new TaskModel
            {
                TaskID = entity.taskID,
                Summary = entity.summary,
                Description = entity.description,
                AssignToID = entity.assignToID,
                DemandID = entity.demandID,
                CustomerID = entity.customerID,
                StatusID = entity.statusID,
                TargetDate = entity.targetDate,
                ClosingDate = entity.closingDate,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                CreatedByID = entity.createdByID
            };

            return model;
        }
    }
}
