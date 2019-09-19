using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.DemandsUsers;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.DemandsUsers;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
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

