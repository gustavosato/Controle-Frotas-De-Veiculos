﻿using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Tasks;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Tasks;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class TaskService : BaseAppService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public string Add(MaintenanceTaskCommand command)
        {
            Task task = new Task();

            task = task.Map(command);

            return _taskRepository.Add(task);
        }

        public void Update(MaintenanceTaskCommand command)
        {
            Task task = new Task();

            task = task.Map(command);

            _taskRepository.Update(task);
        }

        public Result<Task> GetByID(int taskID)
        {
            var task = _taskRepository.GetByID(taskID);

            return Result.Ok<Task>(0, "", task);
        }

        public IPagedList<Task> GetAll(FilterTaskCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var task = _taskRepository.GetAll(command);

            return new PagedList<Task>(task, pageIndex, pageSize);
        }

        public void Delete(int taskID)
        {
            _taskRepository.Delete(taskID);
        }

        public List<Task> GetAllKanban(FilterTaskCommand command)
        {
            var task = _taskRepository.GetAllKanban(command);

            return new List<Task>(task);
        }
    }
}
