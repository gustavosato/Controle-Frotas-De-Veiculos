using ControleVeiculos.Domain.Command.Emprestimos;
using ControleVeiculos.Domain.Entities.Emprestimos;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IEmprestimoRepository
    {
        void Add(Emprestimo emprestimo);
        void Update(Emprestimo emprestimo);
        Emprestimo GetByID(int emprestimoID);
        List<Emprestimo> GetAll(FilterEmprestimoCommand command);
        void Delete(int emprestimoID);
    }
}
