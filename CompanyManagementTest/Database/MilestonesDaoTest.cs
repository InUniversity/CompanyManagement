using System;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class MilestonesDaoTest
    {
        private MilestonesDao milestonesDao;
        private Milestone mile;
        
        [SetUp]
        public void SetUp()
        {
            milestonesDao = new MilestonesDao();
            mile = new Milestone("MIL12342", "Test Project", "Test Details",
                new DateTime(2023, 1, 1, 1, 0, 0),
                new DateTime(2023, 1, 1, 1, 0, 0),
                Utils.emptyDate, "EM006", "PROJ001");
        }
        
        [Test]
        public void Projects_Dao_Search_By_ID_Success()
        {
            var actual = Search("MST0001");
            
            Assert.IsNotNull(actual);
            Assert.AreEqual("MST0001", actual.ID);
            Assert.AreEqual("Thiết kế giải pháp", actual.Title);
            Assert.AreEqual("Giao đoạn quan trọng", actual.Explanation);
            Assert.AreEqual(new DateTime(2023, 1, 1, 8, 0, 0), actual.Start);
            Assert.AreEqual(new DateTime(2023, 4, 1, 17, 0, 0), actual.End);
            Assert.AreEqual(new DateTime(2000, 1, 1, 0, 0, 0), actual.Completed);
            Assert.AreEqual("EM006", actual.OwnerID);
            Assert.AreEqual("PRJ001", actual.ProjID);
        }

        [Test]
        public void Projects_Dao_Add_Update_Delete()
        {
            var initial = Search(mile.ID);
            
            // add
            milestonesDao.Add(mile); 
            var added = Search(mile.ID);
            
            // update
            var updateProject = new Milestone(mile.ID, mile.Title + " Xin chào", mile.Explanation + " Thêm", 
                mile.Start, mile.End, mile.Completed, mile.OwnerID, mile.ProjID);
            milestonesDao.Update(updateProject);
            var updated = Search(mile.ID);  
            
            // delete
            milestonesDao.Delete(mile.ID);
            var afterDelete = Search(mile.ID);
            
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