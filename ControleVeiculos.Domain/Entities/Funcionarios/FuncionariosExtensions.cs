using ControleVeiculos.Domain.Command.Funcionarios;
using System;

namespace ControleVeiculos.Domain.Entities.Funcionarios
{
    public static class FuncionarioExtensions
    {
        public static Result<Funcionario> GetFuncionario(this Funcionario funcionario)
        {
            return Result.Ok(0, "", funcionario);
        }

        public static Funcionario Map(this Funcionario funcionario, MaintenanceFuncionarioCommand command)
        {

            funcionario.funcionarioID = command.FuncionarioID;
            funcionario.nomeFuncionario = command.NomeFuncionario;
            funcionario.endereco = command.Endereco;
            funcionario.cpf = command.CPF;
            funcionario.funcao = command.Funcao;
            funcionario.setor= command.Setor;
            funcionario.telefone = command.Telefone;
            funcionario.numeroCnh = command.NumeroCnh;
            
            return funcionario;
        }
    }
}
