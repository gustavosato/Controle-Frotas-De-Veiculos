using Lean.Test.Cloud.Domain.Command.Users;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Users
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
            user.functionID = command.FunctionID;
            user.functionLevelID = command.FunctionLevelID;
            user.levelClassificationID = command.LevelClassificationID;
            user.departmentID = command.DepartmentID;
            user.totalCost = command.TotalCost;
            user.supervisorID = command.SupervisorID;
            user.description = command.Description;
            user.firstAccess = command.FirstAccess;
            user.isAdmin = command.IsAdmin;
            user.lastAccessDate = command.LastAccessDate;
            user.lastIPAccess = command.LastIPAccess;
            user.isActive = command.IsActive;
            user.accessToDate = command.AccessToDate;
            user.updateRecordTo = command.UpdateRecordTo;
            user.releaseDateUpdateRecordTo = command.ReleaseDateUpdateRecordTo;
            user.startJob = command.StartJob;
            user.endJob = command.EndJob;
            user.contractTypeID = command.ContractTypeID;
            user.hourTypeID = command.HourTypeID;
            user.rg = command.RG;
            user.cpf = command.CPF;
            user.dateOfBirth = command.DateOfBirth;
            user.homeAddress = command.HomeAddress;
            user.cep = command.CEP;
            user.district = command.District;
            user.city = command.City;
            user.state = command.State;
            user.homePhone = command.HomePhone;
            user.typeBankAccountID = command.TypeBankAccountID;
            user.bankName = command.BankName;
            user.typePersonID = command.TypePersonID;
            user.agency = command.Agency;
            user.bankAccount = command.BankAccount;
            user.socialReason = command.SocialReason;
            user.cnpj = command.CNPJ;
            user.optingSimple = command.OptingSimple;
            user.isEmployee = command.IsEmployee;
            user.registeredCity = command.RegisteredCity;
            user.createdByID = command.CreatedByID;
            user.creationDate = command.CreationDate;
            user.modifiedByID = command.ModifiedByID;
            user.lastModifiedDate = command.LastModifiedDate;

            return user;
        }
    }
}
