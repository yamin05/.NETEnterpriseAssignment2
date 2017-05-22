using Assignment2.Models;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Roles.SITE_ENGINEER)]
    public class SiteEngineerController : Controller
    {
        // GET: SiteEngineer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Button)
        {
            if (Button.Equals("Create New Client"))
            {
                return RedirectToAction("Login", "Account");                    //need to change all the redirect function, action and controller here
            }
            else if (Button.Equals("View All Clients Created By You"))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (Button.Equals("View All Clients In Same District"))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (Button.Equals("Create New Intervention"))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (Button.Equals("View All Interventions Created By You"))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (Button.Equals("Change Password"))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
