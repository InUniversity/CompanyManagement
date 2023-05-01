using CompanyManagement.Database.Base;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class EmployeesDao : BaseDao
    {
        public void Add(Employee empl)
        {
            string sqlStr =
                $"INSERT INTO {emplTbl} ({emplID}, {emplName}, {emplGender}, {emplBirthday}, " +
                $"{emplIdentCard}, {emplEmail}, {emplPhoneNo}, {emplAddress}, " +
                $"{emplDeptID},{emplRoleID}, {emplSalary}) VALUES ('{empl.ID}', " +
                $"N'{empl.Name}', N'{empl.Gender}', '{empl.Birthday}', '{empl.IdentifyCard}', '{empl.Email}', " +
                $"'{empl.PhoneNumber}', N'{empl.Address}', '{empl.DepartmentID}', '{empl.RoleID}', '{empl.Salary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {emplTbl} WHERE {emplID}='{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Employee empl)
        {
            string sqlStr = $"UPDATE {emplTbl} " +
                $"SET {emplName}=N'{empl.Name}', {emplGender}=N'{empl.Gender}', " +
                $"{emplBirthday}='{empl.Birthday}', {emplIdentCard}='{empl.IdentifyCard}', " +
                $"{emplEmail}='{empl.Email}', {emplPhoneNo}='{empl.PhoneNumber}', " +
                $"{emplAddress}='{empl.Address}', {emplDeptID}='{empl.DepartmentID}', " +
                $"{emplRoleID}='{empl.RoleID}', {emplSalary}='{empl.Salary}' " +
                $"WHERE {emplID}='{empl.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Employee> SearchByCurrentID(string emplID)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} E WHERE E.{BaseDao.emplID} NOT LIKE '{emplID}'";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplID}='{id}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByIdentifyCard(string identCard)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplIdentCard}='{identCard}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public Employee SearchByPhoneNumber(string phoneNo)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplPhoneNo}='{phoneNo}'";
            return (Employee)dbConnection.GetSingleObject(sqlStr, reader => new Employee(reader));
        }

        public List<Employee> GetManagers()
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplRoleID} ='{managerRole}'";
            return dbConnection.GetList<Employee>(sqlStr, reader => new Employee(reader));
        }
    }
}