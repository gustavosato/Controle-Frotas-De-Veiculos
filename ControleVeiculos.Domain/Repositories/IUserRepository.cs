using ControleVeiculos.Domain.Command.Users;
using ControleVeiculos.Domain.Entities.Users;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IUserRepository
    {
        string Add(User user);
        void Update(User user);
        User GetByID(int userID);
        List<User>GetAll(FilterUserCommand command);
        List<User>GetAll(int userID);
        User GetByEmail(string email);
        void Delete(int userID);
        string GetUserNameByID(int userID);
    }
}
