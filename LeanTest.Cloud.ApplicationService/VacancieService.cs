﻿using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class VacancieService : BaseAppService, IVacancieService
    {
        private readonly IVacancieRepository _vacancieRepository;

        public VacancieService(IVacancieRepository vacancieRepository)
        {
            _vacancieRepository = vacancieRepository;
        }

        public void Add(MaintenanceVacancieCommand command)
        {
            Vacancie vacancie = new Vacancie();

            vacancie = vacancie.Map(command);

            _vacancieRepository.Add(vacancie);
        }

        public void Update(MaintenanceVacancieCommand command)
        {
            Vacancie vacancie = new Vacancie();

            vacancie = vacancie.Map(command);

            _vacancieRepository.Update(vacancie);
        }

        public IList<Vacancie> GetAll(int vacancieID)
        {
            var vacancie = _vacancieRepository.GetAll(vacancieID);

            return new List<Vacancie>(vacancie);
        }

        public Result<Vacancie> GetByID(int vacancieID)
        {
            var vacancie = _vacancieRepository.GetByID(vacancieID);

            return Result.Ok<Vacancie>(0, "", vacancie);
        }

        public IPagedList<Vacancie> GetAll(FilterVacancieCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var vacancie = _vacancieRepository.GetAll(command);

            return new PagedList<Vacancie>(vacancie, pageIndex, pageSize);
        }

        public void Delete(int vacancieID)
        {
            _vacancieRepository.Delete(vacancieID);
        }

        public string GetVacancieNameByID(int vacancieID)
        {
            return _vacancieRepository.GetVacancieNameByID(vacancieID);
        }
    }
}

