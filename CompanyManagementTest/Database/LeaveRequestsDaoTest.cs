using System;
using CompanyManagement.Database;
using CompanyManagement.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class LeaveRequestsDaoTest
    {
        private LeaveRequestsDao myDao;
        private LeaveRequest leaveRequest;

        [SetUp]
        public void SetUp()
        {
            myDao = new LeaveRequestsDao();
            leaveRequest = new LeaveRequest
            {
                ID = "LEA12341",
                Reason = "Vấn đề sức khỏe",
                Notes = "Bệnh nặng",
                Start = new DateTime(2023, 4, 15).Date,
                End = new DateTime(2023, 4, 25).Date,
                Created = new DateTime(2023, 4, 27).Date,
                StatusID = "LS2",
                RequesterID = "EM007",
                ApproverID = "EM006"
            };
        }

        [Test]
        public void Search_By_ID_Success()
        {
            var expected = new LeaveRequest();
            expected.ID = "LEA0001";
            expected.Reason = "Nghỉ do bị ốm";
            expected.Notes = "ghi chú 1";
            expected.Created = new DateTime(2023, 4, 1).Date;
            expected.Start = new DateTime(2023, 4, 8).Date;
            expected.End = new DateTime(2023, 4, 9).Date;
            expected.StatusID = "LS1";
            expected.RequesterID = "EM007";
            expected.ApproverID = "EM006";
            var actualSearch = myDao.SearchByID(expected.ID);
            
            AssertObject(expected, actualSearch);
        }
        
        [Test]
        public void GetMyRequests_Found()
        {
            var list = myDao.GetMyRequests("EM007");
            Assert.AreEqual(1, list.Count);
        }
        
        [Test]
        public void SearchByApproverID_Found()
        {
            var list = myDao.SearchByApproverID("EM006");
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void Add_Update_Delete()
        {
            // add
            myDao.Add(leaveRequest); 
            var added = myDao.SearchByID(leaveRequest.ID);
            
            // update
            var updateObject = new LeaveRequest();
            updateObject.ID = leaveRequest.ID;
            updateObject.Reason = leaveRequest.Reason + " Test 01";
            updateObject.Notes = leaveRequest.Notes + " Hello";
            updateObject.Start = leaveRequest.Start;
            updateObject.End = leaveRequest.End;
            updateObject.Created = leaveRequest.Created;
            updateObject.StatusID = "LS3";
            updateObject.RequesterID = leaveRequest.RequesterID;
            updateObject.ApproverID = leaveRequest.ApproverID;
            myDao.Update(updateObject);
            var updated = myDao.SearchByID(leaveRequest.ID);  
            
            // add
            myDao.Delete(leaveRequest.ID);
            var afterDelete = myDao.SearchByID(leaveRequest.ID);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(leaveRequest, added);
            
            // assert updated
            AssertObject(updateObject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }
        
        
        
        private void AssertObject(LeaveRequest expected, LeaveRequest actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Reason, actual.Reason);
            Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.Start, actual.Start);
            Assert.AreEqual(expected.End, actual.End);
            Assert.AreEqual(expected.Created, actual.Created);
            Assert.AreEqual(expected.RequesterID, actual.RequesterID);
            Assert.AreEqual(expected.ApproverID, actual.ApproverID);
        }
    }
}