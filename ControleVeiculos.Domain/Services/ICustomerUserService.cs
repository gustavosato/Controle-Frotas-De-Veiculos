using ControleVeiculos.Domain.Command.CustomersUsers;
using ControleVeiculos.Domain.Command.Customers;
using ControleVeiculos.Domain.Entities.Customers;
using ControleVeiculos.Domain.Entities.CustomersUsers;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ICustomerUserService : IDisposable
    {
        void Add(MaintenanceCustomerUserCommand command);
        void Delete(int customerID, int userID);
        IPagedList<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
