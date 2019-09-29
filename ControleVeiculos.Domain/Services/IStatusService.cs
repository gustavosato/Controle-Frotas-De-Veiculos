﻿using ControleVeiculos.Domain.Command.Statuss;
using ControleVeiculos.Domain.Entities.Statuss;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IStatusService : IDisposable
    {
        void Add(MaintenanceStatusCommand command);
        void Update(MaintenanceStatusCommand command);
        Result<Status> GetByID(int statusID);
        IPagedList<Status> GetAll(FilterStatusCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int statusID);
    }
}
