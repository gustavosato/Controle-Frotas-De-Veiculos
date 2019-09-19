using Lean.Test.Cloud.Domain.Command.SystemParameters;
using System;

namespace Lean.Test.Cloud.Domain.Entities.SystemParameters
{
    public static class SystemParameterExtensions
    {
        public static Result<SystemParameter> GetSystemParameter(this SystemParameter systemParameter)
        {
            return Result.Ok(0, "", systemParameter);
        }

        public static SystemParameter Map(this SystemParameter systemParameter, MaintenanceSystemParameterCommand command)
        {

            systemParameter.parameterID = command.ParameterID;
            systemParameter.paramterName = command.ParamterName;
            systemParameter.paramterValue = command.ParamterValue;
            systemParameter.paramterDefaultValue = command.ParamterDefaultValue;
            systemParameter.createdByID = command.CreatedByID;
            systemParameter.creationDate = command.CreationDate;
            systemParameter.modifiedByID = command.ModifiedByID;
            systemParameter.lastModifiedDate = command.LastModifiedDate;

            return systemParameter;
        }
    }
}
