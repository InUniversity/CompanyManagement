using CompanyManagement.Database;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class TaskStatusesDaoTest
    {
        private TaskStatusesDao taskStatusesDao;

        [SetUp]
        public void SetUp()
        {
            taskStatusesDao = new TaskStatusesDao();
        }

        [Test]
        public void Task_Statuses_Dao_Get_All()
        {
            var list = taskStatusesDao.GetAll();
            
            Assert.AreEqual(4, list.Count);
        }
    }
}