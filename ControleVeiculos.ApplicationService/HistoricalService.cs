using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Historicals;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Historicals;
using System.Collections.Generic;
using System;

namespace ControleVeiculos.ApplicationService
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

