using ControleVeiculos.Domain.Command.ContractAdditives;
using System;

namespace ControleVeiculos.Domain.Entities.ContractAdditives
{
    public static class ContractAdditiveExtensions
    {
        public static Result<ContractAdditive> GetContractAdditive(this ContractAdditive contractAdditive)
        {
            return Result.Ok(0, "", contractAdditive);
        }

        public static ContractAdditive Map(this ContractAdditive contractAdditive, MaintenanceContractAdditiveCommand command)
        {

            contractAdditive.additiveID = command.AdditiveID;
            contractAdditive.contractID = command.ContractID;
            contractAdditive.additiveObject = command.AdditiveObject;
            contractAdditive.startDate = command.StartDate;
            contractAdditive.endDate = command.EndDate;
            contractAdditive.periodValidityID = command.PeriodValidityID;
            contractAdditive.extencionID = command.ExtencionID;
            contractAdditive.extencionPeriodID = command.ExtencionPeriodID;
            contractAdditive.resetModalityID = command.ResetModalityID;
            contractAdditive.billingCondition = command.BillingCondition;
            contractAdditive.createdByID = command.CreatedByID;
            contractAdditive.creationDate = command.CreationDate;
            contractAdditive.modifiedByID = command.ModifiedByID;
            contractAdditive.lastModifiedDate = DateTime.Now.ToString();

            return contractAdditive;
        }
    }
}
