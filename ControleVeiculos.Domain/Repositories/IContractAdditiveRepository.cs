using ControleVeiculos.Domain.Command.ContractAdditives;
using ControleVeiculos.Domain.Entities.ContractAdditives;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IContractAdditiveRepository
    {
        void Add(ContractAdditive contractAdditive);
        void Update(ContractAdditive contractAdditive);
        ContractAdditive GetByID(int additiveID);
        List<ContractAdditive> GetAll(FilterContractAdditiveCommand command);
        void Delete(int additiveID);
    }
}
