using Lean.Test.Cloud.Domain.Command.ApplicationSystems;
using Lean.Test.Cloud.Domain.Entities.ApplicationSystems;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IApplicationSystemService : IDisposable
    {
        void Add(MaintenanceApplicationSystemCommand command);
        void Update(MaintenanceApplicationSystemCommand command);
        Result<ApplicationSystem> GetByID(int applicationSystemID);
        IPagedList<ApplicationSystem> GetAll(FilterApplicationSystemCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<ApplicationSystem> GetAll(int customerID);
        void Delete(int applicationSystemID);
        string GetApplicationSystemNameByID(int applicationSystemID);
    }
}
