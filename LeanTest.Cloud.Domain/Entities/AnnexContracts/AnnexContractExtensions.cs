using Lean.Test.Cloud.Domain.Command.AnnexContracts;
using System;

namespace Lean.Test.Cloud.Domain.Entities.AnnexContracts
{
    public static class AnnexContractExtensions
    {
        public static Result<AnnexContract> GetAnnexContract(this AnnexContract annexContract)
        {
            return Result.Ok(0, "", annexContract);
        }

        public static AnnexContract Map(this AnnexContract annexContract, MaintenanceAnnexContractCommand command)
        {

            annexContract.annexID = command.AnnexID;
            annexContract.contractID = command.ContractID;
            annexContract.oportunityID = command.OportunityID;
            annexContract.summary = command.Summary;
            annexContract.annexObject = command.AnnexObject;
            annexContract.startDate = command.StartDate;
            annexContract.endDate = command.EndDate;
            annexContract.extencionPeriodID = command.ExtencionPeriodID;
            annexContract.createdByID = command.CreatedByID;
            annexContract.creationDate = command.CreationDate;
            annexContract.modifiedByID = command.ModifiedByID;
            annexContract.lastModifiedDate = DateTime.Now.ToString();

            return annexContract;
        }
    }
}
