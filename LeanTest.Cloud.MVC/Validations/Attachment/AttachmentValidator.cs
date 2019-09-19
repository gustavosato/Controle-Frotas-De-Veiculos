using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Attachments;

namespace Lean.Test.Cloud.MVC.Validations.Attachments
{
    public class AttachmentValidator : AbstractValidator<AttachmentModel>
    {
        public AttachmentValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}
