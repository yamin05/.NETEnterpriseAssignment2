using Assignment2.Helpers;
using Assignment2.Models;
using System;
using System.Web.Mvc;
using Assignment2.Models.Database_Models;
using System.Collections.Generic;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Roles.MANAGER)]
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Button)
        {
            if (Button.Equals("List of Intervention to approve or cancel in your district"))
            {
                return RedirectToAction("ListOfInterventionsForManager");                   
            }
            else if (Button.Equals("List of associated Interevntions"))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (Button.Equals("Change Password"))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult ListOfInterventionsForManager()
        {

            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ListOfInterventionsForManager(IList<ListInterventionViewModel> viewModel)
        {
            var ApproveOrCancelIntervention = new ListInterventionHelper();
            try
            {
                viewModel = ApproveOrCancelIntervention.ListOfProposedInterventions();
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }
    }
}