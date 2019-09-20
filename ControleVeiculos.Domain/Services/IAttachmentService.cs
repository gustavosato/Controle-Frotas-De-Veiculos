using ControleVeiculos.Domain.Command.Attachments;
using ControleVeiculos.Domain.Entities.Attachments;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IAttachmentService : IDisposable
    {
        void Add(MaintenanceAttachmentCommand command);
        void Update(MaintenanceAttachmentCommand command);
        Result<Attachment> GetByID(int attachmentID);
        IPagedList<Attachment> GetAll(FilterAttachmentCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int attachmentID);
        void Delete(string attachmentID, int recordID);
    }
}
