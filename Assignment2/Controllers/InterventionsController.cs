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
    [Authorize(Roles = Roles.SITE_ENGINEER + "," + Roles.MANAGER)]
    public class InterventionsController : Controller
    {
        private CustomDBContext db = new CustomDBContext();

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


        public ActionResult Create()
        {
            var currentUserId = Utils.getInstance.GetCurrentUserId();
            ViewBag.ClientId = new SelectList(db.Clients.Where(c => c.CreatedBy.UserId == currentUserId), "ClientId", "ClientName");
            ViewBag.InterventionTypeId = new SelectList(db.InterventionTypes, "InterventionTypeId", "InterventionTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewInterventionViewModel viewModel)
        {
            var createInterventionHelper = new InterventionHelper();

            try
            {
                createInterventionHelper.CreateIntervention(viewModel.clientId, viewModel.interventionTypeId, viewModel.interventionCost, viewModel.interventionHours);
                ModelState.AddModelError("success", "Intervention Created Successfully!");

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
