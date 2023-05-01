﻿using System.Collections.Generic;
using System.Linq;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class ProjectAssignmentsDao : BaseDao
    {
        public void Add(ProjectAssignment assign)
        {
            string sqlStr = $"INSERT INTO {projAssignTbl} ({projAssignID}, " + $"{projAssignDeptID}) " +
                            $"VALUES ('{assign.ProjID}', '{assign.DeparmentID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(ProjectAssignment assign)
        {
            string sqlStr = $"DELETE FROM {projAssignTbl} WHERE {projAssignID}='{assign.ProjID}' AND " +
                            $"{projAssignDeptID}='{assign.DeparmentID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAllDepartmentInProject(string projID)
        {
            string sqlStr = $"SELECT D.* FROM {deptTbl} D INNER JOIN {projAssignTbl} PA ON " +
                            $"D.{deptID}=PA.{projAssignDeptID} WHERE PA.{projAssignID}='{projID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Employee> GetEmployeesInProject(string projID)
        {
            string sqlStr = $"SELECT * FROM {emplTbl} WHERE {emplDeptID} IN(" +
                $"SELECT {projAssignDeptID} FROM {projAssignTbl} WHERE {projAssignID}='{projID}')";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public List<Department> GetDepartmentsCanAssignWork(string projID, string startDateTime, string endDateTime)
        {
            string sqlStr = $"SELECT * FROM {deptTbl} WHERE {deptID} NOT IN (" +
                            $"SELECT {projAssignDeptID} FROM {projAssignTbl} " +
                            $"WHERE {projAssignID} IN (SELECT {BaseDao.projID} FROM {projTbl} " +
                            $"WHERE {BaseDao.projID} NOT LIKE '{projID}' AND {projProgress} NOT LIKE '100'" +
                            $"AND {projStart} <= '{endDateTime}'" +
                            $"AND {projEnd} >= '{startDateTime}')) EXCEPT (SELECT D.* FROM {deptTbl} D INNER JOIN {projAssignTbl} PA ON D.{deptID}=PA.{projAssignDeptID} WHERE PA.{projAssignID}='{projID}')";
            // TODO
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Project> SearchProjectByEmployeeID(string emplID)
        {
            string sqlStr = $"SELECT * FROM {projTbl} WHERE {projID} IN " +
                            $"(SELECT {projAssignID} FROM {projAssignTbl} PA, {emplTbl} E " +
                            $"WHERE PA.{projAssignDeptID}=E.{emplDeptID} " +
                            $"AND E.{BaseDao.emplID}='{emplID}')";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }

        public List<Project> SearchProjectByCreatorID(string mgrID)
        {
            string sqlStr = $"SELECT * FROM {projTbl} P WHERE P.{projOwnerID}='{mgrID}'";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }
    }
}
