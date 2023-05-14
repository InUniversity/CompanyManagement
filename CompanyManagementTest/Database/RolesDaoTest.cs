using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class RolesDaoTest
    {
        private RolesDao myDao;

        [SetUp]
        public void SetUp()
        {
            myDao = new RolesDao();
        }

        [Test]
        public void GetAll_Found()
        {
            var list = myDao.GetAll();
            Assert.AreEqual(26, list.Count);
        }

        [Test]
        public void SearchByID_Found()
        {
            //Do không có hàm khởi tạo
            var role = myDao.SearchByID("ER01");
            Assert.IsNotNull(role);
        }
    }
}