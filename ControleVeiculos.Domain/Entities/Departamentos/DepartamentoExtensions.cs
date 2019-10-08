using ControleVeiculos.Domain.Command.Departamentos;
using System;

namespace ControleVeiculos.Domain.Entities.Departamentos
{
    public static class DepartamentoExtensions
    {
        public static Result<Departamento> GetDepartamento(this Departamento departamento)
        {
            return Result.Ok(0, "", departamento);
        }

        public static Departamento Map(this Departamento departamento, MaintenanceDepartamentoCommand command)
        {

            departamento.departamentoID = command.DepartamentoID;
            departamento.nomeDepartamento = command.NomeDepartamento;
            departamento.descricao = command.Descricao;
            departamento.funcionarioID = command.FuncionarioID;
            
            return departamento;
        }
    }
}
