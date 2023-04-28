using CompanyManagement.Database;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class ProjectStatusesDaoTest
    {
        private ProjectStatusesDao projectStatusesDao;

        [SetUp]
        public void SetUp()
        {
            projectStatusesDao = new ProjectStatusesDao();
        }

        [Test]
        public void Project_Statuses_Dao_Get_All()
        {
            var list = projectStatusesDao.GetAll();
            
            Assert.AreEqual(5, list.Count);
        }
    }
}