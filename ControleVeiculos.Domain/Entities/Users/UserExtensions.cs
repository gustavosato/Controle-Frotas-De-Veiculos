using ControleVeiculos.Domain.Command.Users;
using System;

namespace ControleVeiculos.Domain.Entities.Users
{
    public static class UserExtensions
    {
        public static Result<User> GetUser(this User user)
        {
            return Result.Ok(0, "", user);
        }

        public static User Map(this User user, MaintenanceUserCommand command)
        {
            user.userID = command.UserID;
            user.userName = command.UserName;
            user.password = command.Password;
            user.email = command.Email;
            user.cellNumber = command.CellNumber;
            user.departamentoID = command.DepartamentoID;
            user.description = command.Description;
            user.firstAccess = command.FirstAccess;
            user.isAdmin = command.IsAdmin;
            user.isActive = command.IsActive;
            user.rg = command.RG;
            user.cpf = command.CPF;
            user.dateOfBirth = command.DateOfBirth;
            user.homeAddress = command.HomeAddress;
            user.cep = command.CEP;
            user.district = command.District;
            user.city = command.City;
            user.state = command.State;
            user.homePhone = command.HomePhone;

            return user;
        }
    }
}
