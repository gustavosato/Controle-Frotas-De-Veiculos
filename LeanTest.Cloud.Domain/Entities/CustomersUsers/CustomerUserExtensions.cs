using Lean.Test.Cloud.Domain.Command.CustomersUsers;

namespace Lean.Test.Cloud.Domain.Entities.CustomersUsers
{
    public static class CustomerUserExtensions
    {
        public static Result<CustomerUser> GetCustomerUser(this CustomerUser customerUser)
        {
            return Result.Ok(0, "", customerUser);
        }

        public static CustomerUser Map(this CustomerUser customerUser, MaintenanceCustomerUserCommand command)
        {
            customerUser.customerID = command.CustomerID;
            customerUser.userID = command.UserID;

            return customerUser;
        }
    }
}
