﻿using Lean.Test.Cloud.Domain.Command.Demands;
using Lean.Test.Cloud.Domain.Entities.Demands;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IDemandRepository
    {
        string Add(int userID, Demand demand);
        void Update(Demand demand);
        Demand GetByID(int demandID);
        string GetTotalHoursByDemandID(string demandID);
        List<Demand> GetAll(string customerID, FilterDemandCommand command);
        List<Demand> GetAllGantt(string customerID, FilterDemandCommand command);
        List<Demand> GetAll(string customerID, string demandID, string userID, bool isAssociated);
        List<Demand> GetAllByTimeReleaseByContact(string customerID, FilterDemandCommand command);
        List<Demand> GetAllByCustomerID(string customerID);
        void Delete(int demandID);
        string GetDemandNameByID(int demandID);
    }
}