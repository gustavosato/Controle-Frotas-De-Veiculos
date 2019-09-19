using Lean.Test.Cloud.Domain.Entities.Pipelines;
using Lean.Test.Cloud.MVC.Models.Pipelines;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class PipelineMappingExtensions
    {
        public static PipelineModel ToModel(this Pipeline entity)
        {
            if (entity == null)
                return null;

            var model = new PipelineModel
            {
                OportunityID = entity.oportunityID,
                CustomerID = entity.customerID,
                OportunityCode = entity.oportunityCode,
                Description = entity.description,
                PriorityID = entity.priorityID,
                Summary = entity.summary,
                FaseID = entity.faseID,
                OwnerID = entity.ownerID,
                SaleManagerID = entity.saleManagerID,
                PreSalesID = entity.preSalesID,
                OperationManagerID = entity.operationManagerID,
                TypeID = entity.typeID,
                CostCenterID = entity.costCenterID,
                OfferID = entity.offerID,
                Sponsor = entity.sponsor,
                PowerSponsor = entity.powerSponsor,
                ExpectedValue = entity.expectedValue,
                TargetDate = entity.targetDate,
                StatusID = entity.statusID,
                Probability = entity.probability,
                Billed = entity.billed,
                Comments = entity.comments,
                ClosingDate = entity.closingDate,
                FrequencyOfInteractionID = entity.frequencyOfInteractionID,
                ApprovedByID = entity.approvedByID,
                ApprovedDate = entity.approvedDate,
                Quarter1 = entity.quarter1,
                Quarter2 = entity.quarter2,
                Quarter3 = entity.quarter3,
                Quarter4 = entity.quarter4,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate

            };

            return model;
        }
    }
}