using Lean.Test.Cloud.Domain.Command.ParameterValues;
using System;

namespace Lean.Test.Cloud.Domain.Entities.ParameterValues
{
    public static class ParameterValueExtensions
    {
        public static Result<ParameterValue> ConsultarGravame(this ParameterValue parameterValue)
        {
            return Result.Ok(0, "", parameterValue);
        }

        public static ParameterValue Map(this ParameterValue parameterValue, MaintenanceParameterValueCommand command)
        {
            parameterValue.parameterValueID = command.ParameterValueID;
            parameterValue.parameterValue = command.ParameterValue;
            parameterValue.parameterID = command.ParameterID;
            parameterValue.parentID = command.ParentID;
            parameterValue.isSystem = command.IsSystem;
            parameterValue.description = command.Description;
            parameterValue.createdByID = command.CreatedByID;
            parameterValue.creationDate = command.CreationDate;
            parameterValue.modifiedByID = command.ModifiedByID;
            parameterValue.lastModifiedDate = command.LastModifiedDate;

            return parameterValue;
        }
    }
}
