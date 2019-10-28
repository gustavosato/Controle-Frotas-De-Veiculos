using ControleVeiculos.Domain.Command.Funcionarios;
using ControleVeiculos.Domain.Entities.Funcionarios;
using System.Collections.Generic;


namespace ControleVeiculos.Domain.Repositories
{
    public interface IFuncionarioRepository
    {
        void Add(Funcionario funcionario);
        void Update(Funcionario funcionario);
        Funcionario GetByID(int funcionarioID);
        List<Funcionario> GetAll(FilterFuncionarioCommand command);
        void Delete(int funcionarioID);
       
    }
}
