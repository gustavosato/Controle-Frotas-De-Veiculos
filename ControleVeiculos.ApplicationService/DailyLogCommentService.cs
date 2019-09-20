﻿using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.DailyLogComments;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.DailyLogComments;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class DailyLogCommentService : BaseAppService, IDailyLogCommentService
    {
        private readonly IDailyLogCommentRepository _dailyLogCommentRepository;

        public DailyLogCommentService(IDailyLogCommentRepository dailyLogCommentRepository)
        {
            _dailyLogCommentRepository = dailyLogCommentRepository;
        }

        public void Add(MaintenanceDailyLogCommentCommand command)
        {
            DailyLogComment dailyLogComment = new DailyLogComment();

            dailyLogComment = dailyLogComment.Map(command);

            _dailyLogCommentRepository.Add(dailyLogComment);
        }

        public void Update(MaintenanceDailyLogCommentCommand command)
        {
            DailyLogComment dailyLogComment = new DailyLogComment();

            dailyLogComment = dailyLogComment.Map(command);

            _dailyLogCommentRepository.Update(dailyLogComment);
        }

        public Result<DailyLogComment> GetByID(int dailyLogsCommentID)
        {
            var dailyLogComment = _dailyLogCommentRepository.GetByID(dailyLogsCommentID);

            return Result.Ok<DailyLogComment>(0, "", dailyLogComment);
        }

        public IPagedList<DailyLogComment> GetAll(FilterDailyLogCommentCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var dailyLogComment = _dailyLogCommentRepository.GetAll(command);

            return new PagedList<DailyLogComment>(dailyLogComment, pageIndex, pageSize);
        }

        public void Delete(int dailyLogsCommentID)
        {
            _dailyLogCommentRepository.Delete(dailyLogsCommentID);
        }
    }
}

