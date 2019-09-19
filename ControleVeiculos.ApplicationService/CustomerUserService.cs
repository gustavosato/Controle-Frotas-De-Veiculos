using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Command.CustomersUsers;
using Lean.Test.Cloud.Domain.Entities.CustomersUsers;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Entities.Customers;
using Lean.Test.Cloud.Domain.Command.Customers;



namespace Lean.Test.Cloud.ApplicationService
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

