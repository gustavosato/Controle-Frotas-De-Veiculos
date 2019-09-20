using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.Historicals;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.Historicals;
using ControleVeiculos.Domain.Entities.Historicals;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;


namespace ControleVeiculos.MVC.Controllers
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