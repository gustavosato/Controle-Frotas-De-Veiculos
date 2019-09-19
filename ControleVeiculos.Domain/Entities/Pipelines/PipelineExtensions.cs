using Lean.Test.Cloud.Domain.Command.Pipelines;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Pipelines
{
    public static class PipelineExtensions
    {
        public static Result<Pipeline> GetMovimentEmployee(this Pipeline pipeline)
        {
            return Result.Ok(0, "", pipeline);
        }

        public static Pipeline Map(this Pipeline pipeline, MaintenancePipelineCommand command)
        {
            pipeline.oportunityID = command.OportunityID;
            pipeline.customerID = command.CustomerID;
            pipeline.oportunityCode = command.OportunityCode;
            pipeline.description = command.Description;
            pipeline.summary = command.Summary;
            pipeline.priorityID = command.PriorityID;
            pipeline.faseID = command.FaseID;
            pipeline.ownerID = command.OwnerID;
            pipeline.saleManagerID = command.SaleManagerID;
            pipeline.preSalesID = command.PreSalesID;
            pipeline.operationManagerID = command.OperationManagerID;
            pipeline.typeID = command.TypeID;
            pipeline.costCenterID = command.CostCenterID;
            pipeline.offerID = command.OfferID;
            pipeline.sponsor = command.Sponsor;
            pipeline.powerSponsor = command.PowerSponsor;
            pipeline.expectedValue = command.ExpectedValue;
            pipeline.targetDate = command.TargetDate;
            pipeline.statusID = command.StatusID;
            pipeline.probability = command.Probability;
            pipeline.billed = command.Billed;
            pipeline.comments = command.Comments;
            pipeline.closingDate = command.ClosingDate;
            pipeline.frequencyOfInteractionID = command.FrequencyOfInteractionID;
            pipeline.approvedByID = command.ApprovedByID;
            pipeline.approvedDate = command.ApprovedDate;
            pipeline.quarter1 = command.Quarter1;
            pipeline.quarter2 = command.Quarter2;
            pipeline.quarter3 = command.Quarter3;
            pipeline.quarter4 = command.Quarter4;
            pipeline.createdByID = command.CreatedByID;
            pipeline.creationDate = command.CreationDate;
            pipeline.modifiedByID = command.ModifiedByID;
            pipeline.lastModifiedDate = command.LastModifiedDate;

            return pipeline;
        }
    }
}
