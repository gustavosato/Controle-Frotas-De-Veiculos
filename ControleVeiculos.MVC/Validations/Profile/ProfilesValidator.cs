using FluentValidation;
using Lean.Test.Cloud.MVC.Models.Profiles;

namespace Lean.Test.Cloud.MVC.Validations.Profiles
{
    public class ProfileValidator : AbstractValidator<ProfileModel>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.GroupID).NotEmpty().WithMessage("O campo é obrigatório");
            RuleFor(x => x.SystemFeatureID).NotEmpty().WithMessage("O campo é obrigatório");


           //RuleFor(x => x.AllowAdd).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowAddRemove).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowChangeStatus).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowDelete).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowExportExcel).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowReportView).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowUpdate).NotEmpty().WithMessage("O campo é obrigatório");
            //RuleFor(x => x.AllowView).NotEmpty().WithMessage("O campo é obrigatório");
        }
    }
}  