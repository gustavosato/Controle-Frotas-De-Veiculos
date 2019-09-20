using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Customers;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Customers;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class CustomerService : BaseAppService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Add(MaintenanceCustomerCommand command)
        {
            Customer customer = new Customer();

            customer = customer.Map(command);

            _customerRepository.Add(customer);
        }

        public void Update(MaintenanceCustomerCommand command)
        {
            Customer customer = new Customer();

            customer = customer.Map(command);

            _customerRepository.Update(customer);
        }

        public Result<Customer> GetByID(int customerID)
        {
            var customer = _customerRepository.GetByID(customerID);

            return Result.Ok<Customer>(0, "", customer);
        }

        public IPagedList<Customer> GetAll(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var customer = _customerRepository.GetAll(command);

            return new PagedList<Customer>(customer, pageIndex, pageSize);
        }

        public IList<Customer> GetAllAssociateCustomerByUserID(string userID, string customerID)
        {
            var customers = _customerRepository.GetAllAssociateCustomerByUserID(userID, customerID);

            return new List<Customer>(customers);
        }

        public IList<Customer> GetAllGroupCompanies(string customerID)
        {
            var customers = _customerRepository.GetAllGroupCompanies(customerID);

            return new List<Customer>(customers);
        }

        public IList<Customer> GetAllNoGroupCompanies(string customerID)
        {
            var customers = _customerRepository.GetAllNoGroupCompanies(customerID);

            return new List<Customer>(customers);
        }

        public IPagedList<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var customers = _customerRepository.GetAllAssociateCustomerByUserID(command);

            return new PagedList<Customer>(customers, pageIndex, pageSize);
        }

        public IPagedList<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var customers = _customerRepository.GetAllNoAssociateCustomerByUserID(command);

            return new PagedList<Customer>(customers, pageIndex, pageSize);
        }

        public void Delete(int customerID)
        {
            _customerRepository.Delete(customerID);
        }

        public string GetCustomerNameByID(int customerID)
        {
            return _customerRepository.GetCustomerNameByID(customerID);
        }
    }
}

