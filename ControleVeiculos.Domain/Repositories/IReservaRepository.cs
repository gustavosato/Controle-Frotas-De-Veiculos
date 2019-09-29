using ControleVeiculos.Domain.Command.Reservas;
using ControleVeiculos.Domain.Entities.Reservas;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IReservaRepository
    {
        void Add(Reserva reserva);
        void Update(Reserva reserva);
        Reserva GetByID(int reservaID);
        List<Reserva> GetAll(FilterReservaCommand command);
        void Delete(int reservaID);
    }
}
