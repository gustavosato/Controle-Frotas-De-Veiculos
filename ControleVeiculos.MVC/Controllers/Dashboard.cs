using ControleVeiculos.Domain.Services;
using ControleVeiculos.MVC.Infrastructure.KendoUI;
using ControleVeiculos.MVC.Models.TimeReleases;
using System;
using System.Linq;
using ControleVeiculos.MVC.Extensions;
using System.Web.Mvc;
using ControleVeiculos.Domain.Command.TimeReleases;
using ControleVeiculos.Domain.Entities.TimeReleases;
using ControleVeiculos.Domain;
//using ControleVeiculos.MVC.Infrastructure.Mvc;

namespace ControleVeiculos.MVC.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IExportManagerService _exportManagerService;

        public DashboardController(IExportManagerService exportManagerService)
        {
            _exportManagerService = exportManagerService;
        }

        public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //var model = new DashboardModel();

            return View();
        }

    }
}