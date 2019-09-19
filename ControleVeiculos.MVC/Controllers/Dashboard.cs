using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.MVC.Infrastructure.KendoUI;
using Lean.Test.Cloud.MVC.Models.TimeReleases;
using System;
using System.Linq;
using Lean.Test.Cloud.MVC.Extensions;
using System.Web.Mvc;
using Lean.Test.Cloud.Domain.Command.TimeReleases;
using Lean.Test.Cloud.Domain.Entities.TimeReleases;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.MVC.Infrastructure.Mvc;

namespace Lean.Test.Cloud.MVC.Controllers
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