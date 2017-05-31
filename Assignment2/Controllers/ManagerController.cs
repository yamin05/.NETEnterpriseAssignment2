using Assignment2.Helpers;
using Assignment2.Models;
using System;
using System.Web.Mvc;
using Assignment2.Models.Database_Models;
using System.Collections.Generic;
using System.Net;


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
            var ApproveOrCancelIntervention = new InterventionHelper();
            try
            {
                viewModel = ApproveOrCancelIntervention.ListOfProposedInterventionsForManager();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }
        public ActionResult EditInterventionForManager(int? id)
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
            var statuslist = intervention.GetPossibleStatusUpdateForIntervention(inter.Status);
            ViewBag.Status = new SelectList(statuslist.Keys);
            return View(inter);
        }


        [HttpPost]
        public ActionResult EditInterventionForManager(ListInterventionViewModel InterList)
        {
            if (ModelState.IsValid)
            {
                InterventionsDao interDao = new InterventionsDao();

                if (InterList.Status == Status.Approved.ToString())
                {
                    interDao.UpdateInterventionStatus_ToAppoved(InterList.InterventionId);


                }
                else if (InterList.Status == Status.Cancelled.ToString())
                {
                    interDao.UpdateInterventionStatus_ToCancelled(InterList.InterventionId);

                }


            }
            return RedirectToAction("EditInterventionForManager");


        }
    }
}