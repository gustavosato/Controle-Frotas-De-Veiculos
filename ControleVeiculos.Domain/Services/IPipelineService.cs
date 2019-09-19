﻿using Lean.Test.Cloud.Domain.Command.Pipelines;
using Lean.Test.Cloud.Domain.Entities.Pipelines;
using System;
using System.Collections.Generic;


namespace Lean.Test.Cloud.Domain.Services
{
    public interface IPipelineService : IDisposable
    {
        string Add(MaintenancePipelineCommand command);
        void Update(MaintenancePipelineCommand command);
        Result<Pipeline> GetByID(int oportunityID);
        IPagedList<Pipeline> GetAll(FilterPipelineCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        List<Pipeline> GetAllCodeByCustomerID(string customerID);
        void Delete(int oportunityID);
        string GetOportunityCodeByID(int oportunityID);
    }
}