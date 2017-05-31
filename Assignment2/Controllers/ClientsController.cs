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
    /// <summary>
    /// With the help of Clients View And ClientVieModel this controller is used for create  view Clients
    /// </summary>
    [Authorize (Roles = "Site_Engineer")]
    public class ClientsController : Controller
    {
        private CustomDBContext db = new CustomDBContext();
        private SiteEngineerHelper siteEngineerHelper = new SiteEngineerHelper();
        private Dao d = new Dao();


        /// <summary>
        /// This method is used to view all associated Clients to user
        /// </summary>
        /// <returns>View</returns>
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


        /// <summary>
        /// This method is used for the view page of creating clients
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            var viewModel = new CreateNewClientViewModel();
            viewModel.clientDistrict = siteEngineerHelper.GetDistrictForSiteManager();
            return View(viewModel);
        }

        /// <summary>
        /// This method is used for calling CreateClient method to create a new client by passing user enterd values in fields
        /// </summary>
        /// <returns>Biew</returns>
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
