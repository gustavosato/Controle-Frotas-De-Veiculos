using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.MVC.Models.Resumes;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ResumeMappingExtensions
    {
        public static ResumeModel ToModel(this Resume entity)
        {
            if (entity == null)
                return null;

            var model = new ResumeModel
            {
                ResumeID = entity.resumeID,
                Summary = entity.summary,
                FunctionID = entity.functionID,
                Description = entity.description,
                GenderID = entity.genderID,
                Age = entity.age,
                TimeExperience = entity.timeExperience,
                FunctionLevelID = entity.functionLevelID,
                StatusRhID = entity.statusRhID,
                ApprovedDateRh = entity.approvedDateRh,
                StatusManagerID = entity.statusManagerID,
                ApprovedDateManager = entity.approvedDateManager,
                StatusClientID = entity.statusClientID,
                ApprovedDateClient = entity.approvedDateClient,
                ExpectedSalary = entity.expectedSalary,
                ContractTypeID = entity.contractTypeID,
                IsEmployee = entity.isEmployee,
                WillingToTravel = entity.willingToTravel,
                MaritalStatusID = entity.maritalStatusID,
                HaveChildren = entity.haveChildren,
                IsSmoker = entity.isSmoker,
                AvailabilityToStart = entity.availabilityToStart,
                Observation = entity.observation,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,                
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                ResultRh = entity.resultRh,
                ResultManager = entity.resultManager,
                ResultClient = entity.resultClient

            };

            return model;
        }
    }
}