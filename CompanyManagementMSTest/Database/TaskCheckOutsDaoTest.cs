using System;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class TaskCheckOutsDaoTest
    {
        private TaskCheckOutsDao myDao;
        
        [TestInitialize]
        public void SetUp()
        {
            myDao = new TaskCheckOutsDao();
        }

        [TestMethod]
        public void Add_Update_Delete_Success()
        {
            var tskOut = new TaskCheckOut("TST1412", "T235323", 
                new DateTime(2023, 2, 2, 12, 0, 0), "20", new TaskInProject());
            
            // add
            myDao.Add(tskOut); 
            var added = Search(tskOut.TimeShtID, tskOut.TaskID);
            
            Delete(tskOut.TimeShtID, tskOut.TaskID);
            var afterDelete = Search(tskOut.TimeShtID, tskOut.TaskID);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(tskOut, added);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }

        [TestMethod]
        public void SearchByProjectID_Found()
        {
            var list = myDao.SearchByProjectID("PRJ001");
            Assert.AreEqual(2, list.Count);
        }

        private TaskCheckOut Search(string timeShtID, string tskID)
        {
            DbConnection dbConnection = new DbConnection();
            string sql = $"SELECT * FROM TaskCheckOuts WHERE TimeSheetID='{timeShtID}' AND TaskID='{tskID}'";
            return (TaskCheckOut)dbConnection.GetSingleObject(sql, reader => new TaskCheckOut(reader));
        }
        
        private void Delete(string timeShtID, string tskID)
        {
            DbConnection dbConnection = new DbConnection();
            string sql = $"DELETE FROM TaskCheckOuts WHERE TimeSheetID='{timeShtID}' AND TaskID='{tskID}'";
            dbConnection.ExecuteNonQuery(sql);
        }
        
        private void AssertObject(TaskCheckOut expected, TaskCheckOut actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.TimeShtID, actual.TimeShtID);
            Assert.AreEqual(expected.TaskID, actual.TaskID);
            Assert.AreEqual(expected.Update, actual.Update);
            Assert.AreEqual(expected.Progress, actual.Progress);
        }
    }
}