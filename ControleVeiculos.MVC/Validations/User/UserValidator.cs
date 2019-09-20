using FluentValidation;
using ControleVeiculos.MVC.Models.Users;

namespace ControleVeiculos.MVC.Validations.Users
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("O campo é obrigatório.");

            RuleFor(x => x.UserName).Length(6, 60);

            RuleFor(x => x.Password).Length(6, 20);

            RuleFor(x => x.PasswordNew).Length(6, 20);

            RuleFor(x => x.PasswordNewConfirm).Length(6, 20);

            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo é obrigatório");

            RuleFor(x => x.Email).Matches(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").WithMessage("O e-mail é inválido.");

            RuleFor(x => x.EmailNew).NotEmpty().WithMessage("O campo é obrigatório");

            RuleFor(x => x.EmailNew).Matches(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").WithMessage("O e-mail é inválido.");

            RuleFor(x => x.EmailNewConfirm).NotEmpty().WithMessage("O campo é obrigatório");

            RuleFor(x => x.EmailNewConfirm).Matches(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").WithMessage("O e-mail é inválido.");

            RuleFor(x => x.EmailNewConfirm).Equal(x => x.EmailNew);

            RuleFor(x => x.Password).NotEmpty().WithMessage("O campo é obrigatório.");

            RuleFor(x => x.PasswordNew).NotEmpty().WithMessage("O campo é obrigatório.");

            RuleFor(x => x.PasswordNewConfirm).NotEmpty().WithMessage("O campo é obrigatório.");

            RuleFor(x => x.PasswordNewConfirm).Equal(x => x.PasswordNew);

            RuleFor(x => x.CellNumber).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.DepartmentID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.SupervisorID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.FunctionID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.FunctionLevelID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.LevelClassificationID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.ContractTypeID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.HourTypeID).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.TotalCost).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.StartJob).NotEmpty().WithMessage("O campo é obrigatório.");
            RuleFor(x => x.AccessToDate).NotEmpty().WithMessage("O campo é obrigatório.");


            //exemplo
            //RuleFor(x => x.Placa).Matches(@"^[a-zA-Z]{3}\d{4}$").WithMessage("O campo placa é inválido");
            //RuleFor(x => x.AnoFabricacao).Matches(@"^\d{4}").WithMessage("O campo ano fabricação é inválido");
        }
    }
}