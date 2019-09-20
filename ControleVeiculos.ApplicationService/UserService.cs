using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Users;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class UserService : BaseAppService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Add(MaintenanceUserCommand command)
        {
            User user = new User();

            user = user.Map(command);

            return _userRepository.Add(user);
        }

        public void Update(MaintenanceUserCommand command)
        {
            User user = new User();

            user = user.Map(command);

            _userRepository.Update(user);
        }

        public Result<User> GetByID(int userid)
        {
            var user = _userRepository.GetByID(userid);

            return Result.Ok<User>(0, "", user);
        }

        public IPagedList<User> GetAllAssociateUserByCustomerID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAllAssociateUserByCustomerID(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }

        public IPagedList<User> GetAllNoAssociateUserByCustomerID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAllNoAssociateUserByCustomerID(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }

        public IPagedList<User> GetAllAssociateUserByDemandID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAllAssociateUserByDemandID(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }

        public IPagedList<User> GetAllNoAssociateUserByDemandID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAllNoAssociateUserByDemandID(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }



        public IPagedList<User> GetAllAssociateUserByGroupID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAllAssociateUserByGroupID(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }

        public IPagedList<User> GetAllNoAssociateUserByGroupID(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAllNoAssociateUserByGroupID(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }




        public IPagedList<User> GetAll(FilterUserCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _userRepository.GetAll(command);

            return new PagedList<User>(users, pageIndex, pageSize);
        }

        public IList<User> GetAll(int userID)
        {
            var user = _userRepository.GetAll(userID);

            return new List<User>(user);
        }

        public Result<User> GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            return Result.Ok<User>(0, "", user);
        }

        public void Delete(int codigo)
        {
            _userRepository.Delete(codigo);
        }

        public string GetUserNameByID(int userID)
        {
            return _userRepository.GetUserNameByID(userID);
        }
    }
}

