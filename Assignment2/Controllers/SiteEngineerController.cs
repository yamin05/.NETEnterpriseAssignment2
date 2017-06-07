using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using Assignment2.Helpers;
using System.Web.UI.WebControls;

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

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method is used to view all associated interventions to user
        /// </summary>
        /// <returns>List</returns>
        
        [HttpPost]
        public ActionResult Index(string Button)
        {
            return RedirectToAction(Button);
        }

        public ActionResult ListInterventions()
        {
            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            try
            {
                viewModel = siteEngineerHelper.ListInterventions();
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
            var viewModel = new CreateNewInterventionViewModel();
            try
            {
                var clients = siteEngineerHelper.ListCurrentClients();
                var listItems = new List<ListItem>();
                foreach (var client in clients)
                {
                    listItems.Add(new ListItem { Text = client.ClientName, Value = client.ClientId.ToString() });
                }
                ViewBag.clients = new SelectList(listItems, "Value", "Text");
                var interventionTypes = siteEngineerHelper.ListInterventionTypes();
                var listItems2 = new List<ListItem>();
                foreach (var type in interventionTypes)
                {
                    listItems2.Add(new ListItem { Text = type.InterventionTypeName, Value = type.InterventionTypeId.ToString() });
                }
                ViewBag.interventionTypes = new SelectList(listItems2, "Value", "Text", interventionTypes.FirstOrDefault());
                viewModel.interventionCost = interventionTypes.FirstOrDefault().InterventionTypeCost;
                viewModel.interventionHours = interventionTypes.FirstOrDefault().InterventionTypeHours;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
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
                    createInterventionHelper.CreateIntervention(viewModel.clientId, viewModel.interventionTypeId, viewModel.interventionCost, viewModel.interventionHours, viewModel.comments);
                    ModelState.AddModelError("success", "Intervention Created Successfully!");
                }
                else
                {
                    ModelState.AddModelError("success", "Sorry, You don't have any client to assosiate intervention with!, Hint: Create a client first!");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex);
            }
            var clients = siteEngineerHelper.ListCurrentClients();
            var listItems3 = new List<ListItem>();
            foreach (var client in clients)
            {
                listItems3.Add(new ListItem { Text = client.ClientName, Value = client.ClientId.ToString() });
            }
            ViewBag.clients = new SelectList(listItems3, "Value", "Text", listItems3.Where(i => i.Value.Equals(viewModel.clientId)).Select(i => i));
            var interventionTypes2 = siteEngineerHelper.ListInterventionTypes();
            var listItems4 = new List<ListItem>();
            foreach (var type in interventionTypes2)
            {
                listItems4.Add(new ListItem { Text = type.InterventionTypeName, Value = type.InterventionTypeId.ToString() });
            }
            ViewBag.interventionTypes = new SelectList(listItems4, "Value", "Text", listItems4.Where(i => i.Value.Equals(viewModel.interventionTypeId)).Select(i => i));
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
                interDao.UpdateComments(InterList.InterventionId, InterList.comments);

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
            try
            {
                viewModel = siteEngineerHelper.ListOfClients();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        public ActionResult ViewClientsInDistrict()
        {
            IList<ListClientsViewModel> viewModel = new List<ListClientsViewModel>();
            try
            {
                viewModel = siteEngineerHelper.ListOfClientsInDistrict();
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
            viewModel.clientDistrict = siteEngineerHelper.GetDistrictForUser();
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
