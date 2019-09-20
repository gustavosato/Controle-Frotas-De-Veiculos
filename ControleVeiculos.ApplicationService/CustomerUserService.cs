using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Command.CustomersUsers;
using ControleVeiculos.Domain.Entities.CustomersUsers;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Entities.Customers;
using ControleVeiculos.Domain.Command.Customers;



namespace ControleVeiculos.ApplicationService
{
    public class CustomerUserService : BaseAppService, ICustomerUserService
    {
        private readonly ICustomerUserRepository _customerUserRepository;

        public CustomerUserService(ICustomerUserRepository customerUserRepository)
        {
            _customerUserRepository = customerUserRepository;
        }

        public void Add(MaintenanceCustomerUserCommand command)
        {
            CustomerUser customerUser = new CustomerUser();

            customerUser = customerUser.Map(command);

            _customerUserRepository.Add(customerUser);
        }

        public void Delete(int customerID, int userID)
        {
            _customerUserRepository.Delete(customerID, userID);
        }

        public IPagedList<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var customers = _customerUserRepository.GetAllAssociateCustomerByUserID(command);

            return new PagedList<Customer>(customers, pageIndex, pageSize);
        }

        public IPagedList<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var customers = _customerUserRepository.GetAllNoAssociateCustomerByUserID(command);

            return new PagedList<Customer>(customers, pageIndex, pageSize);
        }


    }
}

