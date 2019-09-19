using Lean.Test.Cloud.Domain.Command.DailyLogComments;
using System;

namespace Lean.Test.Cloud.Domain.Entities.DailyLogComments
{
    public static class DailyLogCommentExtensions
    {
        public static Result<DailyLogComment> GetDailyLogComment(this DailyLogComment dailyLogComment)
        {
            return Result.Ok(0, "", dailyLogComment);
        }

        public static DailyLogComment Map(this DailyLogComment dailyLogComment, MaintenanceDailyLogCommentCommand command)
        {

            dailyLogComment.dailyLogsCommentID = command.DailyLogsCommentID;
            dailyLogComment.descrition = command.Descrition;
            dailyLogComment.createdByID = command.CreatedByID;
            dailyLogComment.creationDate = command.CreationDate;
            dailyLogComment.modifiedByID = command.ModifiedByID;
            dailyLogComment.lastModifiedDate = DateTime.Now.ToString();

            return dailyLogComment;
        }
    }
}
