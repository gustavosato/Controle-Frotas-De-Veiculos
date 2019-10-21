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
                DepartamentoID = entity.departamentoID,
                Description = entity.description,
                IsAdmin = entity.isAdmin,
                FirstAccess = entity.firstAccess,
                IsActive = entity.isActive,
                RG = entity.rg,
                CPF= entity.cpf,
                DateOfBirth = entity.dateOfBirth,
                HomeAddress = entity.homeAddress,
                CEP = entity.cep,
                District = entity.district,
                City = entity.city,
                State = entity.state,
                HomePhone = entity.homePhone,
            };

            return model;
        }
    }
}