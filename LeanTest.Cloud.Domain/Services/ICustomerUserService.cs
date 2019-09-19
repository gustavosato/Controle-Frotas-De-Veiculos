using Lean.Test.Cloud.Domain.Command.CustomersUsers;
using Lean.Test.Cloud.Domain.Command.Customers;
using Lean.Test.Cloud.Domain.Entities.Customers;
using Lean.Test.Cloud.Domain.Entities.CustomersUsers;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ICustomerUserService : IDisposable
    {
        void Add(MaintenanceCustomerUserCommand command);
        void Delete(int customerID, int userID);
        IPagedList<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
