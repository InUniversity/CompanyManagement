using System;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class MilestonesDaoTest
    {
        private MilestonesDao milestonesDao;
        
        [TestInitialize]
        public void SetUp()
        {
            milestonesDao = new MilestonesDao();
        }
        
        [TestMethod]
        public void Milestones_Dao_Search_By_ID_Success()
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

        [TestMethod]
        public void Milestones_Dao_Add_Update_Delete()
        {
            var mile = new Milestone("MIL12342", "Test Project", "Test Details",
                new DateTime(2023, 1, 1, 1, 0, 0),
                new DateTime(2023, 1, 1, 1, 0, 0),
                Utils.emptyDate, "EM006", "PROJ001");
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
            AssertObj(mile, added);
            
            // assert updated
            AssertObj(updateProject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }

        [TestMethod]
        public void Milestones_Dao_Search_By_ProjectID()
        {
            var list = milestonesDao.SearchByProjectID("PRJ001");
            Assert.AreEqual(1, list.Count);
        }

        private Milestone Search(string id)
        {
            DbConnection dbConnection = new DbConnection();
            string sql = $"SELECT * FROM Milestones WHERE ID='{id}'";
            return (Milestone)dbConnection.GetSingleObject(sql, reader => new Milestone(reader)); 
        }
        
        private void AssertObj(Milestone expected, Milestone actual)
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