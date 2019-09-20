using ControleVeiculos.Domain.Command.Historicals;
using ControleVeiculos.Domain.Entities.Historicals;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IHistoricalRepository
    {
        void Add(Historical historical);
        void Update(Historical historical);
        Historical GetByID(int systemFeatureID);
        List<Historical> GetAll(FilterHistoricalCommand command);
        void Delete(int systemFeatureID);
        void Delete(string systemFeatureID, int recordID);
    }
}
