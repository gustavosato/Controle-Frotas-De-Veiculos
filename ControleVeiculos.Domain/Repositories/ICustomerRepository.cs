using ControleVeiculos.Domain.Command.Customers;
using ControleVeiculos.Domain.Entities.Customers;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Update(Customer customer);
        Customer GetByID(int customerID);
        List<Customer> GetAll(FilterCustomerCommand command);
        List<Customer> GetAllAssociateCustomerByUserID(string userID, string customerID);
        List<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command);
        List<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command);
        List<Customer> GetAllGroupCompanies(string customerID);
        List<Customer> GetAllNoGroupCompanies(string customerID);
        void Delete(int customerID);
        string GetCustomerNameByID(int customerID);
    }
}
