using Lean.Test.Cloud.Domain.Command.Demands;
using Lean.Test.Cloud.Domain.Entities.Demands;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IDemandService : IDisposable
    {
        string Add(int userID, MaintenanceDemandCommand command);
        void Update(MaintenanceDemandCommand command);
        Result<Demand> GetByID(int demandID);
        string GetTotalHoursByDemandID(string demandID);
        IPagedList<Demand> GetAll(string customerID, FilterDemandCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Demand> GetAllByTimeReleaseByContact(string customerID, FilterDemandCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Demand> GetAll(string customerID, string demandID, string userID, bool isAssociated = false);
        List<Demand> GetAllGantt(string customerID, FilterDemandCommand command);
        IList<Demand> GetAllByCustomerID(string customerID);
        void Delete(int demandID);
        string GetDemandNameByID(int demandID);
    }

}

