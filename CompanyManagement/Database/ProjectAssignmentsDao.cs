using System.Collections.Generic;
using System.Linq;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class ProjectAssignmentsDao : BaseDao
    {
        public void Add(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"INSERT INTO {projAssignTbl} ({projAssignID}, " +
                            $"{projAssignDeptID}) VALUES ('{projectAssignment.ProjectID}', " +
                            $"'{projectAssignment.DeparmentID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"DELETE FROM {projAssignTbl} " +
                            $"WHERE {projAssignID}='{projectAssignment.ProjectID}' AND " +
                            $"{projAssignDeptID}='{projectAssignment.DeparmentID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAllDepartmentInProject(string projectID)
        {
            string sqlStr = $"SELECT D.* FROM {deptTbl} D INNER JOIN {projAssignTbl} PA ON " +
                            $"D.{deptID}=PA.{projAssignDeptID} " +
                            $"WHERE PA.{projAssignID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Employee> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplDeptID} IN(" +
                $"SELECT {projAssignDeptID} FROM {projAssignTbl} WHERE {projAssignID}='{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public List<Department> GetDepartmentsCanAssignWork(string projectID, string startDateTime, string endDateTime)
        {
            string sqlStr = $"SELECT * FROM {deptTbl} WHERE {deptID} NOT IN (" +
                            $"SELECT {projAssignDeptID} FROM {projAssignTbl} " +
                            $"WHERE {projAssignID} IN (SELECT {projID} FROM {projTbl} " +
                            $"WHERE {projID} NOT LIKE '{projectID}' AND {projProgress} NOT LIKE '100'" +
                            $"AND {projStart} <= '{endDateTime}'" +
                            $"AND {projEnd} >= '{startDateTime}')) EXCEPT (SELECT D.* FROM {deptTbl} D INNER JOIN {projAssignTbl} PA ON D.{deptID}=PA.{projAssignDeptID} WHERE PA.{projAssignID}='{projectID}')";
            // TODO
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Project> SearchProjectByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {projTbl} WHERE {projID} IN " +
                            $"(SELECT {projAssignID} FROM {projAssignTbl} PA, {emplTbl} E " +
                            $"WHERE PA.{projAssignDeptID}=E.{emplDeptID} " +
                            $"AND E.{emplID}='{employeeID}')";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }

        public List<Project> SearchProjectByCreatorID(string managerID)
        {
            string sqlStr = $"SELECT * FROM {projTbl} P WHERE P.{projOwnerID}='{managerID}'";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }
    }
}
