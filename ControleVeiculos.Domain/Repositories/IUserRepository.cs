using Lean.Test.Cloud.Domain.Command.Users;
using Lean.Test.Cloud.Domain.Entities.Users;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IUserRepository
    {
        string Add(User user);
        void Update(User user);
        User GetByID(int userID);
        List<User> GetAll(FilterUserCommand command);
        List<User> GetAllAssociateUserByCustomerID(FilterUserCommand command);
        List<User> GetAllNoAssociateUserByCustomerID(FilterUserCommand command);
        List<User> GetAllAssociateUserByDemandID(FilterUserCommand command);
        List<User> GetAllAssociateUserByGroupID(FilterUserCommand command);
        List<User> GetAllNoAssociateUserByDemandID(FilterUserCommand command);
        List<User> GetAllNoAssociateUserByGroupID(FilterUserCommand command);
        List<User> GetAll(int userID);
        User GetByEmail(string email);
        void Delete(int userID);
        string GetUserNameByID(int userID);
    }
}
