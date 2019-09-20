using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.MVC.Models.Users;

namespace ControleVeiculos.MVC.Extensions
{
    public static class UserMappingExtensions
    {
        public static UserModel ToModel(this User entity)
        {
            if (entity == null)
                return null;

            var model = new UserModel
            {
                UserID = entity.userID,
                UserName = entity.userName,
                Email = entity.email,
                Password = entity.password,
                CellNumber = entity.cellNumber,
                FunctionID = entity.functionID,
                FunctionLevelID = entity.functionLevelID,
                LevelClassificationID = entity.levelClassificationID,
                DepartmentID = entity.departmentID,
                TotalCost = entity.totalCost,
                SupervisorID = entity.supervisorID,
                Description = entity.description,
                IsAdmin = entity.isAdmin,
                FirstAccess = entity.firstAccess,
                LastAccessDate = entity.lastAccessDate,
                LastIPAccess = entity.lastIPAccess,
                IsActive = entity.isActive,
                AccessToDate = entity.accessToDate,
                UpdateRecordTo = entity.updateRecordTo,
                ReleaseDateUpdateRecordTo = entity.releaseDateUpdateRecordTo,
                StartJob = entity.startJob,
                EndJob = entity.endJob,
                ContractTypeID = entity.contractTypeID,
                HourTypeID = entity.hourTypeID,
                RG = entity.rg,
                CPF= entity.cpf,
                DateOfBirth = entity.dateOfBirth,
                HomeAddress = entity.homeAddress,
                CEP = entity.cep,
                District = entity.district,
                City = entity.city,
                State = entity.state,
                HomePhone = entity.homePhone,
                TypeBankAccount = entity.typeBankAccountID,
                TypePerson = entity.typePersonID,
                Agency = entity.agency,
                BankAccount = entity.bankAccount,
                BankName = entity.bankName,
                SocialReason = entity.socialReason,
                CNPJ = entity.cnpj,
                OptingSimple = entity.optingSimple,
                IsEmployee = entity.isEmployee,
                RegisteredCity = entity.registeredCity,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}