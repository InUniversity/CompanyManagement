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
        private const string NAME_TABLE = "Employee";
        private const string ID = "employee_id";
        private const string NAME = "employee_name";
        private const string GENDER = "gender";
        private const string BIRTHDAY = "birthday";
        private const string SSN = "ssn";
        private const string PHONE_NUMBER = "phone_number";
        private const string MANAGER_ID = "manager_id";
        private const string SALARY = "salary";
        private const string ADDRESS = "address";

        DBConnection dbconnection = new DBConnection();
        public void Add(Employee employee)
        {
            string sqlStr = $"INSERT INTO {NAME_TABLE}({ID}, {NAME}, {GENDER}, {BIRTHDAY}, {SSN}, {PHONE_NUMBER}, {MANAGER_ID}, {SALARY}, {ADDRESS}) VALUES ({employee.ID}, {employee.Name}, {employee.Gender}, {employee.Birthday}, {employee.Ssn}, {employee.Phone}, {employee.Mgr_ID}, {employee.Salary}, {employee.Address})";
                dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Delete(Employee employee)
        {
            string sqlStr = $"DELETE FROM {NAME_TABLE} WHERE {ID} = {employee.ID}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Save(Employee employee)
        {
            string sqlStr = $"UPDATE {NAME_TABLE} SET {NAME} = '{employee.Name}', {GENDER} = '{employee.Gender}', {BIRTHDAY}= '{employee.Birthday}', {SSN}= '{employee.Ssn}', {PHONE_NUMBER}= '{employee.Phone}', {MANAGER_ID}= '{employee.Mgr_ID}', {SALARY}= '{employee.Salary}', {ADDRESS} = '{employee.Address}' WHERE employee_id = '{employee.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);        
        }
        public DataTable ReturnList()
        {
            string sqlStr = string.Format("SELECT * FROM Employee");
            return dbconnection.GetDataTable(sqlStr);
        }
    }
}
