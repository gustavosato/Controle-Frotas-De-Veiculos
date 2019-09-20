using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Demands;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Demands;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class DemandService : BaseAppService, IDemandService
    {
        private readonly IDemandRepository _demandRepository;

        public DemandService(IDemandRepository demandRepository)
        {
            _demandRepository = demandRepository;
        }

        public string Add(int userID, MaintenanceDemandCommand command)
        {
            Demand demand = new Demand();

            demand = demand.Map(command);

           return _demandRepository.Add(userID, demand);
        }

        public void Update(MaintenanceDemandCommand command)
        {
            Demand demand = new Demand();

            demand = demand.Map(command);

            _demandRepository.Update(demand);
        }

        public Result<Demand> GetByID(int demandID)
        {
            var demand = _demandRepository.GetByID(demandID);

            return Result.Ok<Demand>(0, "", demand);
        }

        public string GetTotalHoursByDemandID(string demandID)
        {
            return _demandRepository.GetTotalHoursByDemandID(demandID);
        }

       

        public IPagedList<Demand> GetAll(string customerID, FilterDemandCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var demands = _demandRepository.GetAll(customerID, command);
      
            return new PagedList<Demand>(demands, pageIndex, pageSize);
        }

        public IPagedList<Demand> GetAllByTimeReleaseByContact(string customerID, FilterDemandCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var demands = _demandRepository.GetAllByTimeReleaseByContact(customerID, command);

            return new PagedList<Demand>(demands, pageIndex, pageSize);
        }

        public IList<Demand> GetAll(string cusmtomerID, string demandID, string userID, bool isAssociated)
        {
            var demand = _demandRepository.GetAll(cusmtomerID, demandID, userID, isAssociated);

            return new List<Demand>(demand);
        }

        public List<Demand> GetAllGantt(string customerID, FilterDemandCommand command)
        {
            var demand = _demandRepository.GetAllGantt(customerID,command);

            return new List<Demand>(demand);
        }

        public IList<Demand> GetAllByCustomerID(string customerID)
        {
            var demand = _demandRepository.GetAllByCustomerID(customerID);

            return new List<Demand>(demand);
        }

        public void Delete(int demandID)
        {
            _demandRepository.Delete(demandID);
        }

        public string GetDemandNameByID(int demandID)
        {
            return _demandRepository.GetDemandNameByID(demandID);
        }
    }
}
