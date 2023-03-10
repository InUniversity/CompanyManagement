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
        public static string IDENTIFY_CARD = "identify_card";
        public static string PHONE_NUMBER = "phone_number";
        public static string MANAGER_ID = "manager_id";
        public static string SALARY = "salary";
        public static string ADDRESS = "employee_address";

        DBConnection dbconnection = new DBConnection();

        public void Add(Employee empl)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME} ({ID}, {NAME}, {GENDER}, {BIRTHDAY}, {IDENTIFY_CARD}, {PHONE_NUMBER}, {MANAGER_ID}, {SALARY}, {ADDRESS}) " +
                $"VALUES ('{empl.ID}', N'{empl.Name}', N'{empl.Gender}', '{empl.Birthday.ToString()}', '{empl.IndentifyCard}', '{empl.PhoneNumber}' ," +
                $"'{empl.ManagerID}', {empl.Salary} , N'{empl.Address}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Employee empl)
        {
            string sqlStr = $"UPDATE {TABLE_NAME}" +
                $"SET {NAME} = N'{empl.Name}', {GENDER} = N'{empl.Gender}', {BIRTHDAY} = '{empl.Birthday.ToString()}', {IDENTIFY_CARD}= '{empl.IndentifyCard}'," +
                $"{PHONE_NUMBER} = '{empl.PhoneNumber}', {MANAGER_ID}= '{empl.ManagerID}', {SALARY}= '{empl.Salary}', {ADDRESS} = N'{empl.Address}'" +
                $" WHERE {ID} = '{empl.ID}'";
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
            DataTable dataTable = dbconnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Employee(dataTable.Rows[0]);
        }
    }
}
