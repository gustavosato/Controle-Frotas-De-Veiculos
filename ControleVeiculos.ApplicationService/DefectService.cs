using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Defects;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Defects;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;


namespace Lean.Test.Cloud.ApplicationService
{
    public class DefectService : BaseAppService, IDefectService
    {
        private readonly IDefectRepository _defectRepository;

        public DefectService(IDefectRepository defectRepository)
        {
            _defectRepository = defectRepository;
        }

        public string Add(MaintenanceDefectCommand command)
        {
            Defect defect = new Defect();

            defect = defect.Map(command);

           return _defectRepository.Add(defect);
        }

        public void Update(MaintenanceDefectCommand command)
        {
            Defect defect = new Defect();

            defect = defect.Map(command);

            _defectRepository.Update(defect);
        }

        public Result<Defect> GetByID(int defectID)
        {
            var defect = _defectRepository.GetByID(defectID);

            return Result.Ok<Defect>(0, "", defect);
        }

        public IPagedList<Defect> GetAll(FilterDefectCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var defect = _defectRepository.GetAll(command);

            return new PagedList<Defect>(defect, pageIndex, pageSize);
        }

        public void Delete(int defectID)
        {
            _defectRepository.Delete(defectID);
        }

        public List<Defect> ApiGetAll()
        {
            var defect = _defectRepository.ApiGetAll();

            return new List<Defect>(defect);
        }
     }
}

