using ControleVeiculos.Domain.Command.TestPackages;
using System;

namespace ControleVeiculos.Domain.Entities.TestPackages
{
    public static class TestPackageExtensions
    {
        public static Result<TestPackage> GetTestPackage(this TestPackage testPackage)
        {
            return Result.Ok(0, "", testPackage);
        }

        public static TestPackage Map(this TestPackage testPackage, MaintenanceTestPackageCommand command)
        {

            testPackage.testPackageID = command.TestPackageID;
            testPackage.packageName = command.PackageName;
            testPackage.description = command.Description;
            testPackage.demandID = command.DemandID;
            testPackage.statusID = command.StatusID;
            testPackage.release = command.Release;
            testPackage.cycle = command.Cycle;
            testPackage.emailsToSendReport = command.EmailsToSendReport;
            testPackage.tecnologyID = command.TecnologyID;
            testPackage.browserID = command.BrowserID;
            testPackage.executionSpeedy = command.ExecutionSpeedy;
            testPackage.resetApp = command.ResetApp;
            testPackage.highLight = command.HighLight;
            testPackage.highLightOut = command.HighLightOut;
            testPackage.deviceID = command.DeviceID;
            testPackage.platformNameID = command.PlatformNameID;
            testPackage.sendEmail = command.SendEmail;
            testPackage.generateLog = command.GenerateLog;
            testPackage.logHtml = command.LogHtml;
            testPackage.methodologyID = command.MethodologyID;
            testPackage.solutionPath = command.SolutionPath;
            testPackage.leantestVariable = command.LeantestVariable;
            testPackage.saveEvidenceToExternalPath = command.SaveEvidenceToExternalPath;
            testPackage.createdByID = command.CreatedByID;
            testPackage.creationDate = command.CreationDate;
            testPackage.modifiedByID = command.ModifiedByID;
            testPackage.lastModifiedDate = DateTime.Now.ToString();

            return testPackage;
        }
    }
}
