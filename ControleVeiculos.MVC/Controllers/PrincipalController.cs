using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Controllers
{
    public class PrincipalController : BaseController
    {
       public ActionResult Index()
        {

            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
           
        }

    }
}