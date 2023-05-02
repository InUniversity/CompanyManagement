using System;
using CompanyManagement;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class MilestonesDaoTest
    {
        private MileTasksDao milestonesDao;
        private Milestone mile;
        
        [SetUp]
        public void SetUp()
        {
            milestonesDao = new MileTasksDao();
            mile = new Milestone("MIL12342", "Test Project", "Test Details",
                new DateTime(2023, 1, 1, 1, 0, 0),
                new DateTime(2023, 1, 1, 1, 0, 0),
                new DateTime(2020, 1, 1, 0, 0, 0),
                "EM006", "PROJ001");
        }
        
        [Test]
        public void Projects_Dao_Search_By_ID_Success()
        {
            var actualSearch = Search(mile.ID);
            
            Assert.AreEqual("PRJ001", actualSearch.ID);
            Assert.AreEqual("Website Development", actualSearch.Name);
            Assert.AreEqual("", actualSearch.Details);
            Assert.AreEqual(new DateTime(2023, 1, 1, 8, 0, 0), actualSearch.CreatedDate);
            Assert.AreEqual(new DateTime(2023, 3, 1, 8, 0, 0), actualSearch.StartDate);
            Assert.AreEqual(new DateTime(2023, 6, 30, 17, 0, 0), actualSearch.EndDate);
            Assert.AreEqual(new DateTime(2000, 1, 1, 0, 0, 0), actualSearch.CompletedDate);
            Assert.AreEqual("50", actualSearch.Progress);
            Assert.AreEqual("PST1", actualSearch.StatusID);
            Assert.AreEqual("EM001", actualSearch.OwnerID);
            Assert.AreEqual(100000000.0000, actualSearch.BonusSalary);
        }

        [Test]
        public void Projects_Dao_Add_Update_Delete()
        {
            var initial = milestonesDao.SearchByID(mile.ID);
            
            // add
            milestonesDao.Add(mile); 
            var added = milestonesDao.SearchByID(mile.ID);
            
            // update
            var updateProject = new Project(mile.ID, mile.Name + "Updated", mile.Details + "more details", 
                mile.CreatedDate, mile.StartDate, mile.EndDate, mile.CompletedDate, mile.Progress, 
                mile.StatusID, mile.OwnerID, (decimal)123123123.5000, mile.Departments);
            milestonesDao.Update(updateProject);
            var updated = milestonesDao.SearchByID(mile.ID);  
            
            // delete
            milestonesDao.Delete(mile.ID);
            var afterDelete = milestonesDao.SearchByID(mile.ID);
            
            // assert initial
            Assert.IsNull(initial);
            
            // assert added
            Assert.IsNotNull(added);
            AssertProject(mile, added);
            
            // assert updated
            AssertProject(updateProject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }

        private Milestone Search(string id)
        {
            DBConnection dbConnection = new DBConnection();
            string sql = $"SELECT * FROM Milestones WHERE ID='{id}'";
            return (Milestone)dbConnection.GetSingleObject(sql, reader => new Milestone(reader)); 
        }
        
        private void AssertProject(Milestone expected, Milestone actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Explanation, actual.Explanation);
            Assert.AreEqual(expected.Start, actual.Start);
            Assert.AreEqual(expected.End, actual.End);
            Assert.AreEqual(expected.Completed, actual.Completed);
            Assert.AreEqual(expected.OwnerID, actual.OwnerID);
            Assert.AreEqual(expected.ProjID, actual.ProjID);
        } 
    }
}