using System;
using CompanyManagementEntity;
using CompanyManagementEntity.Database;
using NUnit.Framework;

namespace CompanyManagementEntityTest.Database
{
    [TestFixture]
    public class EmployeesDaoTest
    {
        private EmployeesDao myDao;

        [SetUp]
        public void SetUp()
        {
            myDao = new EmployeesDao();
        }

        [Test]
        public void SearchByID_Found()
        {
            var expected = new Employee
            {
                ID = "EM001",
                FullName = "Nguyễn Văn An",
                Gender = "Nam",
                Birthday = new DateTime(1990, 1, 1),
                IdentifyCard = "001234567890",
                Email = "an.nguyen@it.company.com",
                PhoneNumber = "0123456789",
                EmployeeAddress = "TP. Hồ Chí Minh",
                DepartmentID = "",
                RoleID = "ER01"
            };
            var actualSearch = myDao.SearchByID(expected.ID);

            AssertObject(expected, actualSearch);
        }

        [Test]
        public void Add_Update_Delete_Success()
        {
            var empl = new Employee
            {
                ID = "EM12312",
                FullName = "Nguyễn Văn Anh Hoàng",
                Gender = "Nam",
                Birthday = new DateTime(2000, 1, 1),
                IdentifyCard = "075234567890",
                Email = "an.nguyen12@gmail.com",
                PhoneNumber = "0123454357",
                EmployeeAddress = "Đồng Nai",
                DepartmentID = "DMP124",
                RoleID = "ER04"
            };

            // add
            myDao.Add(empl);
            var added = myDao.SearchByID(empl.ID);

            // update
            var updateObject = new Employee
            {
                ID = empl.ID,
                FullName = "Phạm Văn Hoàng",
                Gender = empl.Gender,
                Birthday = empl.Birthday,
                IdentifyCard = empl.IdentifyCard,
                Email = "an.ng12@gmail.com",
                PhoneNumber = "0123454456",
                EmployeeAddress = "Bạc Liêu",
                DepartmentID = "",
                RoleID = "ER01"
            };
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

        [Test]
        public void SearchByIdentifyCard_Found()
        {
            var expected = new Employee
            {
                ID = "EM001",
                FullName = "Nguyễn Văn An",
                Gender = "Nam",
                Birthday = new DateTime(1990, 1, 1),
                IdentifyCard = "001234567890",
                Email = "an.nguyen@it.company.com",
                PhoneNumber = "0123456789",
                EmployeeAddress = "TP. Hồ Chí Minh",
                DepartmentID = "",
                RoleID = "ER01"
            };          

            var actual = myDao.SearchByIdentifyCard(expected.IdentifyCard);

            AssertObject(expected, actual);
        }

        [Test]
        public void SearchByPhoneNumber_Found()
        {
            var expected = new Employee
            {
                ID = "EM001",
                FullName = "Nguyễn Văn An",
                Gender = "Nam",
                Birthday = new DateTime(1990, 1, 1),
                IdentifyCard = "001234567890",
                Email = "an.nguyen@it.company.com",
                PhoneNumber = "0123456789",
                EmployeeAddress = "TP. Hồ Chí Minh",
                DepartmentID = "",
                RoleID = "ER01"
            };

            var actual = myDao.SearchByPhoneNumber(expected.PhoneNumber);

            AssertObject(expected, actual);
        }

        private void AssertObject(Employee expected, Employee actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.Birthday, actual.Birthday);
            Assert.AreEqual(expected.IdentifyCard, actual.IdentifyCard);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(expected.DepartmentID, actual.DepartmentID);
            Assert.AreEqual(expected.RoleID, actual.RoleID);
        }
    }
}