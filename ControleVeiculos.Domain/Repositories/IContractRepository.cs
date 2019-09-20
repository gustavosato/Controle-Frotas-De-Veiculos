using ControleVeiculos.Domain.Command.Contracts;
using ControleVeiculos.Domain.Entities.Contracts;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IContractRepository
    {
        void Add(Contract contract);
        void Update(Contract contract);
        Contract GetByID(int contractID);
        List<Contract> GetAll(FilterContractCommand command);
        List<Contract> GetAll(int contractID);
        void Delete(int contractID);
    }
}
