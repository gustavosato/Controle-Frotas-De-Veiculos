using Lean.Test.Cloud.Domain.Command.Customers;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Customers
{
    public static class CustomerExtensions
    {
        public static Result<Customer> GetCustomer(this Customer customer)
        {
            return Result.Ok(0, "", customer);
        }

        public static Customer Map(this Customer customer, MaintenanceCustomerCommand command)
        {
            customer.customerID = command.CustomerID;
            customer.customerName = command.CustomerName;
            customer.isActive = command.IsActive;
            customer.segmentID = command.SegmentID;
            customer.typeID = command.TypeID;
            customer.site = command.Site;
            customer.address = command.Address;
            customer.description = command.Description;
            customer.createdByID = command.CreatedByID;
            customer.creationDate = command.CreationDate;
            customer.modifiedByID = command.ModifiedByID;
            customer.lastModifiedDate = DateTime.Now.ToString();

            return customer;
        }
    }
}
