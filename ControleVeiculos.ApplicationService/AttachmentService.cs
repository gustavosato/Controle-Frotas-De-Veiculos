using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Attachments;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Attachments;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class AttachmentService : BaseAppService, IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public void Add(MaintenanceAttachmentCommand command)
        {
            Attachment attachment = new Attachment();

            attachment = attachment.Map(command);

            _attachmentRepository.Add(attachment);
        }

        public void Update(MaintenanceAttachmentCommand command)
        {
            Attachment attachment = new Attachment();

            attachment = attachment.Map(command);

            _attachmentRepository.Update(attachment);
        }

        public Result<Attachment> GetByID(int attachmentID)
        {
            var attachment = _attachmentRepository.GetByID(attachmentID);

            return Result.Ok<Attachment>(0, "", attachment);
        }

        public IPagedList<Attachment> GetAll(FilterAttachmentCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var attachment = _attachmentRepository.GetAll(command);

            return new PagedList<Attachment>(attachment, pageIndex, pageSize);
        }

        public void Delete(int attachmentID)
        {
            _attachmentRepository.Delete(attachmentID);
        }

        public void Delete(string systemFeatureID, int recordID)
        {
            _attachmentRepository.Delete(systemFeatureID, recordID);
        }
    }
}

