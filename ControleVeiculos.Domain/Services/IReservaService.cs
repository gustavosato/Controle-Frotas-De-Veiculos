using ControleVeiculos.Domain.Command.Reservas;
using ControleVeiculos.Domain.Entities.Reservas;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IReservaService : IDisposable
    {
        void Add(MaintenanceReservaCommand command);
        void Update(MaintenanceReservaCommand command);
        Result<Reserva> GetByID(int reservaID);
        IPagedList<Reserva> GetAll(FilterReservaCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int reservaID);
    }
}
