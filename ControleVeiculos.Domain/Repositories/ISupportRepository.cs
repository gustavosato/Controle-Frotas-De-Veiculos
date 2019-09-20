using ControleVeiculos.Domain.Command.Supports;
using ControleVeiculos.Domain.Entities.Supports;
using System.Collections.Generic;


namespace ControleVeiculos.Domain.Repositories
{
    public interface ISupportRepository
    {
        string Add(Support support);
        void Update(Support support);
        Support GetByID(int supportID);
        List<Support> GetAll(FilterSupportCommand command);
        List<Support> GetAll(int supportID, int customerID);
        void Delete(int supportID);
    }
}
