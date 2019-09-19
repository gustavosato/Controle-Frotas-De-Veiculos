using Lean.Test.Cloud.Domain.Command.Attachments;
using Lean.Test.Cloud.Domain.Entities.Attachments;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IAttachmentRepository
    {
        void Add(Attachment attachment);
        void Update(Attachment attachment);
        Attachment GetByID(int attachmentID);
        List<Attachment> GetAll(FilterAttachmentCommand command);
        void Delete(int attachmentID);
        void Delete(string systemFeature, int recordID);
    }
}
