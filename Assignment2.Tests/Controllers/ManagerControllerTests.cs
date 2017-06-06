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
    public class ManagerControllerTests
    {
        [TestMethod()]
        public void ListofInterventions_Test()
        {
            var mockManagerHelper = new Mock<IManagerHelper>();
            mockManagerHelper.Setup(i => i.GetListOfProposedInterventions()).Returns(GetInterventions());
            var MockManagerController = new ManagerController(mockManagerHelper.Object);
            var result = MockManagerController.ListOfInterventions() as ViewResult;
            Assert.AreEqual(2, result.ViewEngineCollection.Count);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<ListInterventionViewModel>));
        }
        
        private IList<ListInterventionViewModel> GetInterventions()
        {
            IList<ListInterventionViewModel> Inter = new List<ListInterventionViewModel>();
            Inter.Add(new ListInterventionViewModel()
            {

                ClientDistrict = Districts.SYDNEY,
                ClientName = "Sam",
                InterventionTypeName = "First Aid Kit",
                InterventionCost = 500,
                InterventionHours = 10,
                CreateDate = new DateTime(2017, 6, 5),
                Status = Status.PROPOSED,
                InterventionId = 1,
                ModifyDate = null,
                Condition = 100
            });
            Inter.Add(new ListInterventionViewModel()
            {
                ClientDistrict = Districts.SYDNEY,
                ClientName = "David",
                InterventionTypeName = "First Aid Kit",
                InterventionCost = 500,
                InterventionHours = 10,
                CreateDate = new DateTime(2017, 6, 4),
                Status = Status.PROPOSED,
                InterventionId = 2,
                ModifyDate = null,
                Condition = 100
            });
            return Inter;

            //    [TestMethod()]
            //    public void EditInterventionTest()
            //    {

            //    }

            //    [TestMethod()]
            //    public void EditInterventionTest1()
            //    {

            //    }
            //}
        }
    }
}