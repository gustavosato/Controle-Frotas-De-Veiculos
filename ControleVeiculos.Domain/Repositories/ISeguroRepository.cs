using ControleVeiculos.Domain.Command.Seguros;
using ControleVeiculos.Domain.Entities.Seguros;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ISeguroRepository
    {
        void Add(Seguro seguro);
        void Update(Seguro seguro);
        Seguro GetByID(int seguroID);
        List<Seguro> GetAll(FilterSeguroCommand command);
        void Delete(int seguroID);
    }
}
