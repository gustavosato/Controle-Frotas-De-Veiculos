using ControleVeiculos.Domain.Command.Historicals;
using System;

namespace ControleVeiculos.Domain.Entities.Historicals
{
    public static class HistoricalExtensions
    {
        public static Result<Historical> GetHistorical(this Historical historical)
        {
            return Result.Ok(0, "", historical);
        }

        public static Historical Map(this Historical historical, MaintenanceHistoricalCommand command)
        {

            historical.historicalID = command.HistoricalID;
            historical.systemFeatureID = command.SystemFeatureID;
            historical.recordID = command.RecordID;
            historical.actionID = command.ActionID;
            historical.oldValue = command.OldValue;
            historical.newValue = command.NewValue;
            historical.fieldName = command.FieldName;
            historical.createdByID = command.CreatedByID;
            historical.creationDate = command.CreationDate;
            historical.modifiedByID = command.ModifiedByID;
            historical.lastModifiedDate = command.LastModifiedDate;

            return historical;
        }
    }
}
