using System;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class TimeSheetsCIODaoTest
    {
        private TimeSheetsDao myDao;

        [TestInitialize]
        public void SetUp()
        {
            myDao = new TimeSheetsDao();
        }

        [TestMethod]
        public void Add_Delete_Success()
        {
            var timeSht = new TimeSheet("ABC", "EM099", new DateTime(2023, 12, 01, 0 , 0 , 0), 
                new DateTime(2023, 12, 01, 10 , 0 , 0), "ABC");
            
            //add
            myDao.Add(timeSht);
            var added = Search(timeSht);
            
            //update 
            var timeShtUpdate = new TimeSheet(timeSht.ID, timeSht.EmployeeID, timeSht.CheckInTime, timeSht.CheckOutTime,
                "SJKSA");
            myDao.Update(timeShtUpdate);
            var updated = Search(timeShtUpdate);
            
            //delete
            myDao.Delete(timeSht.ID);
            var deleted = Search(timeSht);
            
            //assert add
            AssertObject(timeSht, added);
            
            //assert update
            AssertObject(timeShtUpdate, updated);
            
            //assert delete
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void GetAll_Found()
        {
            var list = myDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void ToTalWorksDayByEmployeeID()
        {
            int total = myDao.ToTalWorksDayByEmployeeID("EM007");
            Assert.AreEqual(1, total);
        }
        
        public TimeSheet Search(TimeSheet timeSheet)
        {
            DBConnection dbConnection = new DBConnection();
            string sqlStr = $"Select * From TimeSheets Where ID = '{timeSheet.ID}'";
            return (TimeSheet)dbConnection.GetSingleObject(sqlStr, reader => new TimeSheet(reader));
        }
        
        public void AssertObject(TimeSheet expected, TimeSheet actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.EmployeeID, actual.EmployeeID);
            Assert.AreEqual(expected.CheckInTime, actual.CheckInTime);
            Assert.AreEqual(expected.CheckOutTime, actual.CheckOutTime);
            Assert.AreEqual(expected.EmployeeID, actual.EmployeeID);
            Assert.AreEqual(expected.TaskCheckInID, actual.TaskCheckInID);
        }
    }
}