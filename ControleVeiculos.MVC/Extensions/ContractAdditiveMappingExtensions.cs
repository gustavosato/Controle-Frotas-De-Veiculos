using ControleVeiculos.Domain.Entities.ContractAdditives;
using ControleVeiculos.MVC.Models.ContractAdditives;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ContractAdditiveMappingExtensions
    {
        public static ContractAdditiveModel ToModel(this ContractAdditive entity)
        {
            if (entity == null)
                return null;

            var model = new ContractAdditiveModel
            {
                AdditiveID = entity.additiveID,
                ContractID = entity.contractID,
                AdditiveObject = entity.additiveObject,
                StartDate = entity.startDate,
                EndDate = entity.endDate,
                PeriodValidityID = entity.periodValidityID,
                ExtencionID = entity.extencionID,
                ExtencionPeriodID = entity.extencionPeriodID,
                ResetModalityID = entity.resetModalityID,
                BillingCondition = entity.billingCondition,
                OportunityID = entity.oportunityID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}