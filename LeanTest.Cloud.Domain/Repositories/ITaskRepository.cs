using Lean.Test.Cloud.Domain.Command.Tasks;
using Lean.Test.Cloud.Domain.Entities.Tasks;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITaskRepository
    {
        string Add(Task task);
        void Update(Task task);
        Task GetByID(int taskID);
        List<Task> GetAll(FilterTaskCommand command);
        List<Task> GetAllKanban(FilterTaskCommand command);
        void Delete(int taskID);
    }
}
