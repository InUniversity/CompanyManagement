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
        private LeaveRequestsDao leaveRequestsDao;
        private LeaveRequest leaveRequest;

        [SetUp]
        public void SetUp()
        {
            leaveRequestsDao = new LeaveRequestsDao();
            leaveRequest = new LeaveRequest();
            leaveRequest.ID = "LEA12341";
            leaveRequest.Reason = "Vấn đề sức khỏe";
            leaveRequest.Notes = "Bệnh nặng";
            leaveRequest.Start = new DateTime(2023, 4, 15).Date;
            leaveRequest.End = new DateTime(2023, 4, 25).Date;
            leaveRequest.Created = new DateTime(2023, 4, 27).Date;
            leaveRequest.StatusID = "LS2";
            leaveRequest.RequesterID = "EM007";
            leaveRequest.ApproverID = "EM006";
        }

        [Test]
        public void Leave_Requests_Dao_Search_By_ID_Success()
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
            var actualSearch = leaveRequestsDao.SearchByID(expected.ID);
            
            AssertObject(expected, actualSearch);
        }

        [Test]
        public void Leave_Requests_Dao_Add_Update_Delete()
        {
            // add
            leaveRequestsDao.Add(leaveRequest); 
            var added = leaveRequestsDao.SearchByID(leaveRequest.ID);
            
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
            leaveRequestsDao.Update(updateObject);
            var updated = leaveRequestsDao.SearchByID(leaveRequest.ID);  
            
            // add
            leaveRequestsDao.Delete(leaveRequest.ID);
            var afterDelete = leaveRequestsDao.SearchByID(leaveRequest.ID);
            
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