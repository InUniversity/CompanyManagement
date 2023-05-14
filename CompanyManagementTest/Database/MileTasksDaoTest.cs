using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class MileTasksDaoTest
    {
        private MileTasksDao myDao;

        [SetUp]
        public void SetUp()
        {
            myDao = new MileTasksDao();
        }

        [Test]
        public void MileTasks_Dao_Add_Delete_Update_Test()
        {
            var addTarget = new MileTask("mileID", "taskID");
            myDao.Add(addTarget);
            var addResult = Search(addTarget);

            myDao.Delete(addTarget);
            var deleteResult = Search(addTarget);
            
            // test add
            AssertObj(addTarget, addResult);
            
            // test delete
            Assert.IsNull(deleteResult);
        }

        [Test]
        public void MileTasks_Dao_Search_By_MileID_Test()
        {
            var list = myDao.SearchByMileID("MST0001");
            Assert.AreEqual(2, list.Count);
        }

        private MileTask Search(MileTask mileTsk)
        {
            DBConnection dbConnection = new DBConnection();
            string sql = $"SELECT * FROM MileTasks WHERE MileID='{mileTsk.MileID}' AND TaskID='{mileTsk.TskID}'";
            return (MileTask)dbConnection.GetSingleObject(sql, reader => new MileTask(reader));
        }

        private void AssertObj(MileTask expected, MileTask actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.MileID, actual.MileID);
            Assert.AreEqual(expected.TskID, actual.TskID);
        } 
    }
}