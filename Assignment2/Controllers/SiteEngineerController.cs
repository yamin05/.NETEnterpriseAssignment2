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
    /// With the help of SiteEngineer View And InterventionViewModel, ClientViewModel this controller is used for create edit view Interventions, clients
    /// </summary>
    [Authorize(Roles = Roles.SITE_ENGINEER)]
    public class SiteEngineerController : Controller
    {
        private CustomDBContext db = new CustomDBContext();

        private SiteEngineerHelper siteEngineerHelper = new SiteEngineerHelper();
        private Dao d = new Dao();


        /// <summary>
        /// This method is used to view all associated interventions to user
        /// </summary>
        /// <returns>List</returns>
        public ActionResult Index()
        {
            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            var usersIntervention = new InterventionHelper();
            try
            {
                viewModel = usersIntervention.ListOfUsersInterventions();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        /// <summary>
        /// This method is used for the view page of creating interventions
        /// </summary>
        /// <returns>View</returns>
        public ActionResult CreateIntervention()
        {
            var currentUserId = Utils.getInstance.GetCurrentUserId();
            ViewBag.ClientId = new SelectList(db.Clients.Where(c => c.CreatedBy.District.Equals(c.ClientDistrict)), "ClientId", "ClientName");
            ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName");
            return View();
        }

        /// <summary>
        /// This method is used for calling CreateIntervention method to create a new intervention by passing user enterd values in fields
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIntervention(CreateNewInterventionViewModel viewModel)
        {
            var createInterventionHelper = new InterventionHelper();

            try
            {
                if (viewModel.clientId != 0)
                {
                    createInterventionHelper.CreateIntervention(viewModel.clientId, viewModel.interventionTypeId, viewModel.interventionCost, viewModel.interventionHours);
                    ModelState.AddModelError("success", "Intervention Created Successfully!");


                    //Getting values from DB for Dropdown List
                    var currentUserId = Utils.getInstance.GetCurrentUserId();
                    ViewBag.ClientId = new SelectList(db.Clients.Where(c => c.CreatedBy.District.Equals(c.ClientDistrict)), "ClientId", "ClientName");
                    ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName");
                }
                else
                {
                    var currentUserId = Utils.getInstance.GetCurrentUserId();
                    ViewBag.ClientId = new SelectList(db.Clients.Where(c => c.CreatedBy.District.Equals(c.ClientDistrict)), "ClientId", "ClientName");
                    ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName");
                    ModelState.AddModelError("success", "Sorry, You don't have any client to assosiate intervention with!, Hint: Create a client first!");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }


        /// <summary>
        /// This method is used for view page of Edit Interventions
        /// </summary>
        /// <param name="id">Id of intervention to be edited</param>
        /// <returns>View</returns>
        public ActionResult EditIntervention(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var intervention = new InterventionHelper();
            var inter = intervention.GetIntervention(id);
            if (inter == null)
            {
                return HttpNotFound();
            }
            var statuslist = intervention.GetPossibleStatusUpdateForInterventionForSiteEngineer(inter.Status);
            ViewBag.Status = new SelectList(statuslist.Keys);
            return View(inter);
        }


        /// <summary>
        /// This method is used for calling UpdateIntervention method to update an intervention by passing user enterd values in fields
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIntervention(ListInterventionViewModel InterList)
        {
            if (ModelState.IsValid)
            {
                InterventionsDao interDao = new InterventionsDao();

                interDao.UpdateLife(InterList.InterventionId, InterList.Condition);

                if (InterList.Status.Equals(Status.APPROVED))
                {
                    interDao.UpdateInterventionStatus_ToAppoved(InterList.InterventionId);
                    ModelState.AddModelError("success", "Intervention Updated Successfully!");
                }
                else if (InterList.Status.Equals(Status.COMPLETED))
                {
                    interDao.UpdateInterventionStatus_ToCompleted(InterList.InterventionId);
                    ModelState.AddModelError("success", "Intervention Updated Successfully!");
                }
                else if (InterList.Status.Equals(Status.CANCELLED))
                {
                    interDao.UpdateInterventionStatus_ToCancelled(InterList.InterventionId);
                    ModelState.AddModelError("success", "Intervention Updated Successfully!");
                }


            }
            return Redirect(Request.QueryString["url"]);
        }

        /// <summary>
        /// This method is used to view all assosiated Clients to user
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ViewClients()
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
        /// This method is used to list all Interventions assosiated to client
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <returns>View</returns>
        public ActionResult ViewClientsInterventions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            var usersIntervention = new InterventionHelper();
            try
            {
                viewModel = usersIntervention.ListOfClientsInterventions(id);

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
        public ActionResult CreateClient()
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
        public ActionResult CreateClient(CreateNewClientViewModel viewModel)
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
