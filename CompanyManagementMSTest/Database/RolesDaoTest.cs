using CompanyManagement.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class RolesDaoTest
    {
        private RolesDao myDao;

        [TestInitialize]
        public void SetUp()
        {
            myDao = new RolesDao();
        }

        [TestMethod]
        public void GetAll_Found()
        {
            var list = myDao.GetAll();
            Assert.AreEqual(26, list.Count);
        }

        [TestMethod]
        public void SearchByID_Found()
        {
            //Do không có hàm khởi tạo
            var role = myDao.SearchByID("ER01");
            Assert.IsNotNull(role);
        }
    }
}