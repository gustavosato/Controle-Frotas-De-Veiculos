using ControleVeiculos.Domain.Entities.SystemParameters;
using ControleVeiculos.MVC.Models.SystemParameter;

namespace ControleVeiculos.MVC.Extensions
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