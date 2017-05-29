using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using Assignment2.Helpers;


namespace Assignment2.Controllers
{
    [Authorize (Roles = "Site_Engineer")]
    public class ClientsController : Controller
    {
        private CustomDBContext db = new CustomDBContext();
        private SiteEngineerHelper siteEngineerHelper = new SiteEngineerHelper();
        private Dao d = new Dao();

        public ActionResult Index()
        {
            IList<ListClientsViewModel> viewModel = new List<ListClientsViewModel>();
            var userClients = new ClientHelper();
            try
            {
                viewModel = userClients.ListOfClients();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }


        public ActionResult Create()
        {
            var viewModel = new CreateNewClientViewModel();
            viewModel.clientDistrict = siteEngineerHelper.GetDistrictForSiteManager();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewClientViewModel viewModel)
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
