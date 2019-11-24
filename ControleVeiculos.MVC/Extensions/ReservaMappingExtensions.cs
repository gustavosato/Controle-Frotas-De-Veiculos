using ControleVeiculos.Domain.Entities.Reservas;
using ControleVeiculos.MVC.Models.Reservas;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ReservaMappingExtensions
    {
        public static ReservaModel ToModel(this Reserva entity)
        {
            if (entity == null)
                return null;

            var model = new ReservaModel
            {
                ReservaID = entity.reservaID,
                DataReserva = entity.dataReserva,
                Finalidade = entity.finalidade,
                FuncionarioID = entity.funcionarioID,
                Destino = entity.destino,
                NumeroCnh = entity.numeroCnh,
                VeiculoID = entity.veiculoID,
            };

            return model;
        }
    }
}