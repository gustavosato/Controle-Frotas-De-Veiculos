using ControleVeiculos.Domain.Command.Defects;
using System;

namespace ControleVeiculos.Domain.Entities.Defects
{
    public static class DefectExtensions
    {
        public static Result<Defect> GetMovimentEmployee(this Defect defect)
        {
            return Result.Ok(0, "", defect);
        }

        public static Defect Map(this Defect defect, MaintenanceDefectCommand command)
        {

            defect.defectID = command.DefectID;
            defect.summary = command.Summary;
            defect.description = command.Description;
            defect.statusID = command.StatusID;
            defect.severityID = command.SeverityID;
            defect.priorityID = command.PriorityID;
            defect.assingToID = command.AssingToID;
            defect.typeID = command.TypeID;
            defect.applicationSystemID = command.ApplicationSystemID;
            defect.featureID = command.FeatureID;
            defect.resolutionID = command.ResolutionID;
            defect.resolution = command.Resolution;
            defect.resolutionDate = command.ResolutionDate;
            defect.createdByID = command.CreatedByID;
            defect.creationDate = command.CreationDate;
            defect.modifiedByID = command.ModifiedByID;
            defect.lastModifiedDate = command.LastModifiedDate;

            return defect;
        }
    }
}
