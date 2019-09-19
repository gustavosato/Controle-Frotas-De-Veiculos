using Lean.Test.Cloud.Domain.Entities.CustomersUsers;
using Lean.Test.Cloud.Domain.Command.Customers;
using Lean.Test.Cloud.Domain.Command.CustomersUsers;
using Lean.Test.Cloud.Domain.Entities.Customers;
using System.Collections.Generic;


namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ICustomerUserRepository
    {
        void Add(CustomerUser customerUser);
        void Delete(int custumerID, int userID);
        List<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command);
        List<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command);

    }
}
