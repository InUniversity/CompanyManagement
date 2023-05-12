using System;
using CompanyManagement;
using CompanyManagement.Database;
using CompanyManagement.Enums;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class TasksDaoTest
    {
        private TasksDao myDao;
        
        [SetUp]
        public void SetUp()
        {
            myDao = new TasksDao();
        }
        
        [Test]
        public void SearchByID_Found()
        {
            var expected = new TaskInProject("T000001", "Website Development - Design", 
                "Thiết kế giao diện website cho khách hàng ABC", 
                new DateTime(2023, 3, 1, 9, 0, 0), 
                new DateTime(2023, 3, 15, 17, 0, 0), 
                "50", "EM002", "EM007", "PRJ001", 
                ETaskStatus.Overdue, new Employee());
            var actualSearch = myDao.SearchByID(expected.ID);
            
            AssertObject(expected, actualSearch);
        }

        [Test]
        public void Add_Update_Delete_Success()
        {
            var task = new TaskInProject("T2352342", "Test Task", "Test Explanation 111", 
                new DateTime(2023, 4, 30, 0, 0, 0), 
                new DateTime(2023, 5, 30, 0, 0, 0), 
                "0", "EM001", "EM007", "PRJ001", ETaskStatus.InProcess, new Employee());  
            
            // add
            myDao.Add(task); 
            var added = myDao.SearchByID(task.ID);
            
            // update
            var updateObject = new TaskInProject(task.ID, task.Title + "Updated", task.Explanation, 
                task.Start, task.Deadline, task.Progress, task.OwnerID, "EM008", task.ProjectID, 
                task.Status, new Employee());
            myDao.Update(updateObject);
            var updated = myDao.SearchByID(task.ID);  
            
            // add
            myDao.Delete(task.ID);
            var afterDelete = myDao.SearchByID(task.ID);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(task, added);
            
            // assert updated
            AssertObject(updateObject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }

        [Test]
        public void SearchByProjectID_Found()
        {
            var list = myDao.SearchByProjectID("PRJ001");
            Assert.AreEqual(5, list.Count);
        }
        
        [Test]
        public void SearchByEmployeeID_Found()
        {
            var list = myDao.SearchByEmployeeID("PRJ001", "EM007");
            Assert.AreEqual(3, list.Count);
        }
        
        [Test]
        public void SearchCurrentTasksByEmployeeID_Found()
        {
            var list = myDao.SearchTasksCheckOut("EM007", new DateTime(2023, 3, 2, 0, 0, 0));
            Assert.AreEqual(1, list.Count);
        }
        
        private void AssertObject(TaskInProject expected, TaskInProject actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Explanation, actual.Explanation);
            Assert.AreEqual(expected.Start, actual.Start);
            Assert.AreEqual(expected.Deadline, actual.Deadline);
            Assert.AreEqual(expected.Progress, actual.Progress);
            Assert.AreEqual(expected.OwnerID, actual.OwnerID);
            Assert.AreEqual(expected.EmployeeID, actual.EmployeeID);
            Assert.AreEqual(expected.ProjectID, actual.ProjectID);
            Assert.AreEqual(expected.Status, actual.Status);
        }
    }
}