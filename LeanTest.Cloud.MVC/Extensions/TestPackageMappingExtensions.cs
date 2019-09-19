using Lean.Test.Cloud.Domain.Entities.TestPackages;
using Lean.Test.Cloud.MVC.Models.TestPackages;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class TestPackageMappingExtensions
    {
        public static TestPackageModel ToModel(this TestPackage entity)
        {
            if (entity == null)
                return null;

            var model = new TestPackageModel
            {
                TestPackageID = entity.testPackageID,
                PackageName = entity.packageName,
                Description = entity.description,
                StatusID = entity.statusID,
                Release = entity.release,
                Cycle = entity.cycle,
                DemandID = entity.demandID,
                EmailsToSendReport = entity.emailsToSendReport,
                TecnologyID = entity.tecnologyID,
                BrowserID = entity.browserID,
                ExecutionSpeedy = entity.executionSpeedy,
                ResetApp = entity.resetApp,
                HighLight = entity.highLight,
                HighLightOut = entity.highLightOut,
                DeviceID = entity.deviceID,
                PlatformNameID = entity.platformNameID,
                SendEmail = entity.sendEmail,
                GenerateLog = entity.generateLog,
                LogHtml = entity.logHtml,
                MethodologyID = entity.methodologyID,
                SolutionPath = entity.solutionPath,
                LeantestVariable = entity.leantestVariable,
                SaveEvidenceToExternalPath = entity.saveEvidenceToExternalPath,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,

            };

            return model;
        }
    }
}