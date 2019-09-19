using Lean.Test.Cloud.Domain.Entities.Profiles;
using Lean.Test.Cloud.MVC.Models.Profiles;

namespace Lean.Test.Cloud.MVC.Extensions
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