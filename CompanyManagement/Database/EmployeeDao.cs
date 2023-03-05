using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Database
{
    public class EmployeeDao
    {
        public static string TABLE_NAME = "Employee";
        public static string ID = "employee_id";
        public static string NAME = "employee_name";
        public static string GENDER = "gender";
        public static string BIRTHDAY = "birthday";
        public static string IDENTIFY_CARD = "indentify_card";
        public static string PHONE_NUMBER = "phone_number";
        public static string MANAGER_ID = "manager_id";
        public static string SALARY = "salary";
        public static string ADDRESS = "employee_address";

        DBConnection dbconnection = new DBConnection();

        public void Add(Employee employee)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME} ({ID}, {NAME}, {GENDER}, {BIRTHDAY}, {IDENTIFY_CARD}, {PHONE_NUMBER}, {MANAGER_ID}, {SALARY}, {ADDRESS})" +
                $"VALUES ('{employee.ID}', '{employee.Name}', '{employee.Gender}', {employee.Birthday}, '{employee.IndentifyCard}', '{employee.Phone}'," +
                $"'{employee.ManagerID}', '{employee.Salary}', '{employee.Address}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(Employee employee)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = {employee.ID}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Employee employee)
        {
            string sqlStr = $"UPDATE {TABLE_NAME}" +
                $"SET {NAME} = '{employee.Name}', {GENDER} = '{employee.Gender}', {BIRTHDAY}= {employee.Birthday}, {IDENTIFY_CARD}= '{employee.IndentifyCard}'," +
                $"{PHONE_NUMBER} = '{employee.Phone}', {MANAGER_ID}= '{employee.ManagerID}', {SALARY}= '{employee.Salary}', {ADDRESS} = '{employee.Address}'" +
                $" WHERE {ID} = '{employee.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }

        public Employee SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            DataTable dtemployee = dbconnection.GetDataTable(sqlStr);
            if (dtemployee.Rows.Count == 0)
                return null;
            return new Employee(dtemployee.Rows[0]);
        }
    }
}
