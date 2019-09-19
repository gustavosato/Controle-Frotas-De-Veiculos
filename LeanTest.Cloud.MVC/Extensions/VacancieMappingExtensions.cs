using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.MVC.Models.Vacancies;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class VacancieMappingExtensions
    {
        public static VacancieModel ToModel(this Vacancie entity)
        {
            if (entity == null)
                return null;

            var model = new VacancieModel
            {
                VacancieID = entity.vacancieID,
                Summary = entity.summary,
                VacanciesTypeID = entity.vacanciesTypeID,
                Description = entity.description,
                CustomerID = entity.customerID,
                InternalApplicantID = entity.internalApplicantID,
                ExternalApplicantID = entity.externalApplicantID,
                AssignToID = entity.assignToID,
                ContractTypeID = entity.contractTypeID,
                StatusID = entity.statusID,
                ValidityID = entity.validityID,
                OpeningDate = entity.openingDate,
                ClosingDate = entity.closingDate,
                ExpectedStartDate = entity.expectedStartDate,
                MaximumValue = entity.maximumValue,
                ClosedValue = entity.closedValue,
                WorkPlace = entity.workPlace,
                ResumeSelectedID = entity.resumeSelectedID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}