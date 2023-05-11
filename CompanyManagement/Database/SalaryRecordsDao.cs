using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CompanyManagement.Database
{
    public class SalaryRecordsDao : BaseDao
    {

        public void Add(SalaryRecord salaryRecord)
        {
            string sqlStr = $"INSERT INTO {salaryTbl} ({salaryID},{salaryEmplID}, {salaryMonthYear}, " +
                            $"{salaryWorkdays}, {salaryBonus}, {salaryIncome}) " +
                            $"VALUES ('{salaryRecord.ID}','{salaryRecord.EmployeeID}', '{salaryRecord.MonthYear}', " +
                            $"'{salaryRecord.TotalWorkDays}', '{salaryRecord.TotalBonuses}', '{salaryRecord.Income}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {salaryTbl} WHERE {salaryID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void DeleteByEmployee(string employeeID, DateTime monthYear)
        {
            string sqlStr = $"DELETE FROM {salaryTbl} WHERE {salaryEmplID} = '{employeeID}' " +
                            $"MONTH({salaryMonthYear}) = '{monthYear.Month}' " +
                            $"AND YEAR({salaryMonthYear}) = '{monthYear.Year}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void DeleteByMonthYear(int month, int year)
        {
            string sqlStr = $"DELETE FROM {salaryTbl} WHERE MONTH({salaryMonthYear}) = '{month}' " +
                            $"AND YEAR({salaryMonthYear}) = '{year}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public SalaryRecord SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {salaryTbl} WHERE {salaryID} = '{id}'";
            return (SalaryRecord)dbConnection.GetSingleObject(sqlStr, reader => new SalaryRecord(reader));
        }

        public List<SalaryRecord> GetByTime(int month, int year)
        {
            string sqlStr = $"SELECT * FROM {salaryTbl} WHERE MONTH({salaryMonthYear}) = '{month}' " +
                            $"AND YEAR({salaryMonthYear}) = '{year}'";
            return dbConnection.GetList<SalaryRecord>(sqlStr, reader => new SalaryRecord(reader));
        }

        public List<SalaryRecord> GetByDepartmentID(string departmentID, int month, int year)
        {
            string sqlStr = $"SELECT * FROM {salaryTbl} WHERE MONTH({salaryMonthYear}) = '{month}' " +
                            $"AND YEAR({salaryMonthYear}) = '{year}' AND {salaryEmplID} " +
                            $"IN (SELECT {emplID} FROM {emplTbl} WHERE {emplDeptID} = '{departmentID}' " +
                            $"OR ('{departmentID}' = 'MNG' AND {emplPermsID} = '{managerPermsID}') " +
                            $"OR ('{departmentID}' = 'HR' AND {emplPermsID} = '{hrPermsID}') OR '{departmentID}' = 'ALL')";
            return dbConnection.GetList<SalaryRecord>(sqlStr, reader => new SalaryRecord(reader));
        }

        public List<SalaryRecord> GetByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {salaryTbl} " +
                            $"WHERE {salaryEmplID} = '{employeeID}'";
            return dbConnection.GetList<SalaryRecord>(sqlStr, reader => new SalaryRecord(reader));
        }
    }
}
