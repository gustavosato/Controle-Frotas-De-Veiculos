using Lean.Test.Cloud.Domain.Command.Resumes;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Resumes
{
    public static class ResumeExtensions
    {
        public static Result<Resume> GetMovimentEmployee(this Resume resume)
        {
            return Result.Ok(0, "", resume);
        }

        public static Resume Map(this Resume resume, MaintenanceResumeCommand command)
        {

            resume.resumeID = command.ResumeID;
            resume.summary = command.Summary;
            resume.functionID = command.FunctionID;
            resume.description = command.Description;
            resume.genderID = command.GenderID;
            resume.age = command.Age;
            resume.timeExperience = command.TimeExperience;
            resume.functionLevelID = command.FunctionLevelID;
            resume.statusRhID = command.StatusRhID;
            resume.approvedDateRh = command.ApprovedDateRh;
            resume.statusManagerID = command.StatusManagerID;
            resume.approvedDateManager = command.ApprovedDateManager;
            resume.statusClientID = command.StatusClientID;
            resume.approvedDateClient = command.ApprovedDateClient;
            resume.expectedSalary = command.ExpectedSalary;
            resume.contractTypeID = command.ContractTypeID;
            resume.isEmployee = command.IsEmployee;
            resume.willingToTravel = command.WillingToTravel;
            resume.maritalStatusID = command.MaritalStatusID;
            resume.haveChildren = command.HaveChildren;
            resume.isSmoker = command.IsSmoker;
            resume.availabilityToStart = command.AvailabilityToStart;
            resume.observation = command.Observation;
            resume.createdByID = command.CreatedByID;
            resume.creationDate = command.CreationDate;
            resume.modifiedByID = command.ModifiedByID;
            resume.lastModifiedDate = command.LastModifiedDate;
            resume.resultRh = command.ResultRh;
            resume.resultManager = command.ResultManager;
            resume.resultClient = command.ResultClient;

            return resume;
        }
    }
}
