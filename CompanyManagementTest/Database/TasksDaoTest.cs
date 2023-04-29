using System;
using CompanyManagement;
using CompanyManagement.Database;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class TasksDaoTest
    {
        private TasksDao tasksDao;
        private TaskInProject task;
        
        [SetUp]
        public void SetUp()
        {
            tasksDao = new TasksDao();
            task = new TaskInProject("T2352342", "Test Task", "Test Explanation 111", 
                new DateTime(2023, 4, 30, 0, 0, 0), 
                new DateTime(2023, 5, 30, 0, 0, 0), 
                "0", "EM001", "EM007", "PRJ001", "TS1", new Employee()); 
        }
        
        [Test]
        public void Tasks_Dao_Search_By_ID_Success()
        {
            var expected = new TaskInProject("T000001", "Website Development - Design", 
                "Thiết kế giao diện website cho khách hàng ABC", 
                new DateTime(2023, 3, 1, 9, 0, 0), 
                new DateTime(2023, 4, 15, 0, 0, 0), 
                "50", "EM002", "EM007", "PRJ001", "TS3", new Employee());
            var actualSearch = tasksDao.SearchByID(expected.ID);
            
            AssertObject(expected, actualSearch);
        }

        [Test]
        public void Tasks_Dao_Add_Update_Delete()
        {
            // add
            tasksDao.Add(task); 
            var added = tasksDao.SearchByID(task.ID);
            
            // update
            var updateObject = new TaskInProject(task.ID, task.Title + "Updated", task.Explanation, task.StartDate, 
                task.Deadline, task.Progress, task.OwnerID, "EM008", task.ProjectID, task.StatusID, new Employee());
            tasksDao.Update(updateObject);
            var updated = tasksDao.SearchByID(task.ID);  
            
            // add
            tasksDao.Delete(task.ID);
            var afterDelete = tasksDao.SearchByID(task.ID);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(task, added);
            
            // assert updated
            AssertObject(updateObject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }
        
        private void AssertObject(TaskInProject expected, TaskInProject actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Explanation, actual.Explanation);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.Deadline, actual.Deadline);
            Assert.AreEqual(expected.Progress, actual.Progress);
            Assert.AreEqual(expected.OwnerID, actual.OwnerID);
            Assert.AreEqual(expected.EmployeeID, actual.EmployeeID);
            Assert.AreEqual(expected.ProjectID, actual.ProjectID);
            Assert.AreEqual(expected.StatusID, actual.StatusID);
        }
    }
}