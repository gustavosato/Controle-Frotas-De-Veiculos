using Lean.Test.Cloud.Domain.Entities.SystemParameters;
using Lean.Test.Cloud.MVC.Models.SystemParameter;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class SystemParameterMappingExtensions
    {
        public static SystemParameterModel ToModel(this SystemParameter entity)
        {
            if (entity == null)
                return null;

            var model = new SystemParameterModel
            {
                ParameterID = entity.parameterID,
                ParamterName = entity.paramterName,
                ParamterValue = entity.paramterValue,
                ParamterDefaultValue = entity.paramterDefaultValue,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}