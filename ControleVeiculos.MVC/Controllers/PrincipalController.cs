using System.Web.Mvc;

namespace ControleVeiculos.MVC.Controllers
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