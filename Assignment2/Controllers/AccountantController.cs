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
        
        // GET: Accountant
        public ActionResult Index()
        {
            return View();
        }

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


        public ActionResult ListOfUsers()
        {
            IList<UserViewModel> viewModel = new List<UserViewModel>();
            var usersList = new UsersHelper();
            try
            {
                viewModel = usersList.ListOfUsers();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        public ActionResult ChangeDistrictForUsers(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Users = new UsersHelper();
            UserViewModel userDetail = Users.GetUserData(userId);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            var districtList = Users.GetDistrictList();
            ViewBag.District = new SelectList(districtList);
            return View(userDetail);
        }

        [HttpPost]
        public ActionResult ChangeDistrictForUsers(UserViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                var Users = new UsersHelper();
                Users.UpdateDistrictForUser(UserViewModel.UserId, UserViewModel.District);
            
            }
            return RedirectToAction("ListOfUsers");
        }

        //Report

        public ActionResult TotalCostsByEngineerReport()
        {
            IList<TotalCostsByEngineerModel> viewModel = new List<TotalCostsByEngineerModel>();
            var Report = new ReportHelper();
            try
            {
                viewModel = Report.TotalCostsByEngineerView();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }


        public ActionResult AverageCostsByEngineerReport()
        {
            IList<AverageCostsByEngineerModel> viewModel = new List<AverageCostsByEngineerModel>();
            var Report = new ReportHelper();
            try
            {
                viewModel = Report.AverageCostsByEngineerView();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        public ActionResult CostsByDistrictReport()
        {
            IList<CostsByDistrictModel> viewModel = new List<CostsByDistrictModel>();
            var Report = new ReportHelper();
            try
            {
                viewModel = Report.CostsByDistrictView();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex);
            }
            return View(viewModel);
        }

        //public ActionResult MonthlyCostsForDistrictReport()
        //{
        //    var Users = new UsersHelper();
        //    var districtList = Users.GetDistrictList();
        //    ViewBag.District = new SelectList(districtList);
        //    IList<MonthlyCostsForDistrictModel> viewModel = new List<MonthlyCostsForDistrictModel>();
        //    var Report = new ReportHelper();
        //    return View(viewModel);
        //}


        //[HttpPost]
        //public ActionResult MonthlyCostsForDistrictReport(FormCollection fc, string district)
        //{
        //    var Users = new UsersHelper();
        //    var districtList = Users.GetDistrictList();
        //    ViewBag.District = new SelectList(districtList);
        //    IList<MonthlyCostsForDistrictModel> viewModel = new List<MonthlyCostsForDistrictModel>();
        //    var Report = new ReportHelper();
        //    try
        //    {
        //        viewModel = Report.MonthlyCostsForDistrictView(district);

        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex);
        //    }
        //    return View(viewModel);
        //}
    }
}