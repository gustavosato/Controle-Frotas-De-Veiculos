﻿using ControleVeiculos.Domain.Command.Tasks;
using ControleVeiculos.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface ITaskService : IDisposable
    {
        string Add(MaintenanceTaskCommand command);
        void Update(MaintenanceTaskCommand command);
        Result<Task> GetByID(int taskID);
        IPagedList<Task> GetAll(FilterTaskCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        List<Task> GetAllKanban(FilterTaskCommand command);
        void Delete(int taskID);

    }
}
