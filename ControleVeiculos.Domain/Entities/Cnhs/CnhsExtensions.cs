using ControleVeiculos.Domain.Command.Cnhs;
using System;

namespace ControleVeiculos.Domain.Entities.Cnhs
{
    public static class CnhExtensions
    {
        public static Result<Cnh> GetCnh(this Cnh cnh)
        {
            return Result.Ok(0, "", cnh);
        }

        public static Cnh Map(this Cnh cnh, MaintenanceCnhCommand command)
        {

            cnh.numeroCnh = command.NumeroCnh;
            cnh.validade = command.Validade;
            cnh.categoria = command.Categoria;
            cnh.funcionarioID = command.FuncionarioID;

            return cnh;
        }
    }
}
