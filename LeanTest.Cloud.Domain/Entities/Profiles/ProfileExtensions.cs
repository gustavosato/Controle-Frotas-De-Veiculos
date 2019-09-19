using Lean.Test.Cloud.Domain.Command.Profiles;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Profiles
{
    public static class ProfileExtensions
    {
        public static Result<Profile> GetProfile(this Profile profile)
        {
            return Result.Ok(0, "", profile);
        }

        public static Profile Map(this Profile profile, MaintenanceProfileCommand command)
        {

            profile.ProfileID = command.ProfileID;
            profile.GroupID = command.GroupID;
            profile.SystemFeatureID = command.SystemFeatureID;
            profile.AllowView = command.AllowView;
            profile.AllowAdd = command.AllowAdd;
            profile.AllowUpdate = command.AllowUpdate;
            profile.AllowDelete = command.AllowDelete;
            profile.AllowChangeStatus = command.AllowChangeStatus;
            profile.AllowAddRemove = command.AllowAddRemove;
            profile.AllowExportExcel = command.AllowExportExcel;
            profile.AllowReportView = command.AllowReportView;
            profile.CreatedByID = command.CreatedByID;
            profile.CreationDate = command.CreationDate;
            profile.ModifiedByID = command.ModifiedByID;
            profile.LastModifiedDate = command.LastModifiedDate;

            return profile;
        }
    }
}
