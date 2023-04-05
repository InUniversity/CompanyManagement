using System;
using System.Data.SqlClient;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Account
    {
        private string username = "";
        private string password = "";
        private string employeeID = "";

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public Account() { }

        public Account(string username, string password, string employeeID)
        {
            this.username = username;
            this.password = password;
            this.employeeID = employeeID;
        }

        public Account(SqlDataReader reader)
        {
            try
            {
                Username = (string)reader[BaseDao.ACCOUNT_USERNAME];
                Password = (string)reader[BaseDao.ACCOUNT_PASSWORD];
                employeeID = (string)reader[BaseDao.ACCOUNT_EMPLOYEE_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
