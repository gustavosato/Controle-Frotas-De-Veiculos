using Lean.Test.Cloud.Domain.Command.Attachments;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Attachments
{
    public static class AttachmentExtensions
    {
        public static Result<Attachment> GetAttachment(this Attachment attachment)
        {
            return Result.Ok(0, "", attachment);
        }

        public static Attachment Map(this Attachment attachment, MaintenanceAttachmentCommand command)
        {

            attachment.attachmentID = command.AttachmentID;
            attachment.fileName = command.FileName;
            attachment.description = command.Description;
            attachment.binaryFile = command.BinaryFile;
            attachment.pathFile = command.PathFile;
            attachment.sizeFile = command.SizeFile;
            attachment.recordID = command.RecordID;
            attachment.systemFeatureID = command.SystemFeatureID;
            attachment.createdByID = command.CreatedByID;
            attachment.creationDate = command.CreationDate;
            attachment.modifiedByID = command.ModifiedByID;
            attachment.lastModifiedDate = command.LastModifiedDate;

            return attachment;
        }
    }
}
