using ControleVeiculos.Domain.Command.Users;
using ControleVeiculos.Domain.Entities.Users;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IUserService : IDisposable
    {
        string Add(MaintenanceUserCommand user);
        void Update(MaintenanceUserCommand user);
        IPagedList<User> GetAll(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<User> GetAllAssociateUserByCustomerID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<User> GetAllNoAssociateUserByCustomerID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<User> GetAllAssociateUserByDemandID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<User> GetAllAssociateUserByGroupID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<User> GetAllNoAssociateUserByDemandID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<User> GetAllNoAssociateUserByGroupID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<User> GetAll(int userID);
        Result<User> GetByID(int userID);
        Result<User> GetByEmail(string email);
        void Delete(int userID);
        string GetUserNameByID(int userID);
    }
}
