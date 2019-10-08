using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Reservas;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Reservas;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class ReservaService : BaseAppService, IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public void Add(MaintenanceReservaCommand command)
        {
            Reserva reserva = new Reserva();

            reserva = reserva.Map(command);

            _reservaRepository.Add(reserva);
        }

        public void Update(MaintenanceReservaCommand command)
        {
            Reserva reserva = new Reserva();

            reserva = reserva.Map(command);

            _reservaRepository.Update(reserva);
        }

        public IList<Reserva> GetAll(int reservaID)
        {
            var reserva = _reservaRepository.GetAll(reservaID);

            return new List<Reserva>(reserva);
        }

        public Result<Reserva> GetByID(int reservaID)
        {
            var reserva = _reservaRepository.GetByID(reservaID);

            return Result.Ok<Reserva>(0, "", reserva);
        }

        public IPagedList<Reserva> GetAll(FilterReservaCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var reserva = _reservaRepository.GetAll(command);

            return new PagedList<Reserva>(reserva, pageIndex, pageSize);
        }

        public void Delete(int reservaID)
        {
            _reservaRepository.Delete(reservaID);
        }
       
    }
}

