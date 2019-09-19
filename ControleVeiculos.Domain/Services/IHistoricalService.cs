﻿using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.Domain.Entities.Historicals;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IHistoricalService : IDisposable
    {
        void Add(MaintenanceHistoricalCommand command);
        void Update(MaintenanceHistoricalCommand command);
        Result<Historical> GetByID(int systemFeatureID);
        IPagedList<Historical> GetAll(FilterHistoricalCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int systemFeatureID);
        void Delete(string systemFeatureID, int recordID);

    }
}