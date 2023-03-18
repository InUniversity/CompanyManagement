using System;
using System.Data;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement.Models
{
    public class Account
    {

        private string username;
        private string password;
        private string employeeId;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }
        
        public Account() { }

        public Account(string username, string password, string employeeId)
        {
            this.username = username;
            this.password = password;
            this.employeeId = employeeId;
        }
        
        public Account(DataRow row)
        {
            try
            {
                username = (string)row[AccountDao.USERNAME];
                password = (string)row[AccountDao.PASSWORD];
                employeeId = (string)row[AccountDao.EMPLOYEE_ID];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
