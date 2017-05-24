using Assignment2.Helpers;
using Assignment2.Models;
using System;
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
                return RedirectToAction("CreateNewClient");                    //need to change all the redirect function, action and controller here
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

        public ActionResult CreateNewClient()
        {
            var createClientHelper = new CreateClientHelper();
            var viewModel = new CreateNewClientViewModel();
            viewModel.clientDistrict = createClientHelper.GetDistrictForSiteManager();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateNewClient(CreateNewClientViewModel viewModel)
        {
            var createClientHelper = new CreateClientHelper();
            try
            {
                createClientHelper.CreateClient(viewModel.clientName, viewModel.clientLocation, viewModel.clientDistrict);
                ModelState.AddModelError("success", "Client Created Successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            } 
            return View(viewModel);
        }
    }
}
