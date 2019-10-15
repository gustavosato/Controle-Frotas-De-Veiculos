using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Emprestimos;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Emprestimos;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class EmprestimoService : BaseAppService, IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }

        public void Add(MaintenanceEmprestimoCommand command)
        {
            Emprestimo emprestimo = new Emprestimo();

            emprestimo = emprestimo.Map(command);

            _emprestimoRepository.Add(emprestimo);
        }

        public void Update(MaintenanceEmprestimoCommand command)
        {
            Emprestimo emprestimo = new Emprestimo();

            emprestimo = emprestimo.Map(command);

            _emprestimoRepository.Update(emprestimo);
        }

        //public IList<Emprestimo> GetAll(int emprestimoID)
        //{
        //    var emprestimo = _emprestimoRepository.GetAll(emprestimoID);

        //    return new List<Emprestimo>(emprestimo);
        //}

        public Result<Emprestimo> GetByID(int emprestimoID)
        {
            var emprestimo = _emprestimoRepository.GetByID(emprestimoID);

            return Result.Ok<Emprestimo>(0, "", emprestimo);
        }

        public IPagedList<Emprestimo> GetAll(FilterEmprestimoCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var emprestimo = _emprestimoRepository.GetAll(command);

            return new PagedList<Emprestimo>(emprestimo, pageIndex, pageSize);
        }

        public void Delete(int emprestimoID)
        {
            _emprestimoRepository.Delete(emprestimoID);
        }
       
    }
}

