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
        private const string ID = "employee_id";
        private const string NAME = "employee_name";
        private const string GENDER = "gender";
        private const string BIRTHDAY = "birthday";
        private const string IDENTIFY_CARD = "indentify_card";
        private const string PHONE_NUMBER = "phone_number";
        private const string MANAGER_ID = "manager_id";
        private const string SALARY = "salary";
        private const string ADDRESS = "employee_address";

        DBConnection dbconnection = new DBConnection();

        public void Add(Employee employee)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {GENDER}, {BIRTHDAY}, {IDENTIFY_CARD}, {PHONE_NUMBER}, {MANAGER_ID}, {SALARY}, {ADDRESS}) VALUES ({employee.ID}, {employee.Name}, {employee.Gender}, {employee.Birthday}, {employee.IndentifyCard}, {employee.Phone}, {employee.ManagerID}, {employee.Salary}, {employee.Address})";
                dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(Employee employee)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = {employee.ID}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Employee employee)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{employee.Name}', {GENDER} = '{employee.Gender}', {BIRTHDAY}= '{employee.Birthday}', {IDENTIFY_CARD}= '{employee.IndentifyCard}', {PHONE_NUMBER}= '{employee.Phone}', {MANAGER_ID}= '{employee.ManagerID}', {SALARY}= '{employee.Salary}', {ADDRESS} = '{employee.Address}' WHERE {ID} = '{employee.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);        
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }

        public Employee SearchByID(string id)
        {
            DataTable dtemployee = new DataTable();
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dtemployee = dbconnection.GetDataTable(sqlStr);
            Employee employee = new Employee(
            dtemployee.Columns[0].ToString(),
            dtemployee.Columns[1].ToString(),
            dtemployee.Columns[2].ToString(),
            dtemployee.Columns[3].ToString(),
            dtemployee.Columns[4].ToString(),
            dtemployee.Columns[5].ToString(),
            dtemployee.Columns[6].ToString(),
            int.Parse(dtemployee.Columns[7].ToString()),
            dtemployee.Columns[0].ToString()
                );
            return employee;
        }
    }
}
