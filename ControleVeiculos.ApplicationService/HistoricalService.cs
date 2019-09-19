using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Historicals;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Historicals;
using System.Collections.Generic;
using System;

namespace Lean.Test.Cloud.ApplicationService
{
    public class HistoricalService : BaseAppService, IHistoricalService
    {
        private readonly IHistoricalRepository _historicalRepository;

        public HistoricalService(IHistoricalRepository historicalRepository)
        {
            _historicalRepository = historicalRepository;
        }

        public void Add(MaintenanceHistoricalCommand command)
        {
            Historical historical = new Historical();

            historical = historical.Map(command);

            _historicalRepository.Add(historical);
        }

        public void Update(MaintenanceHistoricalCommand command)
        {
            Historical historical = new Historical();

            historical = historical.Map(command);

            _historicalRepository.Update(historical);
        }

        public Result<Historical> GetByID(int historicalID)
        {
            var historical = _historicalRepository.GetByID(historicalID);

            return Result.Ok<Historical>(0, "", historical);
        }

        public IPagedList<Historical> GetAll(FilterHistoricalCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var historical = _historicalRepository.GetAll(command);

            return new PagedList<Historical>(historical, pageIndex, pageSize);
        }

        public void Delete(int historicalID)
        {
            _historicalRepository.Delete(historicalID);
        }

        public void Delete(string historicalID, int recordID)
        {
            _historicalRepository.Delete(historicalID, recordID);
        }
    }
}

