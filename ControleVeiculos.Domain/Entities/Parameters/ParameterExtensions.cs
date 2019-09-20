using ControleVeiculos.Domain.Command.Parameters;
using System;

namespace ControleVeiculos.Domain.Entities.Parameters
{
    public static class ParameterExtensions
    {
        public static Result<Parameter> GetParameter(this Parameter parameter)
        {
            return Result.Ok(0, "", parameter);
        }

        public static Parameter Map(this Parameter parameter, MaintenanceParameterCommand command)
        {

            parameter.parameterID = command.ParameterID;
            parameter.parameterName = command.ParameterName;
            parameter.systemFeatureID = command.SystemFeatureID;
            parameter.createdByID = command.CreatedByID;
            parameter.creationDate = command.CreationDate;
            parameter.modifiedByID = command.ModifiedByID;
            parameter.lastModifiedDate = command.LastModifiedDate;

            return parameter;
        }
    }
}
