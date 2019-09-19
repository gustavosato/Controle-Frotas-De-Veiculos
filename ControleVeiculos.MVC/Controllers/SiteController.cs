using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class SiteController : BaseController
    {
       public ActionResult Index()
        {
            Session["userID"] = null;

            return View();
           
        }

    }
}