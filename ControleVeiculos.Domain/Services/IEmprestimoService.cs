using ControleVeiculos.Domain.Command.Emprestimos;
using ControleVeiculos.Domain.Entities.Emprestimos;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IEmprestimoService : IDisposable
    {
        void Add(MaintenanceEmprestimoCommand command);
        void Update(MaintenanceEmprestimoCommand command);
        Result<Emprestimo> GetByID(int emprestimoID);
        IPagedList<Emprestimo> GetAll(FilterEmprestimoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int emprestimoID);
    }
}
