using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Funcionarios;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Funcionarios;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class FuncionarioService : BaseAppService, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public void Add(MaintenanceFuncionarioCommand command)
        {
            Funcionario funcionario = new Funcionario();

            funcionario = funcionario.Map(command);

            _funcionarioRepository.Add(funcionario);
        }

        public void Update(MaintenanceFuncionarioCommand command)
        {
            Funcionario funcionario = new Funcionario();

            funcionario = funcionario.Map(command);

            _funcionarioRepository.Update(funcionario);
        }
        
        public Result<Funcionario> GetByID(int funcionarioID)
        {
            var funcionario = _funcionarioRepository.GetByID(funcionarioID);

            return Result.Ok<Funcionario>(0, "", funcionario);
        }

        public IPagedList<Funcionario> GetAll(FilterFuncionarioCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var funcionario = _funcionarioRepository.GetAll(command);

            return new PagedList<Funcionario>(funcionario, pageIndex, pageSize);
        }

        public IList<Funcionario> GetAll(int funcionarioID)
        {
            var funcionario = _funcionarioRepository.GetAll(funcionarioID);

            return new List<Funcionario>(funcionario);
        }

        public void Delete(int funcionarioID)
        {
            _funcionarioRepository.Delete(funcionarioID);
        }
       
    }
}

