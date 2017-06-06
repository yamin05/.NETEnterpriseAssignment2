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
        private readonly IManagerHelper managerHelper = new ManagerHelper();
        public ManagerController(IManagerHelper _managerHelper)
        {
            managerHelper = _managerHelper;
        } 

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Button)
        {
            return RedirectToAction(Button);
        }

        public ActionResult ListOfInterventions()
        {
            IList<ListInterventionViewModel> viewModel = new List<ListInterventionViewModel>();
            try
            {
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
            ListInterventionViewModel viewModel = new ListInterventionViewModel();
            try
            {
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
             
            }
            return View(viewModel);
            
        }
    }
}