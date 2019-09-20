using System.Web.Mvc;

namespace ControleVeiculos.MVC.Controllers
{
    public class HomeController : BaseController
    {
       public ActionResult Index()
        {
            if (Session["userID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Site");
            }
        }

    }
}