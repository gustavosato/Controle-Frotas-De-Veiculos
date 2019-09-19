using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.Historicals;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.Domain.Entities.Historicals;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;


namespace Lean.Test.Cloud.MVC.Controllers
{
    public class HistoricalController : BaseController
    {
        private readonly IHistoricalService _historicalService;

        public HistoricalController(IHistoricalService historicalService)
        {
            _historicalService = historicalService;
        }

        [HttpPost]
        public ActionResult GetAll(DataSourceRequest request, HistoricalModel model)
        {
            var historical = _historicalService.GetAll(new FilterHistoricalCommand
            {
                RecordID = model.RecordID,
                SystemFeatureID = model.SystemFeatureID
            }, request.Page - 1, request.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = historical.Select(x =>
                {
                    var historicalModel = x.ToModel();

                    return historicalModel;
                }),
                Total = historical.TotalCount
            };

            return Json(gridModel);
        }
    }
}