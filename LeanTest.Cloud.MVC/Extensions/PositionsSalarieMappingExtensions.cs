using Lean.Test.Cloud.Domain.Entities.PositionsSalaries;
using Lean.Test.Cloud.MVC.Models.PositionsSalaries;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class PositionsSalarieMappingExtensions
    {
        public static PositionsSalarieModel ToModel(this PositionsSalarie entity)
        {
            if (entity == null)
                return null;

            var model = new PositionsSalarieModel
            {
                PositionsSalarieID = entity.positionsSalarieID,
                FunctionID = entity.functionID,
                LevelID = entity.levelID,
                ClassificationID = entity.classificationID,
                AmountPJ = entity.amountPJ,
                AmountCLT = entity.amountCLT,
                AmountCLTFLEX = entity.amountCLTFLEX,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                StartingDate = entity.startingDate,
                ClosingDate = entity.closingDate

            };

            return model;
        }
    }
}