using FluentValidation;
using ControleVeiculos.MVC.Models.Attachments;

namespace ControleVeiculos.MVC.Validations.Attachments
{
    public class AttachmentValidator : AbstractValidator<AttachmentModel>
    {
        public AttachmentValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}
