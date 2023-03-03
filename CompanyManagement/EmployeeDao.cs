using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement
{
    public class EmployeeDao
    {
        private const string TABLE_NAME = "Employee";
        private const string ID = "employeeID";
        private const string NAME = "employeeName";
        private const string GENDER = "gender";
        private const string BIRTHDAY = "birthDay";
        private const string SSN = "ssn";
        private const string PHONE_NUMBER = "phoneNumber";
        private const string MANAGER_ID = "managerID";
        private const string SALARY = "salary";
        private const string ADDRESS = "address";

        DBConnection dbconnection = new DBConnection();
        public void Add(Employee employee)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {GENDER}, {BIRTHDAY}, {SSN}, {PHONE_NUMBER}, {MANAGER_ID}, {SALARY}, {ADDRESS}) VALUES ({employee.ID}, {employee.Name}, {employee.Gender}, {employee.Birthday}, {employee.Ssn}, {employee.Phone}, {employee.MgrID}, {employee.Salary}, {employee.Address})";
                dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Delete(Employee employee)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = {employee.ID}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Save(Employee employee)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{employee.Name}', {GENDER} = '{employee.Gender}', {BIRTHDAY}= '{employee.Birthday}', {SSN}= '{employee.Ssn}', {PHONE_NUMBER}= '{employee.Phone}', {MANAGER_ID}= '{employee.MgrID}', {SALARY}= '{employee.Salary}', {ADDRESS} = '{employee.Address}' WHERE employee_id = '{employee.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);        
        }
        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }
        public DataTable SearchByID(Employee employee)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{employee.ID}'";
            return dbconnection.GetDataTable(sqlStr);
        }

    }
}
