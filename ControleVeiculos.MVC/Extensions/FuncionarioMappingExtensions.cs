using ControleVeiculos.Domain.Entities.Funcionarios;
using ControleVeiculos.MVC.Models.Funcionarios;

namespace ControleVeiculos.MVC.Extensions
{
    public static class FuncionarioMappingExtensions
    {
        public static FuncionarioModel ToModel(this Funcionario entity)
        {
            if (entity == null)
                return null;

            var model = new FuncionarioModel
            {
                FuncionarioID = entity.funcionarioID,
                NomeFuncionario = entity.nomeFuncionario,
                Endereco = entity.endereco,
                CPF = entity.cpf,
                Funcao = entity.funcao,
                Setor = entity.setor,
                Telefone = entity.telefone,
                NumeroCnh = entity.numeroCnh,
            };

            return model;
        }
    }
}