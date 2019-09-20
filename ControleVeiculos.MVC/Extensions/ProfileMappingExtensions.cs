using ControleVeiculos.Domain.Entities.Profiles;
using ControleVeiculos.MVC.Models.Profiles;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ProfileMappingExtensions
    {
        public static ProfileModel ToModel(this Profile entity)
        {
            if (entity == null)
                return null;

            var model = new ProfileModel
            {
                ProfileID = entity.ProfileID,
                GroupID = entity.GroupID,
                SystemFeatureID = entity.SystemFeatureID,
                AllowAdd = entity.AllowAdd,
                AllowView = entity.AllowView,
                AllowUpdate = entity.AllowUpdate,
                AllowDelete = entity.AllowDelete,
                AllowChangeStatus = entity.AllowChangeStatus,
                AllowAddRemove = entity.AllowAddRemove,
                AllowExportExcel = entity.AllowExportExcel,
                AllowReportView = entity.AllowReportView,
                CreatedByID = entity.CreatedByID,
                CreationDate = entity.CreationDate,
                ModifiedByID = entity.ModifiedByID,
                LastModifiedDate = entity.LastModifiedDate
            };

            return model;
        }
    }
}