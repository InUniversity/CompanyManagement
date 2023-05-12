using System;
using System.Collections.ObjectModel;
using CompanyManagement;
using CompanyManagement.Database;
using CompanyManagement.Enums;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class ProjectsDaoTest
    {
        private ProjectsDao projectsDao;
        private Project project;
        
        [SetUp]
        public void SetUp()
        {
            projectsDao = new ProjectsDao();
            project = new Project("PRJ1111", "Test Project", "Test Details", 
                new DateTime(2023, 1, 1, 1, 0, 0), 
                new DateTime(2023, 1, 1, 1, 0, 0), 
                new DateTime(2023, 12, 1, 1, 0, 0), 
                new DateTime(2000, 1, 1, 1, 0, 0), 
                "0", EProjStatus.Planning, "EM001", 
                (decimal)100000.00, new ObservableCollection<Department>()); 
        }
        
        [Test]
        public void Projects_Dao_Search_By_ID_Success()
        {
            var actualSearch = projectsDao.SearchByID("PRJ001");
            
            Assert.AreEqual("PRJ001", actualSearch.ID);
            Assert.AreEqual("Website Development", actualSearch.Name);
            Assert.AreEqual("", actualSearch.Details);
            Assert.AreEqual(new DateTime(2023, 1, 1, 8, 0, 0), actualSearch.CreatedDate);
            Assert.AreEqual(new DateTime(2023, 3, 1, 8, 0, 0), actualSearch.StartDate);
            Assert.AreEqual(new DateTime(2023, 6, 30, 17, 0, 0), actualSearch.EndDate);
            Assert.AreEqual(new DateTime(2000, 1, 1, 0, 0, 0), actualSearch.CompletedDate);
            Assert.AreEqual("50", actualSearch.Progress);
            Assert.AreEqual(EProjStatus.InProcess, actualSearch.Status);
            Assert.AreEqual("EM001", actualSearch.OwnerID);
            Assert.AreEqual(100000000.0000, actualSearch.BonusSalary);
        }

        [Test]
        public void Projects_Dao_Add_Update_Delete()
        {
            var initial = projectsDao.SearchByID(project.ID);
            
            // add
            projectsDao.Add(project); 
            var added = projectsDao.SearchByID(project.ID);
            
            // update
            var updateProject = new Project(project.ID, project.Name + "Updated", project.Details + "more details", 
                project.CreatedDate, project.StartDate, project.EndDate, project.CompletedDate, project.Progress, 
                project.Status, project.OwnerID, (decimal)123123123.5000, project.Departments);
            projectsDao.Update(updateProject);
            var updated = projectsDao.SearchByID(project.ID);  
            
            // delete
            projectsDao.Delete(project.ID);
            var afterDelete = projectsDao.SearchByID(project.ID);
            
            // assert initial
            Assert.IsNull(initial);
            
            // assert added
            Assert.IsNotNull(added);
            AssertProject(project, added);
            
            // assert updated
            AssertProject(updateProject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }
        
        private void AssertProject(Project expected, Project actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Details, actual.Details);
            Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            Assert.AreEqual(expected.CompletedDate, actual.CompletedDate);
            Assert.AreEqual(expected.Progress, actual.Progress);
            Assert.AreEqual(expected.Status, actual.Status);
            Assert.AreEqual(expected.OwnerID, actual.OwnerID);
            Assert.AreEqual(expected.BonusSalary, actual.BonusSalary); 
        }
    }
}