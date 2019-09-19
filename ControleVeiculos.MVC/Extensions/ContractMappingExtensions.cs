using Lean.Test.Cloud.Domain.Entities.Contracts;
using Lean.Test.Cloud.MVC.Models.Contracts;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class ContractMappingExtensions
    {
        public static ContractModel ToModel(this Contract entity)
        {
            if (entity == null)
                return null;

            var model = new ContractModel
            {
                ContractID = entity.contractID,
                OportunityID = entity.oportunityID,
                ContractTypeID = entity.contractTypeID,
                ContractorCustomerID = entity.contractorCustomerID,
                ContractingCustomerID = entity.contractingCustomerID,
                ObjectContract = entity.objectContract,
                StartDate = entity.startDate,
                EndDate = entity.endDate,
                PeriodValidityID = entity.periodValidityID,
                ExtencionID = entity.extencionID,
                ExtencionPeriodID = entity.extencionPeriodID,
                ResetModalityID = entity.resetModalityID,
                BillingCondition = entity.billingCondition,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}