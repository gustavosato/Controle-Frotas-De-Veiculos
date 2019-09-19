using Lean.Test.Cloud.Domain.Command.Contracts;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Contracts
{
    public static class ContractExtensions
    {
        public static Result<Contract> GetContract(this Contract contract)
        {
            return Result.Ok(0, "", contract);
        }

        public static Contract Map(this Contract contract, MaintenanceContractCommand command)
        {

            contract.contractID = command.ContractID;
            contract.oportunityID = command.OportunityID;
            contract.contractTypeID = command.ContractTypeID;
            contract.contractorCustomerID = command.ContractorCustomerID;
            contract.contractingCustomerID = command.ContractingCustomerID;
            contract.objectContract = command.ObjectContract;
            contract.startDate = command.StartDate;
            contract.endDate = command.EndDate;
            contract.periodValidityID = command.PeriodValidityID;
            contract.extencionID = command.ExtencionID;
            contract.extencionPeriodID = command.ExtencionPeriodID;
            contract.resetModalityID = command.ResetModalityID;
            contract.billingCondition = command.BillingCondition;
            contract.createdByID = command.CreatedByID;
            contract.creationDate = command.CreationDate;
            contract.modifiedByID = command.ModifiedByID;
            contract.lastModifiedDate = DateTime.Now.ToString();

            return contract;
        }
    }
}
