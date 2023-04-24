using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Account
    {
        private string username = "";
        private string password = "";
        private string employeeID = "";

        public string Username => username;
        public string Password => password;
        public string EmployeeID => employeeID;

        public Account() { }

        public Account(string username, string password, string employeeID)
        {
            this.username = username;
            this.password = password;
            this.employeeID = employeeID;
        }

        public Account(IDataRecord reader)
        {
            try
            {
                username = (string)reader[BaseDao.ACCOUNT_USERNAME];
                password = (string)reader[BaseDao.ACCOUNT_PASSWORD];
                employeeID = (string)reader[BaseDao.ACCOUNT_EMPLOYEE_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
