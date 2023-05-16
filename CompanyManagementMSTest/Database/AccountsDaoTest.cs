using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class AccountsDaoTest
    {
        private AccountsDao myDao;

        [TestInitialize]
        public void SetUp()
        {
            myDao = new AccountsDao();
        }

        [TestMethod]
        public void Add_Update_Delete_Success()
        {
            var acc = new Account("TestTest", "1234567", "EM057");
            
            //add
            myDao.Add(acc);
            var added = Search(acc);
            
            //update
            var updateAcc = new Account(acc.Username, "987654321", acc.EmployeeID);
            myDao.Update(updateAcc);
            var updated = Search(updateAcc);
            
            //delete
            myDao.Delete(acc.EmployeeID);
            var deleted = Search(acc);
            
            // assert added
            AssertObject(acc, added);
            
            //assert updated
            AssertObject(updateAcc, updated);
            
            //assert deleted
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void SearchByUsername_Found()
        {
            var acc = myDao.SearchByUsername("EM0010101");
            Assert.IsNotNull(acc);
        }
        
        
        public Account Search(Account account)
        {
            DbConnection dbConnection = new DbConnection();
            string sqlStr = $"SELECT * FROM Accounts WHERE Username = '{account.Username}' ";
            return (Account)dbConnection.GetSingleObject(sqlStr, reader => new Account(reader));
        }
        
        private void AssertObject(Account expected, Account actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.EmployeeID, actual.EmployeeID);
        }
    }
}