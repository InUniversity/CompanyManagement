using CompanyManagement.Database.Base;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class EmployeesDao : BaseDao
    {
        public void Add(Employee empl)
        {
            string sqlStr =
                $"INSERT INTO {EMPLOYEE_TABLE} ({EMPLOYEE_ID}, {EMPLOYEE_NAME}, {EMPLOYEE_GENDER}, {EMPLOYEE_BIRTHDAY}, " +
                $"{EMPLOYEE_IDENTIFY_CARD}, {EMPLOYEE_EMAIL}, {EMPLOYEE_PHONE_NUMBER}, {EMPLOYEE_ADDRESS}, " +
                $"{EMPLOYEE_DEPARTMENT_ID},{EMPLOYEE_POSITION_ID}, {EMPLOYEE_SALARY}) VALUES ('{empl.ID}', " +
                $"N'{empl.Name}', N'{empl.Gender}', '{empl.Birthday}', '{empl.IdentifyCard}', '{empl.Email}', " +
                $"'{empl.PhoneNumber}', N'{empl.Address}', '{empl.DepartmentID}', '{empl.RoleID}', '{empl.Salary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_ID}='{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Employee empl)
        {
            string sqlStr = $"UPDATE {EMPLOYEE_TABLE} " +
                $"SET {EMPLOYEE_NAME}=N'{empl.Name}', {EMPLOYEE_GENDER}=N'{empl.Gender}', " +
                $"{EMPLOYEE_BIRTHDAY}='{empl.Birthday}', {EMPLOYEE_IDENTIFY_CARD}='{empl.IdentifyCard}', " +
                $"{EMPLOYEE_EMAIL}='{empl.Email}', {EMPLOYEE_PHONE_NUMBER}='{empl.PhoneNumber}', " +
                $"{EMPLOYEE_ADDRESS}='{empl.Address}', {EMPLOYEE_DEPARTMENT_ID}='{empl.DepartmentID}', " +
                $"{EMPLOYEE_POSITION_ID}='{empl.RoleID}', {EMPLOYEE_SALARY}='{empl.Salary}' " +
                $"WHERE {EMPLOYEE_ID}='{empl.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Employee> SearchByCurrentID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} E WHERE E.{EMPLOYEE_ID} NOT LIKE '{employeeID}'";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_ID}='{id}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByIdentifyCard(string identifyCard)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_IDENTIFY_CARD}='{identifyCard}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByPhoneNumber(string phoneNumber)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_PHONE_NUMBER}='{phoneNumber}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByPositionID(string id)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_POSITION_ID} ='{id}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }
    }
}