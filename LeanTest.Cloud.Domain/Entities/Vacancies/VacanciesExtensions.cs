using Lean.Test.Cloud.Domain.Command.Vacancies;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Vacancies
{
    public static class VacancieExtensions
    {
        public static Result<Vacancie> GetVacancie(this Vacancie vacancie)
        {
            return Result.Ok(0, "", vacancie);
        }

        public static Vacancie Map(this Vacancie vacancie, MaintenanceVacancieCommand command)
        {

            vacancie.vacancieID = command.VacancieID;
            vacancie.summary = command.Summary;
            vacancie.vacanciesTypeID = command.VacanciesTypeID;
            vacancie.description = command.Description;
            vacancie.customerID = command.CustomerID;
            vacancie.internalApplicantID = command.InternalApplicantID;
            vacancie.externalApplicantID = command.ExternalApplicantID;
            vacancie.assignToID = command.AssignToID;
            vacancie.contractTypeID = command.ContractTypeID;
            vacancie.validityID = command.ValidityID;
            vacancie.statusID = command.StatusID;
            vacancie.openingDate = command.OpeningDate;
            vacancie.closingDate = command.ClosingDate;
            vacancie.expectedStartDate = command.ExpectedStartDate;
            vacancie.maximumValue = command.MaximumValue;
            vacancie.closedValue = command.ClosedValue;
            vacancie.workPlace = command.WorkPlace;
            vacancie.resumeSelectedID = command.ResumeSelectedID;
            vacancie.createdByID = command.CreatedByID;
            vacancie.creationDate = command.CreationDate;
            vacancie.modifiedByID = command.ModifiedByID;
            vacancie.lastModifiedDate = command.LastModifiedDate;

            return vacancie;
        }
    }
}
