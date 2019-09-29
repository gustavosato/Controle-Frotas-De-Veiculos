using ControleVeiculos.Domain.Command.Emprestimos;
using System;

namespace ControleVeiculos.Domain.Entities.Emprestimo
{
    public static class EmprestimoExtensions
    {
        public static Result<Emprestimo> GetEmprestimo(this Emprestimo emprestimo)
        {
            return Result.Ok(0, "", emprestimo);
        }

        public static Emprestimo Map(this Emprestimo emprestimo, MaintenanceEmprestimoCommand command)
        {

            emprestimo.emprestimoID = command.EmprestimoID;
            emprestimo.kmInicial = command.KmInicial;
            emprestimo.kmFinal = command.KmFinal;
            emprestimo.dataSaida = command.DataSaida;
            emprestimo.dataRetorno = command.DataRetorno;
            emprestimo.destino = command.Destino;
            emprestimo.veiculoID = command.VeiculoID;
            emprestimo.funcionarioID = command.FuncionarioID;

            return emprestimo;
        }
    }
}
