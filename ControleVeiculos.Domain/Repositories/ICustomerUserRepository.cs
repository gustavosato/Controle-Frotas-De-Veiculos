using ControleVeiculos.Domain.Entities.CustomersUsers;
using ControleVeiculos.Domain.Command.Customers;
using ControleVeiculos.Domain.Command.CustomersUsers;
using ControleVeiculos.Domain.Entities.Customers;
using System.Collections.Generic;


namespace ControleVeiculos.Domain.Repositories
{
    public interface ICustomerUserRepository
    {
        void Add(CustomerUser customerUser);
        void Delete(int custumerID, int userID);
        List<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command);
        List<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command);

    }
}
