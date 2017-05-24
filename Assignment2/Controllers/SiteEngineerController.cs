using Assignment2.Helpers;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Roles.SITE_ENGINEER)]
    public class SiteEngineerController : Controller
    {
        private SiteEngineerHelper siteEngineerHelper = new SiteEngineerHelper();
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
                return RedirectToAction("CreateNewClient");                    //need to change all the redirect function, action and controller here for every buttons
            }
            else if (Button.Equals("View All Clients In Same District"))
            {
                return RedirectToAction("ViewAllClients");
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
            var viewModel = new CreateNewClientViewModel();
            viewModel.clientDistrict = siteEngineerHelper.GetDistrictForSiteManager();
            return View(viewModel);
        }

        public ActionResult ViewAllClients()
        {
            IList<GetAllClientsViewModel> viewModels= new List<GetAllClientsViewModel>();
            viewModels = siteEngineerHelper.GetAllClientsForUser();
            return View(viewModels);
        }

        [HttpPost]
        public ActionResult CreateNewClient(CreateNewClientViewModel viewModel)
        {
            var createClientHelper = new SiteEngineerHelper();
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
