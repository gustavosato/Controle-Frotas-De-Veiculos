using ControleVeiculos.Domain.Command.Customers;
using ControleVeiculos.Domain.Entities.Customers;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface ICustomerService : IDisposable
    {
        void Add(MaintenanceCustomerCommand command);
        void Update(MaintenanceCustomerCommand command);
        Result<Customer> GetByID(int customerID);
        IPagedList<Customer> GetAll(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Customer> GetAllAssociateCustomerByUserID(string userID, string customerID);
        IList<Customer> GetAllGroupCompanies(string customerID);
        IList<Customer> GetAllNoGroupCompanies(string customerID);
        IPagedList<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int customerID);
        string GetCustomerNameByID(int customerID);
    }
}
