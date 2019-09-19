using Lean.Test.Cloud.Domain.Command.DemandsUsers;

namespace Lean.Test.Cloud.Domain.Entities.DemandsUsers
{
    public static class DemandUserExtensions
    {
        public static Result<DemandUser> GetDemandUser(this DemandUser demandUser)
        {
            return Result.Ok(0, "", demandUser);
        }

        public static DemandUser Map(this DemandUser demandUser, MaintenanceDemandUserCommand command)
        {
            demandUser.userID = command.UserID;
            demandUser.demandID = command.DemandID;

            return demandUser;
        }
    }
}
