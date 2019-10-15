using ControleVeiculos.Domain.Command.Funcionarios;
using ControleVeiculos.Domain.Entities.Funcionarios;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IFuncionarioService : IDisposable
    {
        void Add(MaintenanceFuncionarioCommand command);
        void Update(MaintenanceFuncionarioCommand command);
        Result<Funcionario> GetByID(int funcionarioID);
        IPagedList<Funcionario> GetAll(FilterFuncionarioCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Funcionario> GetAll(int funcionarioID);
        void Delete(int funcionarioID);
    }
}
