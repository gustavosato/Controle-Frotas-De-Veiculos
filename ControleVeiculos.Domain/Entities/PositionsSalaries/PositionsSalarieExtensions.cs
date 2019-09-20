using ControleVeiculos.Domain.Command.PositionsSalaries;
using System;

namespace ControleVeiculos.Domain.Entities.PositionsSalaries
{
    public static class PositionsSalarieExtensions
    {
        public static Result<PositionsSalarie> GetPositionsSalarie(this PositionsSalarie positionsSalarie)
        {
            return Result.Ok(0, "", positionsSalarie);
        }

        public static PositionsSalarie Map(this PositionsSalarie positionsSalarie, MaintenancePositionsSalarieCommand command)
         
        {

            positionsSalarie.positionsSalarieID = command.positionsSalarieID;
            positionsSalarie.functionID = command.functionID;
            positionsSalarie.levelID = command.levelID;
            positionsSalarie.classificationID = command.classificationID;
            positionsSalarie.amountPJ = command.amountPJ;
            positionsSalarie.amountCLT = command.amountCLT;
            positionsSalarie.amountCLTFLEX = command.amountCLTFLEX;
            positionsSalarie.createdByID = command.createdByID;
            positionsSalarie.creationDate = command.creationDate;
            positionsSalarie.modifiedByID = command.modifiedByID;
            positionsSalarie.lastModifiedDate = command.lastModifiedDate;
            positionsSalarie.startingDate = command.startingDate;
            positionsSalarie.closingDate = command.closingDate;


            return positionsSalarie;
        }
    }
}
