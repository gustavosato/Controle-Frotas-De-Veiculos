using ControleVeiculos.Domain.Entities.PositionsSalaries;
using ControleVeiculos.MVC.Models.PositionsSalaries;

namespace ControleVeiculos.MVC.Extensions
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