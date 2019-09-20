using ControleVeiculos.Domain.Command.PipelineEvents;
using System;

namespace ControleVeiculos.Domain.Entities.PipelineEvents
{
    public static class PipelineEventExtensions
    {
        public static Result<PipelineEvent> GetMovimentEmployee(this PipelineEvent pipelineEvent)
        {
            return Result.Ok(0, "", pipelineEvent);
        }

        public static PipelineEvent Map(this PipelineEvent pipelineEvent, MaintenancePipelineEventCommand command)
        {

            pipelineEvent.saleEventID = command.SaleEventID;
            pipelineEvent.registerDate = command.RegisterDate;
            pipelineEvent.typeID = command.TypeID;
            pipelineEvent.nextStepID = command.NextStepID;
            pipelineEvent.targetDate = command.TargetDate;
            pipelineEvent.description = command.Description;
            pipelineEvent.oportunityID = command.OportunityID;
            pipelineEvent.createdByID = command.CreatedByID;
            pipelineEvent.creationDate = command.CreationDate;
            pipelineEvent.modifiedByID = command.ModifiedByID;
            pipelineEvent.lastModifiedDate = command.LastModifiedDate;
            

            return pipelineEvent;
        }
    }
}
