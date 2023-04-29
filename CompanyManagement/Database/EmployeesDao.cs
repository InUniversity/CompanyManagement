using CompanyManagement.Database.Base;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class EmployeesDao : BaseDao
    {
        public void Add(Employee empl)
        {
            string sqlStr =
                $"INSERT INTO {EMPLOYEES_TABLE} ({EMPLOYEES_ID}, {EMPLOYEES_NAME}, {EMPLOYEES_GENDER}, {EMPLOYEES_BIRTHDAY}, " +
                $"{EMPLOYEES_IDENTIFY_CARD}, {EMPLOYEES_EMAIL}, {EMPLOYEES_PHONE_NUMBER}, {EMPLOYEES_ADDRESS}, " +
                $"{EMPLOYEES_DEPARTMENT_ID},{EMPLOYEES_POSITION_ID}, {EMPLOYEES_SALARY}) VALUES ('{empl.ID}', " +
                $"N'{empl.Name}', N'{empl.Gender}', '{empl.Birthday}', '{empl.IdentifyCard}', '{empl.Email}', " +
                $"'{empl.PhoneNumber}', N'{empl.Address}', '{empl.DepartmentID}', '{empl.RoleID}', '{empl.Salary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {EMPLOYEES_TABLE} WHERE {EMPLOYEES_ID}='{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Employee empl)
        {
            string sqlStr = $"UPDATE {EMPLOYEES_TABLE} " +
                $"SET {EMPLOYEES_NAME}=N'{empl.Name}', {EMPLOYEES_GENDER}=N'{empl.Gender}', " +
                $"{EMPLOYEES_BIRTHDAY}='{empl.Birthday}', {EMPLOYEES_IDENTIFY_CARD}='{empl.IdentifyCard}', " +
                $"{EMPLOYEES_EMAIL}='{empl.Email}', {EMPLOYEES_PHONE_NUMBER}='{empl.PhoneNumber}', " +
                $"{EMPLOYEES_ADDRESS}='{empl.Address}', {EMPLOYEES_DEPARTMENT_ID}='{empl.DepartmentID}', " +
                $"{EMPLOYEES_POSITION_ID}='{empl.RoleID}', {EMPLOYEES_SALARY}='{empl.Salary}' " +
                $"WHERE {EMPLOYEES_ID}='{empl.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Employee> SearchByCurrentID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEES_TABLE} E WHERE E.{EMPLOYEES_ID} NOT LIKE '{employeeID}'";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEES_TABLE} WHERE {EMPLOYEES_ID}='{id}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByIdentifyCard(string identifyCard)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEES_TABLE} WHERE {EMPLOYEES_IDENTIFY_CARD}='{identifyCard}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByPhoneNumber(string phoneNumber)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEES_TABLE} WHERE {EMPLOYEES_PHONE_NUMBER}='{phoneNumber}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByPositionID(string id)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEES_TABLE} WHERE {EMPLOYEES_POSITION_ID} ='{id}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }
    }
}