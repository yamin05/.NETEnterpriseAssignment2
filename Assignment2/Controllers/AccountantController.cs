using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2.Helpers;
using Assignment2.Models;
using System.Net;
using System.Data.Entity;
using Assignment2.Data_Access_Layer;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Roles.ACCOUNTANT)]
    public class AccountantController : Controller
    {
        private readonly IUsersHelper userhelper = new UsersHelper();
        private readonly IReportHelper reportHelper = new ReportHelper();
        public AccountantController(IUsersHelper _userHelper)
        {
            userhelper = _userHelper;
        }
        public AccountantController()
        {

        }

        // GET: Accountant
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// This method is used to navigation for accountant user
        /// </summary>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult Index(string Button)
        {
            if (Button.Equals("List of Users"))
            {
                return RedirectToAction("ListOfUsers");
            }
            else if (Button.Equals("Total Costs By Engineer Report"))
            {
                return RedirectToAction("TotalCostsByEngineerReport");
            }
            else if (Button.Equals("Average Costs By Engineer Report"))
            {
                return RedirectToAction("AverageCostsByEngineerReport");
            }
            else if (Button.Equals("Costs By District Report"))
            {
                return RedirectToAction("CostsByDistrictReport");
            }
            else if (Button.Equals("Monthly Costs For District Report"))
            {
                return RedirectToAction("MonthlyCostsForDistrictReport");
            }
            return View();
        }

        /// <summary>
        /// This method is used to view all users list 
        /// </summary>
        /// <returns>view</returns>
        public ActionResult ListOfUsers()
        {
            IList<UserViewModel> viewModel = new List<UserViewModel>();
            try
            {
                viewModel = userhelper.ListOfUsers();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }


        /// <summary>
        /// This method is used to view user detail for change district
        /// </summary>
        /// <returns>view</returns>
        public ActionResult ChangeDistrictForUsers(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userDetail = userhelper.GetUserData(userId);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            var districtList = userhelper.GetDistrictList();
            ViewBag.District = new SelectList(districtList);
            return View(userDetail);
        }

        /// <summary>
        /// This method is used to update user detail for change district
        /// </summary>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult ChangeDistrictForUsers(UserViewModel UserViewModel)
        {
            var districtList = userhelper.GetDistrictList();

            try
            {
               
                if (ModelState.IsValid)
                {
                    ViewBag.District = new SelectList(districtList);
                    userhelper.UpdateDistrictForUser(UserViewModel.UserId, UserViewModel.District);
                    ModelState.AddModelError("success", "The District has been changed!!");
                }
            }

            catch (Exception ex)
            {
                ViewBag.District = new SelectList(districtList);
                ModelState.AddModelError("", ex.Message);
            }
            return View(UserViewModel);
        }


        /// <summary>
        /// This method is used to view total costs by engineer report
        /// </summary>
        /// <returns>view</returns>
        public ActionResult TotalCostsByEngineerReport()
        {
            IList<TotalCostsByEngineerModel> viewModel = new List<TotalCostsByEngineerModel>();
            try
            {
                viewModel = reportHelper.TotalCostsByEngineerView();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        /// <summary>
        /// This method is used to view average costs by engineer report
        /// </summary>
        /// <returns>view</returns>
        public ActionResult AverageCostsByEngineerReport()
        {
            IList<AverageCostsByEngineerModel> viewModel = new List<AverageCostsByEngineerModel>();
            try
            {
                viewModel = reportHelper.AverageCostsByEngineerView();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        /// <summary>
        /// This method is used to view costs by district report
        /// </summary>
        /// <returns>view</returns>
        public ActionResult CostsByDistrictReport()
        {
            IList<CostsByDistrictModel> viewModel = new List<CostsByDistrictModel>();
            try
            {
                viewModel = reportHelper.CostsByDistrictView();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        /// <summary>
        /// This method is used to view monthly costs for district report
        /// </summary>
        /// <returns>view</returns>
        public ActionResult MonthlyCostsForDistrictReport()
        {
            var Users = new UsersHelper();
            var districtList = Users.GetDistrictList();
            ViewBag.District = new SelectList(districtList);
            IList<MonthlyCostsForDistrictModel> viewModel = new List<MonthlyCostsForDistrictModel>();
            return View(viewModel);
        }

        /// <summary>
        /// This method is used to view monthly costs for district report based on search
        /// </summary>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult MonthlyCostsForDistrictReport(FormCollection fc, string district)
        {
            var Users = new UsersHelper();
            var districtList = Users.GetDistrictList();
            ViewBag.District = new SelectList(districtList);
            IList<MonthlyCostsForDistrictModel> viewModel = new List<MonthlyCostsForDistrictModel>();
            try
            {
                viewModel = reportHelper.MonthlyCostsForDistrictView(district);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }
    }
}