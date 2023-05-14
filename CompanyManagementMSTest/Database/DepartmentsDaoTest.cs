using CompanyManagement.Database;
using CompanyManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class DepartmentsDaoTest
    {
        private DepartmentsDao myDao;
        
        [TestInitialize]
        public void SetUp()
        {
            myDao = new DepartmentsDao();
        }
        
        [TestMethod]
        public void SearchByID_Found()
        {
            var expected = new Department("DPM001", "Software Development Department", "EM006");
            var actualSearch = myDao.SearchByID(expected.ID);
            
            AssertObject(expected, actualSearch);
        }

        [TestMethod]
        public void Add_Update_Delete_Success()
        {
            var dept = new Department("DPM1231", "Core Department Test", "EM123");  
            
            // add
            myDao.Add(dept); 
            var added = myDao.SearchByID(dept.ID);
            
            // update
            var updateObject = new Department(dept.ID, dept.Name + " HELOO", "EM4356");
            myDao.Update(updateObject);
            var updated = myDao.SearchByID(dept.ID);
            
            // add
            myDao.Delete(dept.ID);
            var afterDelete = myDao.SearchByID(dept.ID);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(dept, added);
            
            // assert updated
            AssertObject(updateObject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }

        [TestMethod]
        public void GetAll_Success()
        {
            var list = myDao.GetAll();
            Assert.AreEqual(5, list.Count);
        }
        
        private void AssertObject(Department expected, Department actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.DeptHeadID, actual.DeptHeadID);
        }
    }
}