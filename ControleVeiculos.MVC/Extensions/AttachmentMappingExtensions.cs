using Lean.Test.Cloud.Domain.Entities.Attachments;
using Lean.Test.Cloud.MVC.Models.Attachments;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class AttachmentMappingExtensions
    {
        public static AttachmentModel ToModel(this Attachment entity)
        {
            if (entity == null)
                return null;

            var model = new AttachmentModel
            {
                AttachmentID = entity.attachmentID,
                FileName  = entity.fileName,
                SizeFile = entity.sizeFile,
                PathFile = entity.pathFile,
                RecordID = entity.recordID,
                SystemFeatureID = entity.systemFeatureID,
                Description = entity.description,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}