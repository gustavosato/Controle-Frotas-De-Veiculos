using FluentValidation;
using Lean.Test.Cloud.MVC.Models.PipelineEvents;

namespace Lean.Test.Cloud.MVC.Validations.PipelineEvents
{
    public class PipelineEventValidator : AbstractValidator<PipelineEventModel>
    {
        public PipelineEventValidator()
        {
            RuleFor(x => x.TypeID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.NextStepID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.OportunityID).NotEmpty().WithMessage("O campo é obrigatório");           
            RuleFor(x => x.RegisterDate).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.TargetDate).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.Description).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}  