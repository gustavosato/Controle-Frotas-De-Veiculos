using ControleVeiculos.Domain.Command.CustomersUsers;

namespace ControleVeiculos.Domain.Entities.CustomersUsers
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
