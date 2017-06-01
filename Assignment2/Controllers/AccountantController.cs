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

        //public ActionResult EditInterventionForManager(string userId)
        //{
        //    if (userId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var intervention = new InterventionHelper();
        //    var inter = intervention.GetIntervention(id);
        //    if (inter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var statuslist = intervention.GetPossibleStatusUpdateForIntervention(inter.Status);
        //    ViewBag.Status = new SelectList(statuslist.Keys);
        //    return View(inter);
        //}
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

    }
}