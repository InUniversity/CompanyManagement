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
        public void Product_Repository_Add_Delete_Update_Test()
        {
            var addTarget = new MileTask("MIL123132", "T1231244");
            myDao.Add(addTarget);
            var addResult = Search(addTarget);

            myDao.Delete(addTarget);
            var deleteResult = Search(addTarget);
            
            // test add
            AssertObj(addTarget, addResult);
            
            // test delete
            Assert.Null(deleteResult);
        }

        private MileTask Search(MileTask mileTsk)
        {
            DBConnection dbConnection = new DBConnection();
            string sql = $"SELECT * FROM MileTasks WHERE ID='{mileTsk.ID}' AND TaskID='{mileTsk.TskID}'";
            return (MileTask)dbConnection.GetSingleObject(sql, reader => new MileTask(reader));
        }

        private void AssertObj(MileTask expected, MileTask actual)
        {
            Assert.NotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.TskID, actual.TskID);
        } 
    }
}