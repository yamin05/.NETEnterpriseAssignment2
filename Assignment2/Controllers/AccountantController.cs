using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2.Helpers;
using Assignment2.Models;

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
    }
}