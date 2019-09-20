using ControleVeiculos.Domain.Command.Defects;
using ControleVeiculos.Domain.Entities.Defects;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IDefectService : IDisposable
    {
        string Add(MaintenanceDefectCommand command);
        void Update(MaintenanceDefectCommand command);
        Result<Defect> GetByID(int defecID);
        IPagedList<Defect> GetAll(FilterDefectCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        List<Defect> ApiGetAll();
        void Delete(int defecID);
    }
}
