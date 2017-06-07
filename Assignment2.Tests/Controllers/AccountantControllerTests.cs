using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Helpers;
using Moq;
using Assignment2.Models;
using System.Web.Mvc;

namespace Assignment2.Controllers.Tests
{
    [TestClass()]
    public class AccountantControllerTests
    {
        [TestMethod()]
        public void ListOfUsersTest()
        {
            var mockUserHelper = new Mock<IUsersHelper>();
            mockUserHelper.Setup(i=>i.ListOfUsers()).Returns(GetUser());
            var MockAccountantController = new AccountantController(mockUserHelper.Object);
            var result = MockAccountantController.ListOfUsers() as ViewResult;
            Assert.AreEqual(2, result.ViewEngineCollection.Count);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<UserViewModel>));
            
        }

        private IList<UserViewModel> GetUser()
        {
            IList<UserViewModel> users = new List<UserViewModel>();
            users.Add(new UserViewModel()
            {

            });
            users.Add(new UserViewModel()
            {

            });
            return users;
        }


    }
}