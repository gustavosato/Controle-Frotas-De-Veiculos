using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Supports;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Supports;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class SupportService : BaseAppService, ISupportService
    {
        private readonly ISupportRepository _supportRepository;

        public SupportService(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        public string Add(MaintenanceSupportCommand command)
        {
            Support support = new Support();

            support = support.Map(command);

            return _supportRepository.Add(support);
        }


        public void Update(MaintenanceSupportCommand command)
        {
            Support support = new Support();

            support = support.Map(command);

            _supportRepository.Update(support);
        }

        public IList<Support> GetAll(int supportID, int customerID)
        {
            var support = _supportRepository.GetAll(supportID, customerID);

            return new List<Support>(support);
        }

        public Result<Support> GetByID(int supportID)
        {
            var support = _supportRepository.GetByID(supportID);

            return Result.Ok<Support>(0, "", support);
        }

        public IPagedList<Support> GetAll(FilterSupportCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var support = _supportRepository.GetAll(command);

            return new PagedList<Support>(support, pageIndex, pageSize);
        }

        public void Delete(int supportID)
        {
            _supportRepository.Delete(supportID);
        }
    }
}

