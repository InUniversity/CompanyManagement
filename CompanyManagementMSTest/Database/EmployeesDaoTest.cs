using System;
using CompanyManagement;
using CompanyManagement.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class EmployeesDaoTest
    {
        private EmployeesDao myDao;
        
        [TestInitialize]
        public void SetUp()
        {
            myDao = new EmployeesDao();
        }
        
        [TestMethod]
        public void SearchByID_Found()
        {
            var expected = new Employee("EM001", "Nguyễn Văn An", "Nam", 
                new DateTime(1990, 1, 1), "001234567890", 
                "an.nguyen@it.company.com", "0123456789", 
                "TP. Hồ Chí Minh", "", "ER01");
            var actualSearch = myDao.SearchByID(expected.ID);
            
            AssertObject(expected, actualSearch);
        }

        [TestMethod]
        public void Add_Update_Delete_Success()
        {
            var empl = new Employee("EM12312", "Nguyễn Văn Anh Hoàng", "Nam", 
                new DateTime(2000, 1, 1), "075234567890", 
                "an.nguyen12@gmail.com", "0123454357", "Đồng Nai", 
                "DMP124", "ER04");  
            
            // add
            myDao.Add(empl); 
            var added = myDao.SearchByID(empl.ID);
            
            // update
            var updateObject = new Employee(empl.ID, "Phạm Văn Hoàng", empl.Gender, empl.Birthday, 
                empl.IdentifyCard, "an.ng12@gmail.com", "0123454456", 
                "Bạc Liêu", "", "ER02");
            myDao.Update(updateObject);
            var updated = myDao.SearchByID(empl.ID);  
            
            // add
            myDao.Delete(empl.ID);
            var afterDelete = myDao.SearchByID(empl.ID);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(empl, added);
            
            // assert updated
            AssertObject(updateObject, updated);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }

        [TestMethod]
        public void GetAll_Found()
        {
            var list = myDao.GetAll();
            Assert.AreEqual(55, list.Count);
        }

        [TestMethod]
        public void GetAllWithoutManagers_Found()
        {
            var list = myDao.GetAllWithoutManagers();
            Assert.AreEqual(50, list.Count);
        }
        
        [TestMethod]
        public void SearchByIdentifyCard_Found()
        {
            var expected = new Employee("EM001", "Nguyễn Văn An", "Nam", 
                new DateTime(1990, 1, 1), "001234567890", 
                "an.nguyen@it.company.com", "0123456789", 
                "TP. Hồ Chí Minh", "", "ER01");
            
            var actual = myDao.SearchByIdentifyCard(expected.IdentifyCard);
            
            AssertObject(expected, actual);
        }
        
        [TestMethod]
        public void SearchByPhoneNumber_Found()
        {
            var expected = new Employee("EM001", "Nguyễn Văn An", "Nam", 
                new DateTime(1990, 1, 1), "001234567890", 
                "an.nguyen@it.company.com", "0123456789", 
                "TP. Hồ Chí Minh", "", "ER01");
            
            var actual = myDao.SearchByPhoneNumber(expected.PhoneNumber);
            
            AssertObject(expected, actual);
        }
        
        [TestMethod]
        public void GetRequestApprovers_Found()
        {
            var list = myDao.GetRequestApprovers();
            Assert.AreEqual(1,list.Count);
        }

        [TestMethod]
        public void SearchByDepartmentID_Found()
        {
            var expected = new Employee("EM001", "Nguyễn Văn An", "Nam", 
                new DateTime(1990, 1, 1), "001234567890", 
                "an.nguyen@it.company.com", "0123456789", 
                "TP. Hồ Chí Minh", "", "ER01");
            
            var list = myDao.SearchByDepartmentID(expected.DepartmentID);
            
            Assert.AreEqual(6, list.Count);
        }
        
        private void AssertObject(Employee expected, Employee actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.Birthday, actual.Birthday);
            Assert.AreEqual(expected.IdentifyCard, actual.IdentifyCard);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.Address, actual.Address);
            Assert.AreEqual(expected.DepartmentID, actual.DepartmentID);
            Assert.AreEqual(expected.RoleID, actual.RoleID);
        }
    }
}