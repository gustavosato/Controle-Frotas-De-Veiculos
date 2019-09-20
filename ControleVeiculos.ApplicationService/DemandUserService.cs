using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.DemandsUsers;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.DemandsUsers;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class DemandUserService : BaseAppService, IDemandUserService
    {
        private readonly IDemandUserRepository _demandUserRepository;

        public DemandUserService(IDemandUserRepository demandUserRepository)
        {
            _demandUserRepository = demandUserRepository;
        }

        public void Add(MaintenanceDemandUserCommand command)
        {
            DemandUser demandUser = new DemandUser();

            demandUser = demandUser.Map(command);

            _demandUserRepository.Add(demandUser);
        }


        public void Delete(int demandID, int userID)
        {
            _demandUserRepository.Delete(demandID, userID);
        }
    }
}

