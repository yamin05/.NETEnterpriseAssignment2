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
    /// With the help of Intervention View And InterventionViewModel this controller is used for create edit view Interventions
    /// </summary>
    [Authorize(Roles = Roles.SITE_ENGINEER)]
    public class InterventionsController : Controller
    {
        private CustomDBContext db = new CustomDBContext();

        /// <summary>
        /// This method is used to view all associated interventions to user
        /// </summary>
        /// <returns>List</returns>
        public ActionResult Index()
        {
            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            var usersIntervention  = new InterventionHelper();
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
        public ActionResult Create()
        {
            var currentUserId = Utils.getInstance.GetCurrentUserId();
            ViewBag.ClientId = new SelectList(db.Clients.Where(c => c.CreatedBy.UserId == currentUserId), "ClientId", "ClientName");
            ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName");
            return View();
        }

        /// <summary>
        /// This method is used for calling CreateIntervention method to create a new intervention by passing user enterd values in fields
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewInterventionViewModel viewModel)
        {
            var createInterventionHelper = new InterventionHelper();

            try
            {
                createInterventionHelper.CreateIntervention(viewModel.clientId, viewModel.interventionTypeId, viewModel.interventionCost, viewModel.interventionHours);
                ModelState.AddModelError("success", "Intervention Created Successfully!");


                //Getting values from DB for Dropdown List
                var currentUserId = Utils.getInstance.GetCurrentUserId();
                ViewBag.ClientId = new SelectList(db.Clients.Where(c => c.CreatedBy.UserId == currentUserId), "ClientId", "ClientName");
                ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName");

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervention intervention = db.Interventions.Find(id);
            if (intervention == null)
            {
                return HttpNotFound();
            }

            ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName", intervention.InterventionTypeId);
            return View(intervention);
        }


        /// <summary>
        /// This method is used for calling UpdateIntervention method to update an intervention by passing user enterd values in fields
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterventionId,CreatedByUserId,ApprovedByUserId,InterventionTypeId,InterventionCost,InterventionHours,CreateDate,Status,Condition,ModifyDate")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intervention).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApprovedByUserId = new SelectList(db.Users, "UserId", "District", intervention.ApprovedByUserId);
            ViewBag.CreatedByUserId = new SelectList(db.Users, "UserId", "District", intervention.CreatedByUserId);
            ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName", intervention.InterventionTypeId);
            return View(intervention);
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
