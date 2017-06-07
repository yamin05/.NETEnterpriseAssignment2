using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using System.Collections.ObjectModel;

namespace Assignment2.Helpers.Tests
{
    [TestClass()]
    public class SiteEngineerHelperTests
    {
        Mock<IUserDao> mockUserDao;
        Mock<IInterventionsDao> mockInterDao;
        SiteEngineerHelper InterventionHelper_UnderTest;

        [TestMethod()]
        public void GetSiteEngineerDataTest()
        {
            User user = new User();
            user.UserId = "1";
            mockUserDao = new Mock<IUserDao>();
            mockInterDao = new Mock<IInterventionsDao>();
            mockUserDao.Setup(p => p.GetUser(user.UserId)).Returns(user);
            InterventionHelper_UnderTest = new SiteEngineerHelper(mockUserDao.Object, mockInterDao.Object);
            InterventionHelper_UnderTest.GetSiteEngineerData(user.UserId);
            mockUserDao.VerifyAll();
        }

        [TestMethod()]
        public void GetInterventionTypeDataTest()
        {
            InterventionType intertype = new InterventionType();
            intertype.InterventionTypeId = 1;
            intertype.InterventionTypeName = "First Aid Kit";
            mockUserDao = new Mock<IUserDao>();
            mockInterDao = new Mock<IInterventionsDao>();
            mockInterDao.Setup(p => p.GetInterventionType(intertype.InterventionTypeId)).Returns(intertype);
            InterventionHelper_UnderTest = new SiteEngineerHelper(mockUserDao.Object, mockInterDao.Object);
            var inter = InterventionHelper_UnderTest.GetInterventionTypeData(intertype.InterventionTypeId);
            Assert.AreEqual(intertype.InterventionTypeName, inter.InterventionTypeName);
        }

        [TestMethod()]
        public void ValidateInterventionStatusTest_Approved()
        {   var requiredcost = 300;
            var requiredhours = 20;
            User user = new User();
            user.UserId = "1"; 
            user.MaximumHours = 25;
            user.MaximumCost = 500;
            var mockinterventionHelper = new SiteEngineerHelper();
            var status = mockinterventionHelper.ValidateInterventionStatus(requiredhours, requiredcost, user);
            Assert.AreEqual(Status.APPROVED, status);
        }

        [TestMethod()]
        public void ValidateInterventionStatusTest_Proposed()
        {
            var requiredcost = 600;
            var requiredhours = 26;
            User user = new User();
            user.UserId = "1";
            user.MaximumHours = 25;
            user.MaximumCost = 500;
            var mockinterventionHelper = new SiteEngineerHelper();
            var status = mockinterventionHelper.ValidateInterventionStatus(requiredhours, requiredcost, user);
            Assert.AreEqual(Status.PROPOSED, status);
        }

        [TestMethod]
        public void ValidateProposedInterventions_Test_Pass()
        {
            User MockUser = new User();
            List<Intervention> interventionListMock = new List<Intervention>();
            Intervention inter = new Intervention();
            Client client1 = new Client();
            client1.ClientId = 1;
            client1.ClientDistrict = Districts.SYDNEY;
            MockUser.District = Districts.SYDNEY;
            MockUser.MaximumCost = 200;
            MockUser.MaximumHours = 10;
            inter.InterventionCost = 180;
            inter.InterventionHours = 10;
            inter.Client = client1;
            interventionListMock.Add(inter);
            var list2 = interventionListMock;
            var list = new ManagerHelper();
            var list1 = list.ValidateProposedInterventions(MockUser, interventionListMock);
            Collection<Intervention> collection = new Collection<Intervention>(list1);
            CollectionAssert.AreEqual(collection, list2);
        }

        [TestMethod]
        public void ValidateProposedInterventions_Test_Fail()
        {
            User MockUser = new User();
            List<Intervention> interventionListMock = new List<Intervention>();
            Intervention inter = new Intervention();
            Client client1 = new Client();
            client1.ClientId = 1;
            client1.ClientDistrict = Districts.SYDNEY;
            MockUser.District = Districts.SYDNEY;
            MockUser.MaximumCost = 200;
            MockUser.MaximumHours = 10;
            inter.InterventionCost = 180;
            inter.InterventionHours = 14;
            inter.Client = client1;
            interventionListMock.Add(inter);
            var list2 = interventionListMock;
            var list = new ManagerHelper();
            var list1 = list.ValidateProposedInterventions(MockUser, interventionListMock);
            Collection<Intervention> collection = new Collection<Intervention>(list1);
            CollectionAssert.AreNotEqual(collection, list2);
        }

        [TestMethod]
        public void GetPossibleStatusUpdateForInterventionForSiteEngineer_Test()
        {
            var Proposed_Status = Status.PROPOSED;
            var Approved_Status = Status.APPROVED;
            var Completed_Status = Status.COMPLETED;
            var mockinterventionHelper = new SiteEngineerHelper();
            var Status_IsProposed = mockinterventionHelper.GetPossibleStatusUpdateForInterventionForSiteEngineer(Proposed_Status);
            var Status_IsApproved = mockinterventionHelper.GetPossibleStatusUpdateForInterventionForSiteEngineer(Approved_Status);
            var Status_IsCompleted = mockinterventionHelper.GetPossibleStatusUpdateForInterventionForSiteEngineer(Completed_Status);
            Dictionary<string, string> list_forProposed = new Dictionary<string, string>();
            list_forProposed.Add(Status.PROPOSED, Status.PROPOSED);
            list_forProposed.Add(Status.CANCELLED, Status.CANCELLED);
            Dictionary<string, string> list_forAppoved = new Dictionary<string, string>();
            list_forAppoved.Add(Status.APPROVED, Status.APPROVED);
            list_forAppoved.Add(Status.COMPLETED, Status.COMPLETED);
            list_forAppoved.Add(Status.CANCELLED, Status.CANCELLED);
            Dictionary<string, string> list_forCompletd= new Dictionary<string, string>();
            list_forCompletd.Add(Status.COMPLETED, Status.COMPLETED);
            CollectionAssert.AreEqual(list_forProposed, Status_IsProposed);
            CollectionAssert.AreEqual(list_forAppoved, Status_IsApproved);
            CollectionAssert.AreEqual(list_forCompletd, Status_IsCompleted);
        }
    }
}