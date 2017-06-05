using Assignment2.Helpers;
using Assignment2.Models;
using System;
using System.Web.Mvc;
using Assignment2.Models.Database_Models;
using System.Collections.Generic;
using System.Net;
using WebApplication2.Exceptions;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Roles.MANAGER)]
    public class ManagerController : Controller
    {
        private ManagerHelper managerHelper = new ManagerHelper();
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
                return RedirectToAction("ListOfInterventions");
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

        public ActionResult ListOfInterventions()
        {
            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            //var ApproveOrCancelIntervention = new InterventionHelper();
            try
            {
                //viewModel = ApproveOrCancelIntervention.ListOfProposedInterventionsForManager();
                viewModel = managerHelper.GetListOfProposedInterventions();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }

            return View(viewModel);
        }

        public ActionResult EditIntervention(int interventionId)
        {
            //if (interventionId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var intervention = new InterventionHelper();
            //var inter = intervention.GetIntervention(interventionId);
            ListInterventionViewModel viewModel = new ListInterventionViewModel();
            try
            {
                //viewModel = ApproveOrCancelIntervention.ListOfProposedInterventionsForManager();
                viewModel = managerHelper.GetIntervention(interventionId);
                var statuslist = managerHelper.GetPossibleStatusUpdateForIntervention(viewModel.Status);
                ViewBag.Status = new SelectList(statuslist);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult EditIntervention(ListInterventionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var intervention = managerHelper.UpdateInterventionStatus(viewModel.InterventionId, viewModel.Status);
                    var statuslist = managerHelper.GetPossibleStatusUpdateForIntervention(viewModel.Status);
                    ViewBag.Status = new SelectList(statuslist);
                    ModelState.AddModelError("success", "Intervention Updated Successfully");
                }
                catch (Exception ex)
                {
                    ViewBag.Status = new SelectList(new List<string>() { viewModel.Status });
                    ModelState.AddModelError("alert", ex.Message);
                }
                //InterventionsDao interDao = new InterventionsDao();
                //if (InterList.Status == Status.Approved.ToString())
                //{
                //    interDao.UpdateInterventionStatus_ToAppoved(InterList.InterventionId);


                //}
                //else if (InterList.Status == Status.Cancelled.ToString())
                //{
                //    interDao.UpdateInterventionStatus_ToCancelled(InterList.InterventionId);

                //}
            }
            return View(viewModel);
            //return RedirectToAction("EditIntervention");
        }
    }
}