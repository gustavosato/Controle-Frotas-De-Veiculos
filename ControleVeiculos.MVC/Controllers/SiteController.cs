using System.Web.Mvc;

namespace ControleVeiculos.MVC.Controllers
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