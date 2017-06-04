using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect(Utils.getInstance.getHomePageURL());
            }
            else
            {
                return View();
            }
        }
    }
}