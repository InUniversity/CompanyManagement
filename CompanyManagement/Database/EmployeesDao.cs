using CompanyManagement.Database.Base;
using System.Collections.Generic;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class EmployeesDao : BaseDao
    {
        public void Add(Employee empl)
        {
            string sqlStr =
                $"INSERT INTO {emplTbl} ({emplID}, {emplName}, {emplGender}, {emplBirthday}, " +
                $"{emplIdentCard}, {emplEmail}, {emplPhoneNo}, {emplAddress},  {emplPermsID}, {emplDeptID}," +
                $"{emplRoleID}) VALUES ('{empl.ID}', N'{empl.Name}', N'{empl.Gender}', " +
                $"'{Utils.ToSQLFormat(empl.Birthday)}', '{empl.IdentifyCard}', '{empl.Email}', " +
                $"'{empl.PhoneNumber}', N'{empl.Address}', '{empl.PermsID}', '{empl.DepartmentID}', '{empl.RoleID}')";
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
                $"{emplBirthday}='{Utils.ToSQLFormat(empl.Birthday)}', " +
                $"{emplIdentCard}='{empl.IdentifyCard}', {emplEmail}='{empl.Email}', " +
                $"{emplPhoneNo}='{empl.PhoneNumber}', {emplAddress}=N'{empl.Address}', " +
                $"{emplPermsID}='{empl.PermsID}', {emplDeptID}='{empl.DepartmentID}', " +
                $"{emplRoleID}='{empl.RoleID}' WHERE {emplID}='{empl.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Employee> GetEmployeeDoing()
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplDeptID} != '' " +
                            $"OR {emplPerms} = '{managerPermsID}' OR {emplPerms} = '{hrPermsID}'";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public List<Employee> GetAllWithoutManagers()
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplPermsID} NOT LIKE '{managerPermsID}'";
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

        public List<Employee> GetHeaderDepts()
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplPermsID} ='{hrPermsID}'";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public List<Employee> SearchByDepartmentID(string deptID)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplDeptID}='{deptID}'";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }
    }
}