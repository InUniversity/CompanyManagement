using CompanyManagement.Database.Interfaces;
using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Implementations
{
    public class EmployeeDao : BaseDao, IEmployeeDao
    {
        public void Add(Employee empl)
        {
            AddEmployee(empl);
            AddAccount(empl.ID, empl.EmplAccount);
        }

        private void AddEmployee(Employee empl)
        {
            string sqlStr =
                $"INSERT INTO {EMPLOYEE_TABLE} ({EMPLOYEE_ID}, {EMPLOYEE_NAME}, {EMPLOYEE_GENDER}, {EMPLOYEE_BIRTHDAY}, " +
                $"{EMPLOYEE_IDENTIFY_CARD}, {EMPLOYEE_EMAIL}, {EMPLOYEE_PHONE_NUMBER}, {EMPLOYEE_ADDRESS}, " +
                $"{EMPLOYEE_DEPARTMENT_ID},{EMPLOYEE_POSITION_ID}, {EMPLOYEE_SALARY}) VALUES ('{empl.ID}', " +
                $"N'{empl.Name}', N'{empl.Gender}', '{empl.Birthday}', '{empl.IdentifyCard}', '{empl.Email}', " +
                $"'{empl.PhoneNumber}', N'{empl.Address}', '{empl.DepartmentID}', '{empl.PositionID}', '{empl.Salary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        private void AddAccount(string employeeID, Account account)
        {
            string sqlStr = $"INSERT INTO {ACCOUNT_TABLE} ({ACCOUNT_USERNAME}, {ACCOUNT_USERNAME}, {ACCOUNT_EMPLOYEE_ID})" +
                            $"VALUES ({account.Username}, {account.Password}, {employeeID})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            DeleteEmployee(id);
            DeleteAccount(id);
        }

        private void DeleteEmployee(string id)
        {
            string sqlStr = $"DELETE FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_ID}='{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        private void DeleteAccount(string employeeID)
        {
            string sqlStr = $"DELETE FROM {ACCOUNT_TABLE} WHERE {ACCOUNT_EMPLOYEE_ID}='{employeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Employee empl)
        {
            UpdateEmployee(empl);
            UpdateAccount(empl.ID, empl.EmplAccount);
        }

        private void UpdateEmployee(Employee empl)
        {
            string sqlStr = $"UPDATE {EMPLOYEE_TABLE} " +
                $"SET {EMPLOYEE_NAME}=N'{empl.Name}', {EMPLOYEE_GENDER}=N'{empl.Gender}', " +
                $"{EMPLOYEE_BIRTHDAY}='{empl.Birthday}', {EMPLOYEE_IDENTIFY_CARD}='{empl.IdentifyCard}', " +
                $"{EMPLOYEE_EMAIL}='{empl.Email}', {EMPLOYEE_PHONE_NUMBER}='{empl.PhoneNumber}', " +
                $"{EMPLOYEE_ADDRESS}='{empl.Address}', {EMPLOYEE_DEPARTMENT_ID}='{empl.DepartmentID}', " +
                $"{EMPLOYEE_POSITION_ID}='{empl.PositionID}', {EMPLOYEE_SALARY}='{empl.Salary}' " +
                $"WHERE {EMPLOYEE_ID}='{empl.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        private void UpdateAccount(string employeeID, Account account)
        {
            string sqlStr = $"UPDATE {ACCOUNT_TABLE} SET {ACCOUNT_USERNAME}='{account.Username}', " +
                            $"{ACCOUNT_PASSWORD}='{account.Password}' WHERE {ACCOUNT_EMPLOYEE_ID}='{employeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Employee> GetAll()
        {
            string sqlStr = $"SELECT E.*, A.{ACCOUNT_USERNAME}, A.{ACCOUNT_PASSWORD} FROM {EMPLOYEE_TABLE} " +
                            $"E INNER JOIN {ACCOUNT_TABLE} A ON E.{EMPLOYEE_ID}=A.{ACCOUNT_EMPLOYEE_ID}";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByID(string id)
        {
            string sqlStr = $"SELECT E.*, A.{ACCOUNT_USERNAME}, A.{ACCOUNT_PASSWORD} " +
                            $"FROM {EMPLOYEE_TABLE} E INNER JOIN {ACCOUNT_TABLE} A ON " +
                            $"E.{EMPLOYEE_ID}=A.{ACCOUNT_EMPLOYEE_ID} WHERE {EMPLOYEE_ID}='{id}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByIdentifyCard(string identifyCard)
        {
            string sqlStr = $"SELECT E.*, A.{ACCOUNT_USERNAME}, A.{ACCOUNT_PASSWORD} " +
                            $"FROM {EMPLOYEE_TABLE} E INNER JOIN {ACCOUNT_TABLE} A ON " +
                            $"E.{EMPLOYEE_ID}=A.{ACCOUNT_EMPLOYEE_ID} WHERE {EMPLOYEE_IDENTIFY_CARD}='{identifyCard}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByPhoneNumber(string phoneNumber)
        {
            string sqlStr = $"SELECT E.*, A.{ACCOUNT_USERNAME}, A.{ACCOUNT_PASSWORD} " +
                            $"FROM {EMPLOYEE_TABLE} E INNER JOIN {ACCOUNT_TABLE} A ON " +
                            $"E.{EMPLOYEE_ID}=A.{ACCOUNT_EMPLOYEE_ID} WHERE {EMPLOYEE_PHONE_NUMBER}='{phoneNumber}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByUsername(string username)
        {
            string sqlStr = $"SELECT E.*, A.{ACCOUNT_USERNAME}, A.{ACCOUNT_PASSWORD} " +
                            $"FROM {EMPLOYEE_TABLE} E INNER JOIN {ACCOUNT_TABLE} A ON " +
                            $"E.{EMPLOYEE_ID}=A.{ACCOUNT_EMPLOYEE_ID} WHERE {ACCOUNT_USERNAME}='{username}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByName(string fullName)
        {
            string sqlStr = $"SELECT E.*, A.{ACCOUNT_USERNAME}, A.{ACCOUNT_PASSWORD} " +
                            $"FROM {EMPLOYEE_TABLE} E INNER JOIN {ACCOUNT_TABLE} A ON " +
                            $"E.{EMPLOYEE_ID}=A.{ACCOUNT_EMPLOYEE_ID} WHERE {EMPLOYEE_NAME}='{fullName}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }
    }
}